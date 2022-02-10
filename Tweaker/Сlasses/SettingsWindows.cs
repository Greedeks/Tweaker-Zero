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

            if (_key[0] != null && _key[0].GetValue("Enabled", null) != null && _key[0].GetValue("Enabled").ToString() != "0" || _key[1] != null && _key[1].GetValue("AllowAdvertising", null) != null && _key[1].GetValue("AllowAdvertising").ToString() != "0")
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

            if (_key[2] != null && _key[2].GetValue("Enabled", null) != null && _key[2].GetValue("Enabled").ToString() != "0" || _key[3] != null && _key[3].GetValue("Enabled", null) != null && _key[3].GetValue("Enabled").ToString() != "0" ||
                _key[4] != null && _key[4].GetValue("Enabled", null) != null && _key[4].GetValue("Enabled").ToString() != "0" || _key[5] != null && _key[5].GetValue("Enabled", null) != null && _key[5].GetValue("Enabled").ToString() != "0" ||
                _key[6] != null && _key[6].GetValue("Enabled", null) != null && _key[6].GetValue("Enabled").ToString() != "0" || _key[7] != null && _key[7].GetValue("Enabled", null) != null && _key[7].GetValue("Enabled").ToString() != "0")
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

            if (_key[8] != null && _key[8].GetValue("Start", null) != null && _key[8].GetValue("Start").ToString() != "0" || _key[9] != null && _key[9].GetValue("SaveZoneInformation", null) != null && _key[9].GetValue("SaveZoneInformation").ToString() != "1" ||
                _key[10] != null && _key[10].GetValue("Start", null) != null && _key[10].GetValue("Start").ToString() != "4" || _key[11] != null && _key[11].GetValue("Start", null) != null && _key[11].GetValue("Start").ToString() != "4")
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

            if(_key[12] != null && _key[12].GetValue("DisableInventory", null) != null && _key[12].GetValue("DisableInventory").ToString() != "1")
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

            if(_key[13] != null && _key[13].GetValue("AllowTelemetry", null) != null && _key[13].GetValue("AllowTelemetry").ToString() != "0" || _key[14] != null && _key[14].GetValue("AITEnable", null) != null && _key[14].GetValue("AITEnable").ToString() != "0" ||
            _key[15] != null && _key[15].GetValue("AllowDeviceNameInTelemetry", null) != null && _key[15].GetValue("AllowDeviceNameInTelemetry").ToString() != "0" || _key[16].GetValue("Start_TrackProgs", null) != null && _key[16].GetValue("Start_TrackProgs").ToString() != "0")
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

            if (_key[17] != null && _key[17].GetValue("AllowTelemetry", null) != null && _key[17].GetValue("AllowTelemetry").ToString() != "1" || _key[18] != null && _key[18].GetValue("AllowTelemetry", null) != null && _key[18].GetValue("PreventHandwritingErrorReports").ToString() != "1" ||
            _key[19] != null && _key[19].GetValue("Enabled", null) != null && _key[19].GetValue("Enabled").ToString() != "0")
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

            if (_key[20] != null && _key[20].GetValue("DisableUAR", null) != null && _key[20].GetValue("DisableUAR").ToString() != "1" || _key[21] != null && _key[21].GetValue("NoLockScreenCamera", null) != null && _key[21].GetValue("NoLockScreenCamera").ToString() != "1")
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

            if (_key[22] != null && _key[22].GetValue("DisableLocationScripting", null) != null && _key[22].GetValue("DisableLocationScripting").ToString() != "1" || _key[22] != null && _key[22].GetValue("DisableLocation", null) != null && _key[22].GetValue("DisableLocation").ToString() != "1" ||
            _key[22] != null && _key[22].GetValue("DisableWindowsLocationProvider", null) != null && _key[22].GetValue("DisableWindowsLocationProvider").ToString() != "1")
            {
                confidentiality.TButton9.State = true;
                confidentiality.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton9.State = false;
                confidentiality.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#10
            _key[23] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Siuf\Rules");
            _key[24] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection");

            if (_key[23] != null && _key[23].GetValue("NumberOfSIUFInPeriod", null) != null && _key[23].GetValue("NumberOfSIUFInPeriod").ToString() != "0" || _key[23] != null && _key[23].GetValue("PeriodInNanoSeconds", null) != null && _key[23].GetValue("PeriodInNanoSeconds").ToString() != "0" ||
            _key[24] != null && _key[24].GetValue("DoNotShowFeedbackNotifications", null) != null && _key[24].GetValue("DoNotShowFeedbackNotifications").ToString() != "1")
            {
                confidentiality.TButton10.State = true;
                confidentiality.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton10.State = false;
                confidentiality.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#11
            _key[25] = localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Speech");

            if (_key[25] != null && _key[25].GetValue("AllowSpeechModelUpdate", null) != null && _key[25].GetValue("AllowSpeechModelUpdate").ToString() != "0")
            {
                confidentiality.TButton11.State = true;
                confidentiality.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton11.State = false;
                confidentiality.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#12
            _key[26] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CDPUserSvc");

            if (_key[26] != null && _key[26].GetValue("AllowSpeechModelUpdate", null) != null && _key[26].GetValue("AllowSpeechModelUpdate").ToString() != "4")
            {
                confidentiality.TButton12.State = true;
                confidentiality.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton12.State = false;
                confidentiality.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#13
            _key[27] = localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System");

            if (_key[27] != null && _key[27].GetValue("AllowExperimentation", null) != null && _key[27].GetValue("AllowExperimentation").ToString() != "0")
            {
                confidentiality.TButton13.State = true;
                confidentiality.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton13.State = false;
                confidentiality.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#14
            _key[28] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
            _key[29] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");

            if (_key[28] != null && _key[28].GetValue("Start", null) != null && _key[28].GetValue("Start").ToString() != "4" ||
                _key[29] != null && _key[28].GetValue("Start", null) != null && _key[29].GetValue("Start").ToString() != "4")
            {
                confidentiality.TButton14.State = true;
                confidentiality.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton14.State = false;
                confidentiality.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#15
            _key[30] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service");

            if (_key[30] != null && _key[30].GetValue("Start", null) != null && _key[30].GetValue("Start").ToString() != "4")
            {
                confidentiality.TButton15.State = true;
                confidentiality.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton15.State = false;
                confidentiality.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#16
            _key[31] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer");

            if (_key[31] != null && _key[31].GetValue("Start", null) != null && _key[31].GetValue("Start").ToString() != "4")
            {
                confidentiality.TButton15.State = true;
                confidentiality.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton15.State = false;
                confidentiality.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
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
