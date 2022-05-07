using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static byte _countTasksConfidentiality = default;
        private Process _process;
        private BackgroundWorker _worker;
        private string _state = default;

        #region Confidentiality
        internal void GetSettingConfidentiality(Confidentiality _confidentiality)
        {
            //#1
            _key[0] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            _key[1] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth");

            if (_key[0] == null || _key[0].GetValue("Enabled", null) == null || _key[0].GetValue("Enabled").ToString() != "0" || _key[1] == null || _key[1].GetValue("AllowAdvertising", null) == null || _key[1].GetValue("AllowAdvertising").ToString() != "0")
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

            if (_key[2] == null || _key[2].GetValue("Enabled", null) == null || _key[2].GetValue("Enabled").ToString() != "0" || _key[3] == null || _key[3].GetValue("Enabled", null) == null || _key[3].GetValue("Enabled").ToString() != "0" ||
                _key[4] == null || _key[4].GetValue("Enabled", null) == null || _key[4].GetValue("Enabled").ToString() != "0" || _key[5] == null || _key[5].GetValue("Enabled", null) == null || _key[5].GetValue("Enabled").ToString() != "0" ||
                _key[6] == null || _key[6].GetValue("Enabled", null) == null || _key[6].GetValue("Enabled").ToString() != "0" || _key[7] == null || _key[7].GetValue("Enabled", null) == null || _key[7].GetValue("Enabled").ToString() != "0")
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

            if (_key[8] == null || _key[8].GetValue("Start", null) == null || _key[8].GetValue("Start").ToString() != "0" || _key[9] == null || _key[9].GetValue("SaveZoneInformation", null) == null || _key[9].GetValue("SaveZoneInformation").ToString() != "1")
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
            if (_countTasksConfidentiality > 0)
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

            if (_key[12] == null || _key[12].GetValue("DisableInventory", null) == null || _key[12].GetValue("DisableInventory").ToString() != "1")
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

            if (_key[13] == null || _key[13].GetValue("AllowTelemetry", null) == null || _key[13].GetValue("AllowTelemetry").ToString() != "0" || _key[14] == null || _key[14].GetValue("AITEnable", null) == null || _key[14].GetValue("AITEnable").ToString() != "0" ||
            _key[15] == null || _key[15].GetValue("AllowTelemetry", null) == null || _key[15].GetValue("AllowTelemetry").ToString() != "0" || _key[16].GetValue("Start_TrackProgs", null) == null || _key[16].GetValue("Start_TrackProgs").ToString() != "0")
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

            if (_key[17] == null || _key[17].GetValue("PreventHandwritingDataSharing", null) == null || _key[17].GetValue("PreventHandwritingDataSharing").ToString() != "1" || _key[18] == null || _key[18].GetValue("PreventHandwritingErrorReports", null) == null || _key[18].GetValue("PreventHandwritingErrorReports").ToString() != "1" ||
            _key[19] == null || _key[19].GetValue("Enabled", null) == null || _key[19].GetValue("Enabled").ToString() != "0")
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

            if (_key[20] == null || _key[20].GetValue("DisableUAR", null) == null || _key[20].GetValue("DisableUAR").ToString() != "1" || _key[21] == null || _key[21].GetValue("NoLockScreenCamera", null) == null || _key[21].GetValue("NoLockScreenCamera").ToString() != "1")
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

            if (_key[22] == null || _key[22].GetValue("DisableLocationScripting", null) == null || _key[22].GetValue("DisableLocationScripting").ToString() != "1" || _key[22] == null || _key[22].GetValue("DisableLocation", null) == null || _key[22].GetValue("DisableLocation").ToString() != "1" ||
            _key[22] == null || _key[22].GetValue("DisableWindowsLocationProvider", null) == null || _key[22].GetValue("DisableWindowsLocationProvider").ToString() != "1")
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

            if (_key[23] == null || _key[23].GetValue("NumberOfSIUFInPeriod", null) == null || _key[23].GetValue("NumberOfSIUFInPeriod").ToString() != "0" || _key[23] == null || _key[23].GetValue("PeriodInNanoSeconds", null) == null || _key[23].GetValue("PeriodInNanoSeconds").ToString() != "0" ||
            _key[24] == null || _key[24].GetValue("DoNotShowFeedbackNotifications", null) == null || _key[24].GetValue("DoNotShowFeedbackNotifications").ToString() != "1")
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

            if (_key[25] == null || _key[25].GetValue("AllowSpeechModelUpdate", null) == null || _key[25].GetValue("AllowSpeechModelUpdate").ToString() != "0")
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

            if (_key[26] == null || _key[26].GetValue("Start", null) == null || _key[26].GetValue("Start").ToString() != "4")
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

            if (_key[27] == null || _key[27].GetValue("AllowExperimentation", null) == null || _key[27].GetValue("AllowExperimentation").ToString() != "0")
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

            if (_key[28] != null || _key[29] != null)
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

            if (_key[30] == null || _key[30].GetValue("Start", null) == null || _key[30].GetValue("Start").ToString() != "4")
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

            if (_key[31] == null || _key[31].GetValue("Start", null) == null || _key[31].GetValue("Start").ToString() != "4")
            {
                _confidentiality.TButton16.State = true;
                _confidentiality.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentiality.TButton16.State = false;
                _confidentiality.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
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
            _countTasksConfidentiality = 0;
            foreach (var _task in TaskName)
            {
                _process.StartInfo.Arguments = string.Format("/c chcp 65001 & schtasks /tn {0}", _task);
                _process.Start();
                _process.StandardOutput.ReadLine();
                string _tbl = _process.StandardOutput.ReadToEnd();
                if (_tbl.Split('A').Last().Trim() == "Ready")
                    _countTasksConfidentiality++;
            }
            _process.Dispose();
        }

        internal void ChangeSettingConfidentiality(in bool _choose, in byte _select)
        {
            try
            {
                switch (_select)
                {
                    case 1:
                        {
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth").SetValue("AllowAdvertising", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", true).DeleteValue("Enabled");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", true).DeleteValue("AllowAdvertising");
                            }
                            break;
                        }
                    case 2:
                        {
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility").SetValue("Enabled", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", true).DeleteValue("Enabled");
                            }
                            break;
                        }
                    case 3:
                        {
                            string _state = default;
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener").SetValue("Start", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments").SetValue("SaveZoneInformation", 1, RegistryValueKind.DWord);
                                _state = "/disable";
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener").SetValue("Start", 1, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments").DeleteValue("SaveZoneInformation");
                                _state = "/enable";
                            }

                            string[] TaskName = new string[8] { @"""Microsoft\Office\Office ClickToRun Service Monitor""", @"""Microsoft\Office\OfficeTelemetry\AgentFallBack2016""", @"""Microsoft\Office\OfficeTelemetry\OfficeTelemetryAgentLogOn2016""",
                         @"""Microsoft\Office\OfficeTelemetryAgentFallBack2016""", @"""Microsoft\Office\OfficeTelemetryAgentLogOn2016""", @"""Microsoft\Office\OfficeTelemetryAgentFallBack""",
                         @"""Microsoft\Office\OfficeTelemetryAgentLogOn""", @"""Microsoft\Office\Office 15 Subscription Heartbeat""",};

                            _process = new Process();
                            _process.StartInfo.UseShellExecute = false;
                            _process.StartInfo.RedirectStandardOutput = true;
                            _process.StartInfo.CreateNoWindow = true;
                            _process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            _process.StartInfo.FileName = "cmd.exe";
                            foreach (string _taskName in TaskName)
                            {
                                Parallel.Invoke(() =>
                                {
                                    _process.StartInfo.Arguments = string.Format(@"/c schtasks /change /tn {0} {1}", _taskName, _state);
                                    _process.Start();
                                });
                            }
                            _process.Dispose();
                            break;
                        }
                    case 4:
                        {
                            _worker = new BackgroundWorker();
                            _worker.DoWork += Worker_DoWorkTaskConfidentiality; ;
                            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                            if (_choose)
                            {
                                _state = "/disable";
                                _worker.RunWorkerAsync();
                                _countTasksConfidentiality = 0;
                            }
                            else
                            {
                                _state = "/enable";
                                _worker.RunWorkerAsync();
                                _countTasksConfidentiality = 2;
                            }
                            break;
                        }
                    case 5:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat").SetValue("DisableInventory", 1, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true).DeleteValue("DisableInventory");
                            break;
                        }
                    case 6:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection").SetValue("AllowTelemetry", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat").SetValue("AITEnable", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection").SetValue("AllowTelemetry", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("Start_TrackProgs", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection").SetValue("AllowTelemetry", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat").SetValue("AITEnable", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection").SetValue("AllowTelemetry", 1, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("Start_TrackProgs", 1, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 7:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC").SetValue("PreventHandwritingDataSharing", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports").SetValue("PreventHandwritingErrorReports", 1, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Input\TIPC").SetValue("Enabled", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC").DeleteValue("PreventHandwritingDataSharing");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports").SetValue("PreventHandwritingErrorReports", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Input\TIPC").SetValue("Enabled", 1, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 8:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat").SetValue("DisableUAR", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization").SetValue("NoLockScreenCamera", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true).DeleteValue("DisableUAR");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization", true).DeleteValue("NoLockScreenCamera");
                            }
                            break;
                        }
                    case 9:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors").SetValue("DisableLocation", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors").SetValue("DisableLocationScripting", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors").SetValue("DisableWindowsLocationProvider", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).DeleteValue("DisableLocation");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).DeleteValue("DisableLocationScripting");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).DeleteValue("DisableWindowsLocationProvider");
                            }
                            break;
                        }
                    case 10:
                        {
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Siuf\Rules").SetValue("NumberOfSIUFInPeriod", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Siuf\Rules").SetValue("PeriodInNanoSeconds", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection").SetValue("DoNotShowFeedbackNotifications", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Siuf\Rules", true).DeleteValue("NumberOfSIUFInPeriod");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Siuf\Rules", true).DeleteValue("PeriodInNanoSeconds");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection", true).DeleteValue("DoNotShowFeedbackNotifications");
                            }
                            break;
                        }
                    case 11:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Speech").SetValue("AllowSpeechModelUpdate", 0, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Speech", true).DeleteValue("AllowSpeechModelUpdate");
                            break;
                        }
                    case 12:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\CDPUserSvc").SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\CDPUserSvc").SetValue("Start", 2, RegistryValueKind.DWord);
                            break;
                        }
                    case 13:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System").SetValue("AllowExperimentation", 0, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System", true).DeleteValue("AllowExperimentation");
                            break;
                        }
                    case 14:
                        {
                            if (_choose)
                            {
                                _localMachineKey.DeleteSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
                                _localMachineKey.DeleteSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");
                            }
                            break;
                        }
                    case 15:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service").SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service").SetValue("Start", 3, RegistryValueKind.DWord);
                            break;
                        }
                    case 16:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer").SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer").SetValue("Start", 2, RegistryValueKind.DWord);
                            break;
                        }
                }
            }
            catch { };
        }

        private void Worker_DoWorkTaskConfidentiality(object sender, DoWorkEventArgs e)
        {
            string[] TaskName = new string[12] {@"""Microsoft\Windows\Maintenance\WinSAT""", @"""Microsoft\Windows\Autochk\Proxy""", @"""Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser""",
                @"""Microsoft\Windows\Application Experience\ProgramDataUpdater""", @"""Microsoft\Windows\Application Experience\StartupAppTask""", @"""Microsoft\Windows\PI\Sqm-Tasks""",
                @"""Microsoft\Windows\NetTrace\GatherNetworkInfo""", @"""Microsoft\Windows\Customer Experience Improvement Program\Consolidator""", @"""Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask""",
                @"""Microsoft\Windows\Customer Experience Improvement Program\UsbCeip""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticResolver""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticDataCollector"""};

            _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            _process.StartInfo.FileName = "cmd.exe";
            foreach (string _taskName in TaskName)
            {
                Parallel.Invoke(() => {
                _process.StartInfo.Arguments = string.Format(@"/c schtasks /change /tn {0} {1}", _taskName, _state);
                _process.Start();
                });
            }
            _process.Dispose();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _worker.Dispose();
        }
        #endregion

        #region Interface
        internal void GetSettingInterface(in Interface _interface)
        {
            //#1
            _key[32] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[32] == null || _key[32].GetValue("MenuShowDelay", null) == null || _key[32].GetValue("MenuShowDelay").ToString() != "20")
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
            //Отправить (поделиться)
            _key[32] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\*\shellex\ContextMenuHandlers\ModernSharing");
            //Передать на устройство
            _key[33] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\CLSID\{7AD84985-87B4-4a16-BE58-8B72A5B390F7}");
            //Изменить Фото
            _key[34] = _classesRootKey.OpenSubKey(@"AppX43hnxtbyyps62jhe9sqpdzxn1790zetc\Shell\ShellEdit");
            //Добавить в библиотеку
            _key[35] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\Folder\shellex\ContextMenuHandlers\Library Location");
            //Поиск музыки в Интернете
            _key[36] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\Directory.Audio\shellex\ContextMenuHandlers\WMPShopMusic");
            //Изменить в Pain3D
            _key[37] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.3ds\Shell\3D Edit");
            _key[38] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.3ds\Shell\3D Print");
            _key[39] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.3mf\Shell\3D Edit");
            _key[40] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.3mf\Shell\3D Print");
            _key[41] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.bmp\Shell\3D Edit");
            _key[42] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.dae\Shell\3D Print");
            _key[43] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.dxf\Shell\3D Print");
            _key[44] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.fbx\Shell\3D Edit");
            _key[45] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.gif\Shell\3D Edit");
            _key[46] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.glb\Shell\3D Edit");
            _key[47] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jfif\Shell\3D Edit");
            _key[48] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpe\Shell\3D Edit");
            _key[49] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpeg\Shell\3D Edit");
            _key[50] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpg\Shell\3D Edit");
            _key[51] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.obj\Shell\3D Edit");
            _key[52] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.obj\Shell\3D Print");
            _key[53] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.ply\Shell\3D Edit");
            _key[54] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.ply\Shell\3D Print");
            _key[55] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.png\Shell\3D Edit");
            _key[56] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.stl\Shell\3D Edit");
            _key[57] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.stl\Shell\3D Print");
            _key[58] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.tif\Shell\3D Edit");
            _key[59] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.tiff\Shell\3D Edit");
            _key[60] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.wrl\Shell\3D Edit");
            _key[61] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.wrl\Shell\3D Print");

            if (_key[32] != null || _key[33] != null || _key[34] == null || _key[34].GetValue("ProgrammaticAccessOnly", null) == null ||
               _key[35] != null || _key[36] != null || _key[37] != null || _key[38] != null || _key[39] != null || _key[40] != null || _key[41] != null
               || _key[42] != null || _key[43] != null || _key[44] != null || _key[45] != null || _key[46] != null || _key[47] != null || _key[48] != null
               || _key[49] != null || _key[50] != null || _key[51] != null || _key[52] != null || _key[53] != null || _key[54] != null || _key[55] != null || _key[56] != null
               || _key[57] != null || _key[58] != null || _key[59] != null || _key[60] != null || _key[61] != null)
            {
                _interface.TButton2.State = true;
                _interface.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton2.State = false;
                _interface.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#3
            _key[62] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\.bmp\ShellNew]");
            _key[63] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\.contact\ShellNew");
            _key[64] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\.rtf\ShellNew");

            if (_key[62] != null || _key[63] != null || _key[64] != null)
            {
                _interface.TButton3.State = true;
                _interface.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton3.State = false;
                _interface.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#4
            _key[65] = _usersKey.OpenSubKey(@".DEFAULT\Control Panel\Colors");
            _key[66] = _usersKey.OpenSubKey(@"S-1-5-19\Control Panel\Colors");
            _key[67] = _usersKey.OpenSubKey(@"S-1-5-20\Control Panel\Colors");

            if (_key[65] == null || _key[65].GetValue("InfoWindow", null) == null || _key[65].GetValue("InfoWindow").ToString() != "246 253 255" || _key[66] == null || _key[66].GetValue("InfoWindow", null) == null || _key[66].GetValue("InfoWindow").ToString() != "246 253 255" ||
            _key[67] == null || _key[67].GetValue("InfoWindow", null) == null || _key[67].GetValue("InfoWindow").ToString() != "246 253 255")
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
            _key[68] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[68] == null || _key[68].GetValue("CaptionHeight", null) == null || _key[68].GetValue("CaptionHeight").ToString() != "-270" ||
                _key[68] == null || _key[68].GetValue("CaptionWidth", null) == null || _key[68].GetValue("CaptionWidth").ToString() != "-270")
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
            _key[69] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
            _key[70] = _localMachineKey.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");

            if (_key[69] != null || _key[70] != null)
            {
                _interface.TButton6.State = true;
                _interface.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton6.State = false;
                _interface.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#7
            _key[71] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{088e3905-0323-4b02-9826-5d99428e115f}");
            _key[72] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}");
            _key[73] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}");
            _key[74] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");
            _key[75] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{d3162b92-9365-467a-956b-92703aca08af}");
            _key[76] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}");

            if (_key[71] != null || _key[72] != null || _key[73] != null || _key[74] != null || _key[75] != null || _key[76] != null)
            {
                _interface.TButton7.State = true;
                _interface.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton7.State = false;
                _interface.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#8
            _key[77] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons");

            if (_key[77] == null || _key[77].GetValue("20", null) == null || _key[77].GetValue("29").ToString() != @"%systemroot%\\Blank.ico,0")
            {
                _interface.TButton8.State = true;
                _interface.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton8.State = false;
                _interface.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#9
            _key[78] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer");

            if (_key[78] == null || _key[78].GetValue("link", null) == null || _key[78].GetValue("link").ToString() != "00 00 00 00")
            {
                _interface.TButton9.State = true;
                _interface.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton9.State = false;
                _interface.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#10
            _key[79] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[79] == null || _key[79].GetValue("CursorBlinkRate", null) == null || _key[79].GetValue("CursorBlinkRate").ToString() != "250")
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
            _key[80] = _currentUserKey.OpenSubKey(@"Control Panel\Mouse");

            if (_key[80] == null || _key[80].GetValue("MouseHoverTime", null) == null || _key[80].GetValue("MouseHoverTime").ToString() != "20")
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
            _key[81] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer");

            if (_key[81] == null || _key[81].GetValue("EnableAutoTray", null) == null || _key[81].GetValue("EnableAutoTray").ToString() != "0")
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
            _key[82] = _currentUserKey.OpenSubKey(@"Software\Policies\Microsoft\Windows\Explorer");

            if (_key[82] == null || _key[82].GetValue("DisableNotificationCenter", null) == null || _key[82].GetValue("DisableNotificationCenter").ToString() != "1")
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
            _key[83] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop\WindowMetrics");

            if (_key[83] == null || _key[83].GetValue("ScrollHeight", null) == null || _key[83].GetValue("ScrollHeight").ToString() != "-210" ||
                _key[83] == null || _key[83].GetValue("ScrollWidth", null) == null || _key[83].GetValue("ScrollWidth").ToString() != "-210")
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
            _key[84] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel");

            if (_key[84] == null || _key[84].GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", null) == null || _key[84].GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}").ToString() != "0")
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
            _key[85] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

            if (_key[85] == null || _key[85].GetValue("PersistBrowsers", null) == null || _key[85].GetValue("PersistBrowsers").ToString() != "1")
            {
                _interface.TButton16.State = true;
                _interface.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interface.TButton16.State = false;
                _interface.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "11")
            {
                //#17
                _key[86] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");
                _key[87] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search");

                if (_key[86] == null || _key[86].GetValue("ShowTaskViewButton", null) == null || _key[86].GetValue("ShowTaskViewButton").ToString() != "0" || _key[86] == null || _key[86].GetValue("TaskbarMn", null) == null || _key[86].GetValue("TaskbarMn").ToString() != "0"
                    || _key[86] == null || _key[86].GetValue("TaskbarDa", null) == null || _key[86].GetValue("TaskbarDa").ToString() != "0" || _key[87] == null || _key[87].GetValue("SearchboxTaskbarMode", null) == null || _key[87].GetValue("SearchboxTaskbarMode").ToString() != "0")
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
                _key[88] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
                _key[89] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Privacy");
                _key[90] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager");

                if (_key[88] == null || _key[88].GetValue("Enabled", null) == null || _key[88].GetValue("Enabled").ToString() != "0" || _key[89] == null || _key[89].GetValue("TailoredExperiencesWithDiagnosticDataEnabled", null) == null || _key[89].GetValue("TailoredExperiencesWithDiagnosticDataEnabled").ToString() != "0" ||
                    _key[90] == null || _key[90].GetValue("RotatingLockScreenEnabled", null) == null || _key[90].GetValue("RotatingLockScreenEnabled").ToString() != "0" || _key[90] == null || _key[90].GetValue("RotatingLockScreenOverlayEnabled", null) == null || _key[90].GetValue("RotatingLockScreenOverlayEnabled").ToString() != "0" ||
                    _key[90] == null || _key[90].GetValue("SubscribedContent-338387Enabled", null) == null || _key[88].GetValue("SubscribedContent-338387Enabled").ToString() != "0")
                {
                    _interface.TButton18.State = true;
                    _interface.Tweak18.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _interface.TButton18.State = false;
                    _interface.Tweak18.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }

                //#19
                _key[91] = _currentUserKey.OpenSubKey(@"Software\Classes\CLSID\{86ca1aa0‑34aa‑4e8b‑a509‑50c905bae2a2}\InprocServer32");

                if (_key[91] == null || _key[91].GetValue("InprocServer32", null) == null)
                {
                    _interface.TButton19.State = true;
                    _interface.Tweak19.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _interface.TButton19.State = false;
                    _interface.Tweak19.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }

                //#20
                _key[92] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager");
                _key[93] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\UserProfileEngagement");

                if (_key[92] == null || _key[92].GetValue("SoftLandingEnabled", null) == null || _key[92].GetValue("SoftLandingEnabled").ToString() != "0" ||
                    _key[93] == null || _key[93].GetValue("ScoobeSystemSettingEnabled ", null) == null || _key[93].GetValue("ScoobeSystemSettingEnabled ").ToString() != "0")
                {
                    _interface.TButton20.State = true;
                    _interface.Tweak20.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _interface.TButton20.State = false;
                    _interface.Tweak20.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }
            }
        }

        internal void ChangeSettingInterface(in bool _choose, in byte _select)
        {
            try
            {
                switch (_select)
                {
                    case 1:
                        {
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth").SetValue("AllowAdvertising", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", true).DeleteValue("Enabled");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", true).DeleteValue("AllowAdvertising");
                            }
                            break;
                        }
                    case 2:
                        {
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility").SetValue("Enabled", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", true).DeleteValue("Enabled");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", true).DeleteValue("Enabled");
                            }
                            break;
                        }
                    case 3:
                        {
                            string _state = default;
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener").SetValue("Start", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments").SetValue("SaveZoneInformation", 1, RegistryValueKind.DWord);
                                _state = "/disable";
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener").SetValue("Start", 1, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments").DeleteValue("SaveZoneInformation");
                                _state = "/enable";
                            }

                            string[] TaskName = new string[8] { @"""Microsoft\Office\Office ClickToRun Service Monitor""", @"""Microsoft\Office\OfficeTelemetry\AgentFallBack2016""", @"""Microsoft\Office\OfficeTelemetry\OfficeTelemetryAgentLogOn2016""",
                         @"""Microsoft\Office\OfficeTelemetryAgentFallBack2016""", @"""Microsoft\Office\OfficeTelemetryAgentLogOn2016""", @"""Microsoft\Office\OfficeTelemetryAgentFallBack""",
                         @"""Microsoft\Office\OfficeTelemetryAgentLogOn""", @"""Microsoft\Office\Office 15 Subscription Heartbeat""",};

                            _process = new Process();
                            _process.StartInfo.UseShellExecute = false;
                            _process.StartInfo.RedirectStandardOutput = true;
                            _process.StartInfo.CreateNoWindow = true;
                            _process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            _process.StartInfo.FileName = "cmd.exe";
                            foreach (string _taskName in TaskName)
                            {
                                Parallel.Invoke(() =>
                                {
                                    _process.StartInfo.Arguments = string.Format(@"/c schtasks /change /tn {0} {1}", _taskName, _state);
                                    _process.Start();
                                });
                            }
                            _process.Dispose();
                            break;
                        }
                    case 4:
                        {
                            _worker = new BackgroundWorker();
                            _worker.DoWork += Worker_DoWorkTaskConfidentiality; ;
                            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                            if (_choose)
                            {
                                _state = "/disable";
                                _worker.RunWorkerAsync();
                                _countTasksConfidentiality = 0;
                            }
                            else
                            {
                                _state = "/enable";
                                _worker.RunWorkerAsync();
                                _countTasksConfidentiality = 2;
                            }
                            break;
                        }
                    case 5:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat").SetValue("DisableInventory", 1, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true).DeleteValue("DisableInventory");
                            break;
                        }
                    case 6:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection").SetValue("AllowTelemetry", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat").SetValue("AITEnable", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection").SetValue("AllowTelemetry", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("Start_TrackProgs", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection").SetValue("AllowTelemetry", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat").SetValue("AITEnable", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection").SetValue("AllowTelemetry", 1, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("Start_TrackProgs", 1, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 7:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC").SetValue("PreventHandwritingDataSharing", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports").SetValue("PreventHandwritingErrorReports", 1, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Input\TIPC").SetValue("Enabled", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC").DeleteValue("PreventHandwritingDataSharing");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports").SetValue("PreventHandwritingErrorReports", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Input\TIPC").SetValue("Enabled", 1, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 8:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat").SetValue("DisableUAR", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization").SetValue("NoLockScreenCamera", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat", true).DeleteValue("DisableUAR");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization", true).DeleteValue("NoLockScreenCamera");
                            }
                            break;
                        }
                    case 9:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors").SetValue("DisableLocation", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors").SetValue("DisableLocationScripting", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors").SetValue("DisableWindowsLocationProvider", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).DeleteValue("DisableLocation");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).DeleteValue("DisableLocationScripting");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).DeleteValue("DisableWindowsLocationProvider");
                            }
                            break;
                        }
                    case 10:
                        {
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Siuf\Rules").SetValue("NumberOfSIUFInPeriod", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Siuf\Rules").SetValue("PeriodInNanoSeconds", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection").SetValue("DoNotShowFeedbackNotifications", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Siuf\Rules", true).DeleteValue("NumberOfSIUFInPeriod");
                                _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Siuf\Rules", true).DeleteValue("PeriodInNanoSeconds");
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection", true).DeleteValue("DoNotShowFeedbackNotifications");
                            }
                            break;
                        }
                    case 11:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Speech").SetValue("AllowSpeechModelUpdate", 0, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Speech", true).DeleteValue("AllowSpeechModelUpdate");
                            break;
                        }
                    case 12:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\CDPUserSvc").SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\CDPUserSvc").SetValue("Start", 2, RegistryValueKind.DWord);
                            break;
                        }
                    case 13:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System").SetValue("AllowExperimentation", 0, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System", true).DeleteValue("AllowExperimentation");
                            break;
                        }
                    case 14:
                        {
                            if (_choose)
                            {
                                _localMachineKey.DeleteSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
                                _localMachineKey.DeleteSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");
                            }
                            break;
                        }
                    case 15:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service").SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service").SetValue("Start", 3, RegistryValueKind.DWord);
                            break;
                        }
                    case 16:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer").SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer").SetValue("Start", 2, RegistryValueKind.DWord);
                            break;
                        }
                }
            }
            catch { };
        }
        #endregion

        #region Applications
        internal void AppWidgetsState(in bool _choose)
        {
            try
            {
                if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "11")
                {
                    if (_choose)
                        _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Dsh").SetValue("AllowNewsAndInterests", 0, RegistryValueKind.DWord);
                    else
                        _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Dsh", true).DeleteValue("AllowNewsAndInterests");
                }
            }
            catch { }
        }

        internal byte AppOneDriveCheck()
        {
            if (_classesRootKey.OpenSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}") == null && _classesRootKey.OpenSubKey(@"Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}") == null)
                return 0;
            else

                return 1;
        }

        internal void AppCortana(in bool _choose)
        {

            if (_choose)
            {
                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Speech_OneCore\Preferences").SetValue("ModelDownloadAllowed", 0, RegistryValueKind.DWord);
                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search").SetValue("AllowCloudSearch", 0, RegistryValueKind.DWord);
                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search").SetValue("AllowCortana", 0, RegistryValueKind.DWord);
                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search").SetValue("AllowSearchToUseLocation", 0, RegistryValueKind.DWord);
                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search").SetValue("ConnectedSearchUseWeb", 0, RegistryValueKind.DWord);
                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search").SetValue("DisableWebSearch", 1, RegistryValueKind.DWord);
                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search").SetValue("AllowNewsAndInterests", 0, RegistryValueKind.DWord);
                _currentUserKey.CreateSubKey(@"Software\Microsoft\InputPersonalization").SetValue("RestrictImplicitInkCollection", 1, RegistryValueKind.DWord);
                _currentUserKey.CreateSubKey(@"Software\Microsoft\InputPersonalization").SetValue("RestrictImplicitTextCollection", 1, RegistryValueKind.DWord);
                _currentUserKey.CreateSubKey(@"Software\Microsoft\InputPersonalization\TrainedDataStore").SetValue("HarvestContacts", 0, RegistryValueKind.DWord);
                _currentUserKey.CreateSubKey(@"Software\Microsoft\Personalization\Settings").SetValue("AcceptedPrivacyPolicy", 0, RegistryValueKind.DWord);
                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Windows Search]").SetValue("CortanaConsent", 0, RegistryValueKind.DWord);
            }
            else
            {
                _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Speech_OneCore\Preferences", true).DeleteValue("ModelDownloadAllowed");
                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true).DeleteValue("AllowCloudSearch");
                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true).DeleteValue("AllowCortana");
                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true).DeleteValue("AllowSearchToUseLocation");
                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true).DeleteValue("ConnectedSearchUseWeb");
                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true).DeleteValue("DisableWebSearch");
                _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Search", true).DeleteValue("AllowNewsAndInterests");
                _currentUserKey.OpenSubKey(@"Software\Microsoft\InputPersonalization", true).DeleteValue("RestrictImplicitInkCollection");
                _currentUserKey.OpenSubKey(@"Software\Microsoft\InputPersonalization", true).DeleteValue("RestrictImplicitTextCollection");
                _currentUserKey.OpenSubKey(@"Software\Microsoft\InputPersonalization\TrainedDataStore").DeleteValue("HarvestContacts");
                _currentUserKey.OpenSubKey(@"Software\Microsoft\Personalization\Settings", true).DeleteValue("AcceptedPrivacyPolicy");
                _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Windows Search]", true).DeleteValue("CortanaConsent");
            }


        }

        internal void AppOneDrive(bool _choose)
        {
            Parallel.Invoke(() =>
            {
                if (_choose)
                {
                    RegistryKey _keyOneDrive = _classesRootKey.OpenSubKey(@"CLSID", true);
                    _keyOneDrive.DeleteSubKeyTree(@"{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                    _keyOneDrive = _classesRootKey.OpenSubKey(@"Wow6432Node\CLSID", true);
                    _keyOneDrive.DeleteSubKeyTree(@"{018D5C66-4533-4307-9B53-224DE2ED1FE6}");

                    string[] _onedrive = new string[6] { @"taskkill /f /im OneDrive.exe", @"%systemroot%\System32\OneDriveSetup.exe /uninstall", @"%systemroot%\SysWOW64\OneDriveSetup.exe /uninstall", @"rd /s /q %userprofile%\OneDrive", @"rd /s /q %userprofile%\AppData\Local\Microsoft\OneDrive", @"rd /s /q "" % allusersprofile %\Microsoft OneDrive""" };
                    _process = new Process();
                    _process.StartInfo.UseShellExecute = false;
                    _process.StartInfo.RedirectStandardOutput = true;
                    _process.StartInfo.CreateNoWindow = true;
                    _process.StartInfo.FileName = "powershell.exe";
                    foreach (var _setcommand in _onedrive)
                    {
                        _process.StartInfo.Arguments = string.Format("cmd /c {0}", _setcommand);
                        _process.Start();
                    }
                    _process.Dispose();
                }
                else
                {
                    _classesRootKey.CreateSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                    _classesRootKey.CreateSubKey(@"Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}");


                    string[] _onedrive = new string[2] { @"%systemroot%\System32\OneDriveSetup.exe", @"%systemroot%\SysWOW64\OneDriveSetup.exe" };
                    _process = new Process();
                    _process.StartInfo.UseShellExecute = false;
                    _process.StartInfo.RedirectStandardOutput = true;
                    _process.StartInfo.CreateNoWindow = true;
                    _process.StartInfo.FileName = "powershell.exe";
                    foreach (var _setcommand in _onedrive)
                    {
                        _process.StartInfo.Arguments = string.Format("cmd /c {0}", _setcommand);
                        _process.Start();
                    }
                    _process.Dispose();
                }
            });
        }
        #endregion
    }
}
