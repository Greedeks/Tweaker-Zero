using Microsoft.Win32;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using Tweaker.Pages;

namespace Tweaker.Сlasses
{
    internal sealed class SettingsWindows
    {
        private readonly RegistryKey _classesRootKey = Registry.ClassesRoot, _currentUserKey = Registry.CurrentUser,
            _localMachineKey = Registry.LocalMachine, _usersKey = Registry.Users,
            _currentConfigKey = Registry.CurrentConfig;  
        private readonly RegistryKey[] _key = new RegistryKey[500];
        private static byte _counTasksConfidentiality = 0;
        private Process _process;

        internal void GetSettingConfidentiality(in Confidentiality _confidentiality)
        {
            //#1
            _key[0] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            _key[1] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth");

            if (_key[0] != null && _key[0].GetValue("Enabled", null) != null && _key[0].GetValue("Enabled").ToString() != "0" || _key[1] != null && _key[1].GetValue("AllowAdvertising", null) != null && _key[1].GetValue("AllowAdvertising").ToString() != "0")
            {
                _confidentiality.TButton1.State = true;
                _confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton1.State = false;
                _confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#2
            _key[2] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings");
            _key[3] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials");
            _key[4] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language");
            _key[5] = _currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization");
            _key[6] = _currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows");
            _key[7] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility");

            if (_key[2] != null && _key[2].GetValue("Enabled", null) != null && _key[2].GetValue("Enabled").ToString() != "0" || _key[3] != null && _key[3].GetValue("Enabled", null) != null && _key[3].GetValue("Enabled").ToString() != "0" ||
                _key[4] != null && _key[4].GetValue("Enabled", null) != null && _key[4].GetValue("Enabled").ToString() != "0" || _key[5] != null && _key[5].GetValue("Enabled", null) != null && _key[5].GetValue("Enabled").ToString() != "0" ||
                _key[6] != null && _key[6].GetValue("Enabled", null) != null && _key[6].GetValue("Enabled").ToString() != "0" || _key[7] != null && _key[7].GetValue("Enabled", null) != null && _key[7].GetValue("Enabled").ToString() != "0")
            {
                _confidentiality.TButton2.State = true;
                _confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton2.State = false;
                _confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#3
            _key[8] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener");
            _key[9] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments");
            _key[10] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
            _key[11] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");

            if (_key[8] != null && _key[8].GetValue("Start", null) != null && _key[8].GetValue("Start").ToString() != "0" || _key[9] != null && _key[9].GetValue("SaveZoneInformation", null) != null && _key[9].GetValue("SaveZoneInformation").ToString() != "1" ||
                _key[10] != null && _key[10].GetValue("Start", null) != null && _key[10].GetValue("Start").ToString() != "4" || _key[11] != null && _key[11].GetValue("Start", null) != null && _key[11].GetValue("Start").ToString() != "4")
            {
                _confidentiality.TButton3.State = true;
                _confidentiality.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton3.State = false;
                _confidentiality.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#4
            if (_counTasksConfidentiality > 0)
            {
                _confidentiality.TButton4.State = true;
                _confidentiality.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton4.State = false;
                _confidentiality.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#5
            _key[12] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");

            if(_key[12] != null && _key[12].GetValue("DisableInventory", null) != null && _key[12].GetValue("DisableInventory").ToString() != "1")
            {
                _confidentiality.TButton5.State = true;
                _confidentiality.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton5.State = false;
                _confidentiality.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#6
            _key[13] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection");
            _key[14] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");
            _key[15] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection");
            _key[16] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

            if(_key[13] != null && _key[13].GetValue("AllowTelemetry", null) != null && _key[13].GetValue("AllowTelemetry").ToString() != "0" || _key[14] != null && _key[14].GetValue("AITEnable", null) != null && _key[14].GetValue("AITEnable").ToString() != "0" ||
            _key[15] != null && _key[15].GetValue("AllowDeviceNameInTelemetry", null) != null && _key[15].GetValue("AllowDeviceNameInTelemetry").ToString() != "0" || _key[16].GetValue("Start_TrackProgs", null) != null && _key[16].GetValue("Start_TrackProgs").ToString() != "0")
            {
                _confidentiality.TButton6.State = true;
                _confidentiality.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton6.State = false;
                _confidentiality.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#7
            _key[17] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC");
            _key[18] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports");
            _key[19] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Input\TIPC");

            if (_key[17] != null && _key[17].GetValue("AllowTelemetry", null) != null && _key[17].GetValue("AllowTelemetry").ToString() != "1" || _key[18] != null && _key[18].GetValue("PreventHandwritingErrorReports", null) != null && _key[18].GetValue("PreventHandwritingErrorReports").ToString() != "1" ||
            _key[19] != null && _key[19].GetValue("Enabled", null) != null && _key[19].GetValue("Enabled").ToString() != "0")
            {
                _confidentiality.TButton7.State = true;
                _confidentiality.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton7.State = false;
                _confidentiality.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#8
            _key[20] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");
            _key[21] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization");

            if (_key[20] != null && _key[20].GetValue("DisableUAR", null) != null && _key[20].GetValue("DisableUAR").ToString() != "1" || _key[21] != null && _key[21].GetValue("NoLockScreenCamera", null) != null && _key[21].GetValue("NoLockScreenCamera").ToString() != "1")
            {
                _confidentiality.TButton8.State = true;
                _confidentiality.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton8.State = false;
                _confidentiality.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#9
            _key[22] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors");

            if (_key[22] != null && _key[22].GetValue("DisableLocationScripting", null) != null && _key[22].GetValue("DisableLocationScripting").ToString() != "1" || _key[22] != null && _key[22].GetValue("DisableLocation", null) != null && _key[22].GetValue("DisableLocation").ToString() != "1" ||
            _key[22] != null && _key[22].GetValue("DisableWindowsLocationProvider", null) != null && _key[22].GetValue("DisableWindowsLocationProvider").ToString() != "1")
            {
                _confidentiality.TButton9.State = true;
                _confidentiality.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton9.State = false;
                _confidentiality.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#10
            _key[23] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Siuf\Rules");
            _key[24] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection");

            if (_key[23] != null && _key[23].GetValue("NumberOfSIUFInPeriod", null) != null && _key[23].GetValue("NumberOfSIUFInPeriod").ToString() != "0" || _key[23] != null && _key[23].GetValue("PeriodInNanoSeconds", null) != null && _key[23].GetValue("PeriodInNanoSeconds").ToString() != "0" ||
            _key[24] != null && _key[24].GetValue("DoNotShowFeedbackNotifications", null) != null && _key[24].GetValue("DoNotShowFeedbackNotifications").ToString() != "1")
            {
                _confidentiality.TButton10.State = true;
                _confidentiality.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton10.State = false;
                _confidentiality.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#11
            _key[25] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Speech");

            if (_key[25] != null && _key[25].GetValue("AllowSpeechModelUpdate", null) != null && _key[25].GetValue("AllowSpeechModelUpdate").ToString() != "0")
            {
                _confidentiality.TButton11.State = true;
                _confidentiality.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton11.State = false;
                _confidentiality.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#12
            _key[26] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CDPUserSvc");

            if (_key[26] != null && _key[26].GetValue("AllowSpeechModelUpdate", null) != null && _key[26].GetValue("AllowSpeechModelUpdate").ToString() != "4")
            {
                _confidentiality.TButton12.State = true;
                _confidentiality.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton12.State = false;
                _confidentiality.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#13
            _key[27] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System");

            if (_key[27] != null && _key[27].GetValue("AllowExperimentation", null) != null && _key[27].GetValue("AllowExperimentation").ToString() != "0")
            {
                _confidentiality.TButton13.State = true;
                _confidentiality.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton13.State = false;
                _confidentiality.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#14
            _key[28] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
            _key[29] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");

            if (_key[28] != null && _key[28].GetValue("Start", null) != null && _key[28].GetValue("Start").ToString() != "4" ||
                _key[29] != null && _key[28].GetValue("Start", null) != null && _key[29].GetValue("Start").ToString() != "4")
            {
                _confidentiality.TButton14.State = true;
                _confidentiality.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton14.State = false;
                _confidentiality.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#15
            _key[30] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service");

            if (_key[30] != null && _key[30].GetValue("Start", null) != null && _key[30].GetValue("Start").ToString() != "4")
            {
                _confidentiality.TButton15.State = true;
                _confidentiality.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton15.State = false;
                _confidentiality.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#16
            _key[31] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer");

            if (_key[31] != null && _key[31].GetValue("Start", null) != null && _key[31].GetValue("Start").ToString() != "4")
            {
                _confidentiality.TButton15.State = true;
                _confidentiality.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton15.State = false;
                _confidentiality.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        internal void TaskCheckStateConfidentiality()
        {
            string[] TaskName = new string[12] {@"""Microsoft\Windows\Maintenance\WinSAT""", @"""Microsoft\Windows\Autochk\Proxy""", @"""Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser""",
                @"""Microsoft\Windows\Application Experience\ProgramDataUpdater""", @"""Microsoft\Windows\Application Experience\StartupAppTask""", @"""Microsoft\Windows\PI\Sqm-Tasks""",
                @"""Microsoft\Windows\NetTrace\GatherNetworkInfo""", @"""Microsoft\Windows\Customer Experience Improvement Program\Consolidator""", @"""Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask""",
                @"""Microsoft\Windows\Customer Experience Improvement Program\UsbCeip""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticResolver""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticDataCollector"""};

            _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.FileName = "cmd.exe";
            foreach (var _task in TaskName)
            {
                _process.StartInfo.Arguments = string.Format("/c chcp 65001 & schtasks /tn {0}", _task);
                _process.Start();
                _process.StandardOutput.ReadLine();
                string _tbl = _process.StandardOutput.ReadToEnd();
                if (_tbl.Split('A').Last().Trim() == "Ready")
                    _counTasksConfidentiality++;
            }
            _process.WaitForExit();
            _process.Dispose();
        }

        internal void GetSettingInterface(in Interface _interface)
        {
            //#1
            _key[32] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[32] != null && _key[32].GetValue("MenuShowDelay", null) != null && _key[32].GetValue("MenuShowDelay").ToString() != "20")
            {
                _interface.TButton1.State = true;
                _interface.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton1.State = false;
                _interface.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#2

            //#3

            //#4
            _key[33] = _usersKey.OpenSubKey(@".DEFAULT\Control Panel\Colors");
            _key[34] = _usersKey.OpenSubKey(@"S-1-5-19\Control Panel\Colors");
            _key[35] = _usersKey.OpenSubKey(@"S-1-5-20\Control Panel\Colors");

            if (_key[33] != null && _key[33].GetValue("InfoWindow", null) != null && _key[33].GetValue("InfoWindow").ToString() != "246 253 255" || _key[34] != null && _key[34].GetValue("InfoWindow", null) != null && _key[34].GetValue("InfoWindow").ToString() != "246 253 255" ||
            _key[35] != null && _key[35].GetValue("InfoWindow", null) != null && _key[35].GetValue("InfoWindow").ToString() != "246 253 255")
            {
                _interface.TButton4.State = true;
                _interface.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton4.State = false;
                _interface.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#5
            _key[36] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[36] != null && _key[36].GetValue("CaptionHeight", null) != null && _key[36].GetValue("CaptionHeight").ToString() != "-270" || 
                _key[36] != null && _key[36].GetValue("CaptionWidth", null) != null && _key[36].GetValue("CaptionWidth").ToString() != "-270")
            {
                _interface.TButton5.State = true;
                _interface.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton5.State = false;
                _interface.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#6

            //#7

            //#8

            //#9

            //#10
            _key[37] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[37] != null && _key[37].GetValue("CursorBlinkRate", null) != null && _key[37].GetValue("CursorBlinkRate").ToString() != "250")
            {
                _interface.TButton10.State = true;
                _interface.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton10.State = false;
                _interface.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#11
            _key[38] = _currentUserKey.OpenSubKey(@"Control Panel\Mouse");

            if (_key[38] != null && _key[38].GetValue("MouseHoverTime", null) != null && _key[38].GetValue("MouseHoverTime").ToString() != "20")
            {
                _interface.TButton11.State = true;
                _interface.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton11.State = false;
                _interface.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#12
            _key[39] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer");

            if (_key[39] != null && _key[39].GetValue("EnableAutoTray", null) != null && _key[39].GetValue("EnableAutoTray").ToString() != "0")
            {
                _interface.TButton12.State = true;
                _interface.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton12.State = false;
                _interface.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#13
            _key[40] = _currentUserKey.OpenSubKey(@"Software\Policies\Microsoft\Windows\Explorer");

            if (_key[40] != null && _key[40].GetValue("DisableNotificationCenter", null) != null && _key[40].GetValue("DisableNotificationCenter").ToString() != "1")
            {
                _interface.TButton13.State = true;
                _interface.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton13.State = false;
                _interface.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#14
            _key[41] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop\WindowMetrics");

            if (_key[41] != null && _key[41].GetValue("ScrollHeight", null) != null && _key[41].GetValue("ScrollHeight").ToString() != "-210" ||
                _key[41] != null && _key[41].GetValue("ScrollWidth", null) != null && _key[41].GetValue("ScrollWidth").ToString() != "-210")
            {
                _interface.TButton14.State = true;
                _interface.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton14.State = false;
                _interface.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#15
            _key[42] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel");

            if (_key[42] != null && _key[42].GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", null) != null && _key[42].GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}").ToString() != "0")
            {
                _interface.TButton15.State = true;
                _interface.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton15.State = false;
                _interface.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#16
            _key[43] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

            if (_key[43] != null && _key[43].GetValue("PersistBrowsers", null) != null && _key[43].GetValue("PersistBrowsers").ToString() != "1")
            {
                _interface.TButton16.State = true;
                _interface.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton16.State = false;
                _interface.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#17
            _key[44] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");
            _key[45] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search");

            if (_key[44] != null && _key[44].GetValue("ShowTaskViewButton", null) != null && _key[44].GetValue("ShowTaskViewButton").ToString() != "0" || _key[44] != null && _key[44].GetValue("TaskbarMn", null) != null && _key[44].GetValue("TaskbarMn").ToString() != "0" 
                || _key[44] != null && _key[44].GetValue("TaskbarDa", null) != null && _key[44].GetValue("TaskbarDa").ToString() != "0" || _key[45] != null && _key[45].GetValue("SearchboxTaskbarMode", null) != null && _key[45].GetValue("SearchboxTaskbarMode").ToString() != "0")
            {
                _interface.TButton17.State = true;
                _interface.Tweak17.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton17.State = false;
                _interface.Tweak17.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#18

            //#19

            //#20

        }
        
        internal void AppWidgetsState(in bool _choose)
        {
            if(_choose)
                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Dsh").SetValue("AllowNewsAndInterests", 1, RegistryValueKind.DWord);
            else
                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Dsh").SetValue("AllowNewsAndInterests", 0, RegistryValueKind.DWord);
        }
    }
}
