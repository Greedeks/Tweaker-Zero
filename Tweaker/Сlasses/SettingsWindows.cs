using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using Tweaker.Pages;

namespace Tweaker.Сlasses
{
    internal class SettingsWindows
    {
        private readonly RegistryKey classesRootKey = Registry.ClassesRoot, currentUserKey = Registry.CurrentUser,
            localMachineKey = Registry.LocalMachine, usersKey = Registry.Users,
            currentConfigKey = Registry.CurrentConfig;  
        private readonly RegistryKey[] _key = new RegistryKey[100];
        private byte _counTasks = 0;

        internal void GetSettingConfidentiality(Confidentiality confidentiality)
        {
            //#1
            _key[0] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            _key[1] = localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth");

            if (_key[0] != null && _key[0].GetValue("Enabled").ToString() != "0" || _key[1] != null && _key[1].GetValue("AllowAdvertising").ToString() != "0")
            {
                confidentiality.TButton1.State = true;
                confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton1.State = false;
                confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#2
            _key[2] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings");
            _key[3] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials");
            _key[4] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language");
            _key[5] = currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization");
            _key[6] = currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows");
            _key[7] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility");

            if (_key[2] != null && _key[2].GetValue("Enabled").ToString() != "0" || _key[3] != null && _key[3].GetValue("Enabled").ToString() != "0" ||
                _key[4] != null && _key[4].GetValue("Enabled").ToString() != "0" || _key[5] != null && _key[5].GetValue("Enabled").ToString() != "0" ||
                _key[6] != null && _key[6].GetValue("Enabled").ToString() != "0" || _key[7] != null && _key[7].GetValue("Enabled").ToString() != "0")
            {
                confidentiality.TButton2.State = true;
                confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton2.State = false;
                confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#3
            _key[8] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener");
            _key[9] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments");
            _key[10] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
            _key[11] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");

            if (_key[8] != null && _key[8].GetValue("Start").ToString() != "0" || _key[9] != null && _key[9].GetValue("SaveZoneInformation").ToString() != "1" || 
                _key[10] != null && _key[10].GetValue("Start").ToString() != "4" || _key[11] != null && _key[11].GetValue("Start").ToString() != "4")
            {
                confidentiality.TButton3.State = true;
                confidentiality.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton3.State = false;
                confidentiality.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#4
            TaskCheckState(@"""Microsoft\Windows\Maintenance\WinSAT""", @"""Microsoft\Windows\Autochk\Proxy""", @"""Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser""", 
                @"""Microsoft\Windows\Application Experience\ProgramDataUpdater""", @"""Microsoft\Windows\Application Experience\StartupAppTask""", @"""Microsoft\Windows\PI\Sqm-Tasks""",
                @"""Microsoft\Windows\NetTrace\GatherNetworkInfo""", @"""Microsoft\Windows\Customer Experience Improvement Program\Consolidator""", @"""Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask""", 
                @"""Microsoft\Windows\Customer Experience Improvement Program\UsbCeip""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticResolver""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticDataCollector""");

            if (_counTasks>0)
            {
                confidentiality.TButton4.State = true;
                confidentiality.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton4.State = false;
                confidentiality.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#5
            _key[12] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");

            if(_key[12] != null && _key[12].GetValue("DisableInventory").ToString() != "1")
            {
                confidentiality.TButton5.State = true;
                confidentiality.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton5.State = false;
                confidentiality.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#6
            _key[13] = localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection");
            _key[14] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");
            _key[15] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection");
            _key[16] = currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

            if(_key[13] != null && _key[13].GetValue("AllowTelemetry").ToString() != "0" || _key[14] != null && _key[14].GetValue("AITEnable").ToString() != "0" ||
            _key[15] != null && _key[15].GetValue("AllowDeviceNameInTelemetry").ToString() != "0" || _key[16] != null && _key[16].GetValue("Start_TrackProgs").ToString() != "0")
            {
                confidentiality.TButton6.State = true;
                confidentiality.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton6.State = false;
                confidentiality.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#7
            _key[17] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC");
            _key[18] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports");
            _key[19] = currentUserKey.OpenSubKey(@"Software\Microsoft\Input\TIPC");

            if (_key[17] != null && _key[17].GetValue("PreventHandwritingDataSharing").ToString() != "1" || _key[18] != null && _key[18].GetValue("PreventHandwritingErrorReports").ToString() != "1" ||
            _key[19] != null && _key[19].GetValue("Enabled").ToString() != "0")
            {
                confidentiality.TButton7.State = true;
                confidentiality.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton7.State = false;
                confidentiality.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#8
            _key[20] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");
            _key[21] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization");

            if (_key[20] != null && _key[20].GetValue("DisableUAR").ToString() != "1" || _key[21] != null && _key[21].GetValue("NoLockScreenCamera").ToString() != "1")
            {
                confidentiality.TButton8.State = true;
                confidentiality.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton8.State = false;
                confidentiality.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#9
            _key[22] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors");

            //if (_key[22] != null && _key[22].GetValue("DisableLocationScripting").ToString() != "1" || _key[22] != null && _key[22].GetValue("DisableLocation").ToString() != "1" ||
            //_key[22] != null && _key[22].GetValue("DisableWindowsLocationProvider").ToString() != "1")
            //{
            //    confidentiality.TButton9.State = true;
            //    confidentiality.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            //}
            //else
            //{
            //    confidentiality.TButton9.State = false;
            //    confidentiality.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            //}

            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation", null) != null && Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation", null).ToString() != "1")
            {
                confidentiality.TButton9.State = true;
                confidentiality.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
        }

        private void TaskCheckState(params string[] TaskName)
        {
            Process process = Process.Start(new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "schtasks.exe",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.GetEncoding(866),
                WindowStyle = ProcessWindowStyle.Hidden
            });
            _counTasks = 0;
            for (int i = 0; i < TaskName.Length; i++)
            {
                 process.StartInfo.Arguments = String.Format("/TN {0}", TaskName[i]);
                 process.Start();
                 process.StandardOutput.ReadLine();
                 string tbl = process.StandardOutput.ReadToEnd();
                 process.WaitForExit();
                 if (tbl.Split('A').Last().Trim() == "Ready" || tbl.Split('A').Last().Trim() == "Готово")
                    _counTasks++;
            }
        }
    }
}
