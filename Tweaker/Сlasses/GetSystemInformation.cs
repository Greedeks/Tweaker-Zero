using System;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tweaker.Pages;

namespace Tweaker.Сlasses
{
    internal sealed class GetSystemInformation
    {
        #region Image User
        internal static ImageSource _urlImage = default;
        private string _UserName = default;

        [DllImport("shell32.dll", EntryPoint = "#261", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void GetUserTilePath(string username, UInt32 whatever, StringBuilder picpath, int maxLength);

        private string GetUserTilePath(string username)
        {
            var _sb = new StringBuilder(1000);
            GetUserTilePath(username, 0x80000000, _sb, _sb.Capacity);
            _UserName = username;
            return _sb.ToString();
        }

        internal ImageSource SetImageUser()
        {
            ImageSource _imgSource = new BitmapImage(new Uri(GetUserTilePath(_UserName)));
            return _imgSource;
        }
        #endregion

        internal string NameUser()
        {
            string _FullName = default;
            foreach (var managementObj in new ManagementObjectSearcher("select FullName from Win32_UserAccount where domain='" + Environment.UserDomainName + "' and name='" + Environment.UserName.ToLower() + "'").Get())
                _FullName = (string)managementObj["FullName"];
            if (_FullName != string.Empty) return _FullName;
            else return Environment.UserName.ToLower();
        }

        private readonly static string[] _INFthisPC = new string[11];
        private string _type = default;
        internal static string _windowsV = default;
        internal static string _ipAddress = "Пожалуйста немного подождите...";
        internal void GetInormationPC()
        {
            Parallel.Invoke(
            () =>
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Caption, OSArchitecture, Version from Win32_OperatingSystem").Get()) {
                    _INFthisPC[0] = Convert.ToString(managementObj["Caption"]).Substring(Convert.ToString(managementObj["Caption"]).IndexOf('W')) + ", " + System.Text.RegularExpressions.Regex.Replace((string)managementObj["OSArchitecture"], @"\-.+", "-bit") + ", V" + (string)managementObj["Version"];
                    _windowsV = Convert.ToString(managementObj["Caption"]).Substring(Convert.ToString(managementObj["Caption"]).IndexOf('W') + 8);
                }
            },
            () =>
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Name, SerialNumber from Win32_BIOS").Get())
                    _INFthisPC[1] = ((string)managementObj["Name"] + ", S/N-" + (string)managementObj["SerialNumber"]);
            },
            () =>
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Manufacturer, Product, Version from Win32_BaseBoard").Get())
                    _INFthisPC[2] = ((string)managementObj["Manufacturer"] + (string)managementObj["Product"] + ", V" + (string)managementObj["Version"]);
            },
            () =>
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Name from Win32_Processor").Get())
                    _INFthisPC[3] = ((string)managementObj["Name"]);
            },
            () =>
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Name, AdapterRAM from Win32_VideoController").Get())
                    _INFthisPC[4] += ((string)managementObj["Name"] + ", " + Convert.ToString(((uint)managementObj["AdapterRAM"] / 1024000000)) + " GB\n");
                _INFthisPC[4] = _INFthisPC[4].TrimEnd('\n');
            },
            () =>
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select  Manufacturer, Capacity, ConfiguredClockSpeed from Win32_PhysicalMemory").Get())
                    _INFthisPC[5] += ((string)managementObj["Manufacturer"] + ", " + Convert.ToString((ulong)managementObj["Capacity"] / 1024000000) + " GB, " + Convert.ToString((uint)managementObj["ConfiguredClockSpeed"]) + "MHz\n");
                _INFthisPC[5] = _INFthisPC[5].TrimEnd('\n');
            },      
            () =>
            {
                try
                {
                    IPHostEntry _host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress _ip4 in _host.AddressList)
                    {
                        if (_ip4.AddressFamily == AddressFamily.InterNetwork)
                        {
                            _INFthisPC[8] = _ip4.ToString();
                        }
                    }
                }
                catch { _INFthisPC[8] = "В системе нет сетевых адаптеров с адресом IPv4"; }
            },
            () =>
            {
                try
                {
                    _INFthisPC[9] =
                        (
                            from nic in NetworkInterface.GetAllNetworkInterfaces()
                            where nic.OperationalStatus == OperationalStatus.Up
                            select nic.GetPhysicalAddress().ToString()
                        ).FirstOrDefault();
                }
                catch { _INFthisPC[9] = "В системе нет сетевых адаптеров с MAC адресом"; }
            },
            () =>
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select name from Win32_NetworkAdapter where NetConnectionStatus=2 or NetConnectionStatus=7").Get())
                    _INFthisPC[10] += (string)managementObj["Name"] + "\n";
                _INFthisPC[10] = _INFthisPC[10].TrimEnd('\n');
            });

            UpdateInormation();
        }

        internal void SetInormationPC(in SystemInfromation _systemInfromation)
        {
            _systemInfromation.NameOS.Text = _INFthisPC[0];
            _systemInfromation.NameBIOS.Text = _INFthisPC[1];
            _systemInfromation.NameMotherBr.Text = _INFthisPC[2];
            _systemInfromation.NameCPU.Text = _INFthisPC[3];
            _systemInfromation.NameGPU.Text = _INFthisPC[4];
            _systemInfromation.NameRAM.Text = _INFthisPC[5];

            _systemInfromation.NameDisk.Text = _INFthisPC[6];
            _systemInfromation.NameSound.Text = _INFthisPC[7];
            _systemInfromation.IpAddress.Text = _ipAddress;
            _systemInfromation.Ipv4.Text = _INFthisPC[8];
            _systemInfromation.MACaddress.Text = _INFthisPC[9];
            _systemInfromation.NameNetAdapter.Text = _INFthisPC[10];
        }

        internal void UpdateInormation()
        {
            Parallel.Invoke(
            () =>
            {
                _INFthisPC[6] = string.Empty;
                foreach (var managementObj in new ManagementObjectSearcher(@"\\.\root\microsoft\windows\storage", "select FriendlyName,MediaType,Size,BusType from MSFT_PhysicalDisk").Get())
                {
                    switch ((ushort)(managementObj["MediaType"]))
                    {
                        case 3:
                            _type = "(HDD)";
                            break;
                        case 4:
                            _type = "(SSD)";
                            break;
                        case 5:
                            _type = "(SCM)";
                            break;
                        default:
                            _type = "(Unspecified)";
                            break;
                    }
                    if (_type == "(Unspecified)" && ((ushort)(managementObj["BusType"])) == 7) _type = "(USB)";
                    _INFthisPC[6] += Convert.ToString((ulong)managementObj["Size"] / 1024000000) + " GB " + "[" + (string)managementObj["FriendlyName"] + "] " + _type + "\n";
                }
                _INFthisPC[6] = _INFthisPC[6].TrimEnd('\n');
            },
            () =>
            {
                _INFthisPC[7] = string.Empty;
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select name from Win32_SoundDevice").Get())
                    _INFthisPC[7] += (string)managementObj["Name"] + "\n";
                _INFthisPC[7] = _INFthisPC[7].TrimEnd('\n');
            });
        }

        #region CheckIntCn/getIp
        private bool CheckInternetConnection()
        {
            try
            {
                HttpWebRequest _request = (HttpWebRequest)WebRequest.Create("http://google.com");
                _request.KeepAlive = false;
                using (HttpWebResponse _response = (HttpWebResponse)_request.GetResponse())
                    return true;
            }
            catch { return false; }
        }

        internal void GetIpUser()
        {
            Parallel.Invoke(() =>
            {
                if (CheckInternetConnection())
                {
                    try
                    {
                        string _exIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
                        var _exlIp = IPAddress.Parse(_exIpString);
                        _ipAddress = _exlIp.ToString();
                    }
                    catch { _ipAddress = "Доступ к сети ограничен"; }
                }
                else
                    _ipAddress = "Отсутствует подключения к интернету";
            });
        }
        #endregion
    }

}

