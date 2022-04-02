using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
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
                _FullName= (string)managementObj["FullName"];
            if (_FullName != String.Empty) return _FullName;
            else return Environment.UserName.ToLower();
        }

        private static List<string> _INFthisPC = new List<string>(), _idSearch = new List<string>();
        private string _setinfo = default;
        internal void GetInormationPC()
        {
            foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Caption, OSArchitecture, Version from Win32_OperatingSystem").Get())
            {
                string _caption = (string)managementObj["Caption"], _archt = (string)managementObj["OSArchitecture"];
                _INFthisPC.Add(_caption.Substring(_caption.IndexOf('W')) + ", " + System.Text.RegularExpressions.Regex.Replace(_archt, @"\-.+", "-bit") + ", V" +(string)managementObj["Version"]);
            }

            foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Name, SerialNumber from Win32_BIOS").Get())
                _INFthisPC.Add((string)managementObj["Name"] + ", S/N "+(string)managementObj["SerialNumber"]);

            foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Manufacturer, Product, Version from Win32_BaseBoard").Get())
                _INFthisPC.Add((string)managementObj["Manufacturer"]+(string)managementObj["Product"]+", V"+ (string)managementObj["Version"]);

            foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Name from Win32_Processor").Get())
                _INFthisPC.Add((string)managementObj["Name"]);

            foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select DeviceID from Win32_VideoController").Get())
                _idSearch.Add((string)managementObj["DeviceID"]);
            for (int i = 0; i < _idSearch.Count; i++)
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Name, AdapterRAM from Win32_VideoController where DeviceID='" + _idSearch[i] + "'").Get())
                    _setinfo += ((string)managementObj["Name"] + ", " + Convert.ToString(((uint)managementObj["AdapterRAM"] / 1048576000)) + " GB\n");
            }
            _INFthisPC.Add(_setinfo);

            _setinfo = string.Empty;
            _idSearch.Clear();

            foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Tag from Win32_PhysicalMemory").Get())
                _idSearch.Add((string)managementObj["Tag"]);
            for (int i = 0; i < _idSearch.Count; i++)
            {
                foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Manufacturer, Capacity, ConfiguredClockSpeed from Win32_PhysicalMemory where Tag='" + _idSearch[i] + "'").Get())
                    _setinfo += ((string)managementObj["Manufacturer"] + ", " + Convert.ToString((ulong)managementObj["Capacity"] / 1048576000) + " GB, " + Convert.ToString((uint)managementObj["ConfiguredClockSpeed"]) + " MHz\n");
            }
            _INFthisPC.Add(_setinfo);

        }

        internal void SetInormationPC(SystemInfromation systemInfromation)
        {
            systemInfromation.NameOS.Text = _INFthisPC[0];
            systemInfromation.NameBIOS.Text = _INFthisPC[1];
            systemInfromation.NameMotherBr.Text = _INFthisPC[2];
            systemInfromation.NameCPU.Text = _INFthisPC[3];
            systemInfromation.NameGPU.Text = _INFthisPC[4];
            systemInfromation.NameRAM.Text = _INFthisPC[5];
        }
    }
}

