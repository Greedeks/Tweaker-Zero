﻿using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tweaker.Pages;

namespace Tweaker.Сlasses
{
    internal sealed class SettingsWindows
    {
        private readonly ToastNotification toastNotification = new ToastNotification();
        private readonly RegistryKey _classesRootKey = Registry.ClassesRoot, _currentUserKey = Registry.CurrentUser,
            _localMachineKey = Registry.LocalMachine, _usersKey = Registry.Users;
        private readonly RegistryKey[] _key = new RegistryKey[238];
        private static byte _countTasksConfidentiality = default, _countTaskSystem = default, _countProtocolSystem = default;
        internal static byte _verificationW = default;
        private BackgroundWorker _worker;
        private string _state = default;

        private const uint SPI_SETMOUSESPEED = 0x0071;
        private const uint SPI_SETKEYBOARDDELAY = 0x0017;
        private const uint SPI_SETKEYBOARDSPEED = 0x000B;

        #region Confidentiality
        internal void GetSettingConfidentiality(in ConfidentialityPage _confidentialityPage)
        {
            //#1
            _key[0] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            _key[1] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth");

            if (_key[0] == null || _key[0].GetValue("Enabled", null) == null || _key[0].GetValue("Enabled").ToString() != "0" || _key[1] == null || _key[1].GetValue("AllowAdvertising", null) == null || _key[1].GetValue("AllowAdvertising").ToString() != "0")
            {
                _confidentialityPage.TButton1.State = true;
                _confidentialityPage.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton1.State = false;
                _confidentialityPage.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
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
                _confidentialityPage.TButton2.State = true;
                _confidentialityPage.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton2.State = false;
                _confidentialityPage.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#3
            _key[8] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener");
            _key[9] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments");

            if (_key[8] == null || _key[8].GetValue("Start", null) == null || _key[8].GetValue("Start").ToString() != "0" || _key[9] == null || _key[9].GetValue("SaveZoneInformation", null) == null || _key[9].GetValue("SaveZoneInformation").ToString() != "1")
            {
                _confidentialityPage.TButton3.State = true;
                _confidentialityPage.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton3.State = false;
                _confidentialityPage.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#4
            if (_countTasksConfidentiality > 0)
            {
                _confidentialityPage.TButton4.State = true;
                _confidentialityPage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton4.State = false;
                _confidentialityPage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#5
            _key[12] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");

            if (_key[12] == null || _key[12].GetValue("DisableInventory", null) == null || _key[12].GetValue("DisableInventory").ToString() != "1")
            {
                _confidentialityPage.TButton5.State = true;
                _confidentialityPage.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton5.State = false;
                _confidentialityPage.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#6
            _key[13] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection");
            _key[14] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");
            _key[15] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection");
            _key[16] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

            if (_key[13] == null || _key[13].GetValue("AllowTelemetry", null) == null || _key[13].GetValue("AllowTelemetry").ToString() != "0" || _key[14] == null || _key[14].GetValue("AITEnable", null) == null || _key[14].GetValue("AITEnable").ToString() != "0" ||
            _key[15] == null || _key[15].GetValue("AllowTelemetry", null) == null || _key[15].GetValue("AllowTelemetry").ToString() != "0" || _key[16].GetValue("Start_TrackProgs", null) == null || _key[16].GetValue("Start_TrackProgs").ToString() != "0")
            {
                _confidentialityPage.TButton6.State = true;
                _confidentialityPage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton6.State = false;
                _confidentialityPage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#7
            _key[17] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\TabletPC");
            _key[18] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports");
            _key[19] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Input\TIPC");

            if (_key[17] == null || _key[17].GetValue("PreventHandwritingDataSharing", null) == null || _key[17].GetValue("PreventHandwritingDataSharing").ToString() != "1" || _key[18] == null || _key[18].GetValue("PreventHandwritingErrorReports", null) == null || _key[18].GetValue("PreventHandwritingErrorReports").ToString() != "1" ||
            _key[19] == null || _key[19].GetValue("Enabled", null) == null || _key[19].GetValue("Enabled").ToString() != "0")
            {
                _confidentialityPage.TButton7.State = true;
                _confidentialityPage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton7.State = false;
                _confidentialityPage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#8
            _key[20] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\AppCompat");
            _key[21] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Personalization");

            if (_key[20] == null || _key[20].GetValue("DisableUAR", null) == null || _key[20].GetValue("DisableUAR").ToString() != "1" || _key[21] == null || _key[21].GetValue("NoLockScreenCamera", null) == null || _key[21].GetValue("NoLockScreenCamera").ToString() != "1")
            {
                _confidentialityPage.TButton8.State = true;
                _confidentialityPage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton8.State = false;
                _confidentialityPage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#9
            _key[22] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors");

            if (_key[22] == null || _key[22].GetValue("DisableLocationScripting", null) == null || _key[22].GetValue("DisableLocationScripting").ToString() != "1" || _key[22] == null || _key[22].GetValue("DisableLocation", null) == null || _key[22].GetValue("DisableLocation").ToString() != "1" ||
            _key[22] == null || _key[22].GetValue("DisableWindowsLocationProvider", null) == null || _key[22].GetValue("DisableWindowsLocationProvider").ToString() != "1")
            {
                _confidentialityPage.TButton9.State = true;
                _confidentialityPage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton9.State = false;
                _confidentialityPage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#10
            _key[23] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Siuf\Rules");
            _key[24] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\DataCollection");

            if (_key[23] == null || _key[23].GetValue("NumberOfSIUFInPeriod", null) == null || _key[23].GetValue("NumberOfSIUFInPeriod").ToString() != "0" || _key[23] == null || _key[23].GetValue("PeriodInNanoSeconds", null) == null || _key[23].GetValue("PeriodInNanoSeconds").ToString() != "0" ||
            _key[24] == null || _key[24].GetValue("DoNotShowFeedbackNotifications", null) == null || _key[24].GetValue("DoNotShowFeedbackNotifications").ToString() != "1")
            {
                _confidentialityPage.TButton10.State = true;
                _confidentialityPage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton10.State = false;
                _confidentialityPage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#11
            _key[25] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Speech");

            if (_key[25] == null || _key[25].GetValue("AllowSpeechModelUpdate", null) == null || _key[25].GetValue("AllowSpeechModelUpdate").ToString() != "0")
            {
                _confidentialityPage.TButton11.State = true;
                _confidentialityPage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton11.State = false;
                _confidentialityPage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#12
            _key[26] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CDPUserSvc");

            if (_key[26] == null || _key[26].GetValue("Start", null) == null || _key[26].GetValue("Start").ToString() != "4")
            {
                _confidentialityPage.TButton12.State = true;
                _confidentialityPage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton12.State = false;
                _confidentialityPage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#13
            _key[27] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\System");

            if (_key[27] == null || _key[27].GetValue("AllowExperimentation", null) == null || _key[27].GetValue("AllowExperimentation").ToString() != "0")
            {
                _confidentialityPage.TButton13.State = true;
                _confidentialityPage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton13.State = false;
                _confidentialityPage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#14
            _key[28] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
            _key[29] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");

            if (_key[28] != null || _key[29] != null)
            {
                _confidentialityPage.TButton14.State = true;
                _confidentialityPage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton14.State = false;
                _confidentialityPage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#15
            _key[30] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service");

            if (_key[30] == null || _key[30].GetValue("Start", null) == null || _key[30].GetValue("Start").ToString() != "4")
            {
                _confidentialityPage.TButton15.State = true;
                _confidentialityPage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton15.State = false;
                _confidentialityPage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#16
            _key[31] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer");

            if (_key[31] == null || _key[31].GetValue("Start", null) == null || _key[31].GetValue("Start").ToString() != "4")
            {
                _confidentialityPage.TButton16.State = true;
                _confidentialityPage.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _confidentialityPage.TButton16.State = false;
                _confidentialityPage.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        internal void TaskCheckStateConfidentiality()
        {
            string[] TaskName = new string[12] {@"""Microsoft\Windows\Maintenance\WinSAT""", @"""Microsoft\Windows\Autochk\Proxy""", @"""Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser""",
                @"""Microsoft\Windows\Application Experience\ProgramDataUpdater""", @"""Microsoft\Windows\Application Experience\StartupAppTask""", @"""Microsoft\Windows\PI\Sqm-Tasks""",
                @"""Microsoft\Windows\NetTrace\GatherNetworkInfo""", @"""Microsoft\Windows\Customer Experience Improvement Program\Consolidator""", @"""Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask""",
                @"""Microsoft\Windows\Customer Experience Improvement Program\UsbCeip""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticResolver""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticDataCollector"""};

            Process _process = new Process();
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
                if (_tbl.Contains("Ready"))
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

                            Process _process = new Process();
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
                            _worker.DoWork += Worker_DoWorkTaskConfidentiality;
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
                                RegistryKey _modl = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services", true);
                                _modl.DeleteSubKeyTree(@"DiagTrack");
                                _modl.DeleteSubKeyTree(@"dmwappushservice");
                                _modl.Close();
                            }
                            else
                            {
                                RegistryKey _modl = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services", true);
                                _modl.CreateSubKey(@"DiagTrack");
                                _modl.CreateSubKey(@"dmwappushservice");
                                _modl.Close();
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
                            string[] NvTelemetry = new string[6];
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer").SetValue("Start", 4, RegistryValueKind.DWord);
                                NvTelemetry = new string[6] { "schtasks /change /tn NvTmRepOnLogon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /disable", "schtasks /change /tn NvTmRep_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /disable", "schtasks /change /tn NvTmMon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /disable",
                            "net stop NvTelemetryContainer", "sc config NvTelemetryContainer start= disabled", "sc stop NvTelemetryContainer"};
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\NvTelemetryContainer").SetValue("Start", 2, RegistryValueKind.DWord);
                                NvTelemetry = new string[6] { "schtasks /change /tn NvTmRepOnLogon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /enable", "schtasks /change /tn NvTmRep_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /enable", "schtasks /change /tn NvTmMon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /enable",
                            "net start NvTelemetryContainer", "sc config NvTelemetryContainer start= auto", "sc start NvTelemetryContainer"};
                            }

                            Process _process = new Process();
                            _process.StartInfo.UseShellExecute = false;
                            _process.StartInfo.RedirectStandardOutput = true;
                            _process.StartInfo.CreateNoWindow = true;
                            _process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            _process.StartInfo.FileName = "cmd.exe";
                            foreach (string _nvtelemetry in NvTelemetry)
                            {
                                Parallel.Invoke(() =>
                                {
                                    _process.StartInfo.Arguments = string.Format(@"/c {0}", _nvtelemetry);
                                    _process.Start();
                                });
                            }
                            _process.Dispose();
                            break;
                        }
                }
            }
            catch { };
        }

        private void Worker_DoWorkTaskConfidentiality(object sender, DoWorkEventArgs e)
        {
            try
            {
                string[] TaskName = new string[12] {@"""Microsoft\Windows\Maintenance\WinSAT""", @"""Microsoft\Windows\Autochk\Proxy""", @"""Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser""",
                @"""Microsoft\Windows\Application Experience\ProgramDataUpdater""", @"""Microsoft\Windows\Application Experience\StartupAppTask""", @"""Microsoft\Windows\PI\Sqm-Tasks""",
                @"""Microsoft\Windows\NetTrace\GatherNetworkInfo""", @"""Microsoft\Windows\Customer Experience Improvement Program\Consolidator""", @"""Microsoft\Windows\Customer Experience Improvement Program\KernelCeipTask""",
                @"""Microsoft\Windows\Customer Experience Improvement Program\UsbCeip""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticResolver""", @"""Microsoft\Windows\DiskDiagnostic\Microsoft-Windows-DiskDiagnosticDataCollector"""};

                Process _process = new Process();
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
            }
            catch { }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => _worker.Dispose();
 
        #endregion

        #region Interface
        internal void GetSettingInterface(in InterfacePage _interfacePage)
        {
            //#1
            _key[32] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[32] == null || _key[32].GetValue("MenuShowDelay", null) == null || _key[32].GetValue("MenuShowDelay").ToString() != "20")
            {
                _interfacePage.TButton1.State = true;
                _interfacePage.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton1.State = false;
                _interfacePage.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
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
                _interfacePage.TButton2.State = true;
                _interfacePage.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton2.State = false;
                _interfacePage.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#3
            _key[62] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\.bmp\ShellNew");
            _key[63] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\.contact\ShellNew");
            _key[64] = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\.rtf\ShellNew");

            if (_key[62] != null || _key[63] != null || _key[64] != null)
            {
                _interfacePage.TButton3.State = true;
                _interfacePage.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton3.State = false;
                _interfacePage.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#4
            _key[65] = _usersKey.OpenSubKey(@".DEFAULT\Control Panel\Colors");
            _key[66] = _usersKey.OpenSubKey(@"S-1-5-19\Control Panel\Colors");
            _key[67] = _usersKey.OpenSubKey(@"S-1-5-20\Control Panel\Colors");

            if (_key[65] == null || _key[65].GetValue("InfoWindow", null) == null || _key[65].GetValue("InfoWindow").ToString() != "246 253 255" || _key[66] == null || _key[66].GetValue("InfoWindow", null) == null || _key[66].GetValue("InfoWindow").ToString() != "246 253 255" ||
            _key[67] == null || _key[67].GetValue("InfoWindow", null) == null || _key[67].GetValue("InfoWindow").ToString() != "246 253 255")
            {
                _interfacePage.TButton4.State = true;
                _interfacePage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton4.State = false;
                _interfacePage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#5
            _key[68] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop\WindowMetrics");

            if (_key[68] == null || _key[68].GetValue("CaptionHeight", null) == null || _key[68].GetValue("CaptionHeight").ToString() != "-270" ||
                _key[68] == null || _key[68].GetValue("CaptionWidth", null) == null || _key[68].GetValue("CaptionWidth").ToString() != "-270")
            {
                _interfacePage.TButton5.State = true;
                _interfacePage.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton5.State = false;
                _interfacePage.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#6
            _key[69] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
            _key[70] = _localMachineKey.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");

            if (_key[69] != null || _key[70] != null)
            {
                _interfacePage.TButton6.State = true;
                _interfacePage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton6.State = false;
                _interfacePage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
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
                _interfacePage.TButton7.State = true;
                _interfacePage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton7.State = false;
                _interfacePage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#8
            _key[77] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons");

            if (_key[77] == null || _key[77].GetValue("29", null) == null || _key[77].GetValue("29").ToString() != @"%systemroot%\\Blank.ico,0")
            {
                _interfacePage.TButton8.State = true;
                _interfacePage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton8.State = false;
                _interfacePage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#9
            _key[78] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer");

            if (_key[78] == null || _key[78].GetValue("link", null) == null)
            {
                _interfacePage.TButton9.State = true;
                _interfacePage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton9.State = false;
                _interfacePage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#10
            _key[79] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[79] == null || _key[79].GetValue("CursorBlinkRate", null) == null || _key[79].GetValue("CursorBlinkRate").ToString() != "250")
            {
                _interfacePage.TButton10.State = true;
                _interfacePage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton10.State = false;
                _interfacePage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#11
            _key[80] = _currentUserKey.OpenSubKey(@"Control Panel\Mouse");

            if (_key[80] == null || _key[80].GetValue("MouseHoverTime", null) == null || _key[80].GetValue("MouseHoverTime").ToString() != "20")
            {
                _interfacePage.TButton11.State = true;
                _interfacePage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton11.State = false;
                _interfacePage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#12
            _key[81] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel");

            if (_key[81] == null || _key[81].GetValue("{645FF040-5081-101B-9F08-00AA002F954E}", null) == null || _key[81].GetValue("{645FF040-5081-101B-9F08-00AA002F954E}").ToString() != "1")
            {
                _interfacePage.TButton12.State = true;
                _interfacePage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton12.State = false;
                _interfacePage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#13
            _key[82] = _currentUserKey.OpenSubKey(@"Software\Policies\Microsoft\Windows\Explorer");

            if (_key[82] == null || _key[82].GetValue("DisableNotificationCenter", null) == null || _key[82].GetValue("DisableNotificationCenter").ToString() != "1")
            {
                _interfacePage.TButton13.State = true;
                _interfacePage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton13.State = false;
                _interfacePage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#14
            _key[83] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop\WindowMetrics");

            if (_key[83] == null || _key[83].GetValue("ScrollHeight", null) == null || _key[83].GetValue("ScrollHeight").ToString() != "-210" ||
                _key[83] == null || _key[83].GetValue("ScrollWidth", null) == null || _key[83].GetValue("ScrollWidth").ToString() != "-210")
            {
                _interfacePage.TButton14.State = true;
                _interfacePage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton14.State = false;
                _interfacePage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#15
            _key[84] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel");

            if (_key[84] == null || _key[84].GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", null) == null || _key[84].GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}").ToString() != "0")
            {
                _interfacePage.TButton15.State = true;
                _interfacePage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton15.State = false;
                _interfacePage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#16
            _key[85] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

            if (_key[85] == null || _key[85].GetValue("PersistBrowsers", null) == null || _key[85].GetValue("PersistBrowsers").ToString() != "1")
            {
                _interfacePage.TButton16.State = true;
                _interfacePage.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _interfacePage.TButton16.State = false;
                _interfacePage.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "11")
            {
                //#17
                _key[86] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");
                _key[87] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search");

                if (_key[86] == null || _key[86].GetValue("ShowTaskViewButton", null) == null || _key[86].GetValue("ShowTaskViewButton").ToString() != "0" || _key[86] == null || _key[86].GetValue("TaskbarMn", null) == null || _key[86].GetValue("TaskbarMn").ToString() != "0"
                    || _key[86] == null || _key[86].GetValue("TaskbarDa", null) == null || _key[86].GetValue("TaskbarDa").ToString() != "0" || _key[87] == null || _key[87].GetValue("SearchboxTaskbarMode", null) == null || _key[87].GetValue("SearchboxTaskbarMode").ToString() != "0")
                {
                    _interfacePage.TButton17.State = true;
                    _interfacePage.Tweak17.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _interfacePage.TButton17.State = false;
                    _interfacePage.Tweak17.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }

                //#18
                _key[88] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
                _key[89] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Privacy");
                _key[90] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager");

                if (_key[88] == null || _key[88].GetValue("Enabled", null) == null || _key[88].GetValue("Enabled").ToString() != "0" || _key[89] == null || _key[89].GetValue("TailoredExperiencesWithDiagnosticDataEnabled", null) == null || _key[89].GetValue("TailoredExperiencesWithDiagnosticDataEnabled").ToString() != "0" ||
                    _key[90] == null || _key[90].GetValue("RotatingLockScreenEnabled", null) == null || _key[90].GetValue("RotatingLockScreenEnabled").ToString() != "0" || _key[90] == null || _key[90].GetValue("RotatingLockScreenOverlayEnabled", null) == null || _key[90].GetValue("RotatingLockScreenOverlayEnabled").ToString() != "0" ||
                    _key[90] == null || _key[90].GetValue("SubscribedContent-338387Enabled", null) == null || _key[90].GetValue("SubscribedContent-338387Enabled").ToString() != "0")
                {
                    _interfacePage.TButton18.State = true;
                    _interfacePage.Tweak18.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _interfacePage.TButton18.State = false;
                    _interfacePage.Tweak18.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }

                //#19
                _key[91] = _currentUserKey.OpenSubKey(@"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32");

                if (_key[91] == null)
                {
                    _interfacePage.TButton19.State = true;
                    _interfacePage.Tweak19.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _interfacePage.TButton19.State = false;
                    _interfacePage.Tweak19.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }

                //#20
                _key[92] = _currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager");
                _key[93] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\UserProfileEngagement");

                if (_key[92] == null || _key[92].GetValue("SoftLandingEnabled", null) == null || _key[92].GetValue("SoftLandingEnabled").ToString() != "0" ||
                    _key[93] == null || _key[93].GetValue("ScoobeSystemSettingEnabled ", null) == null || _key[93].GetValue("ScoobeSystemSettingEnabled ").ToString() != "0")
                {
                    _interfacePage.TButton20.State = true;
                    _interfacePage.Tweak20.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _interfacePage.TButton20.State = false;
                    _interfacePage.Tweak20.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
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
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").SetValue("MenuShowDelay", 20, RegistryValueKind.String);
                            else
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").SetValue("MenuShowDelay", 400, RegistryValueKind.String);
                            break;
                        }
                    case 2:
                        {
                            if (_choose)
                            {
                                RegistryKey _photo = _classesRootKey.OpenSubKey(@"AppX43hnxtbyyps62jhe9sqpdzxn1790zetc\Shell\ShellEdit", true);
                                RegistrySecurity regsec = _photo.GetAccessControl();
                                _photo.SetAccessControl(regsec);
                                _photo.SetValue("ProgrammaticAccessOnly", "", RegistryValueKind.String);
                                _photo.Close();

                                RegistryKey _share = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\*\shellex\ContextMenuHandlers", true);
                                _share.DeleteSubKeyTree(@"ModernSharing");
                                _share.Close();

                                RegistryKey _send = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\CLSID", RegistryKeyPermissionCheck.ReadWriteSubTree);
                                _send.DeleteSubKeyTree(@"{7AD84985-87B4-4a16-BE58-8B72A5B390F7}");
                                _send.Close();

                                RegistryKey _lib = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\Folder\shellex\ContextMenuHandlers", true);
                                _lib.DeleteSubKeyTree(@"Library Location");
                                _lib.Close();

                                RegistryKey _mus = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\Directory.Audio\shellex\ContextMenuHandlers", true);
                                _mus.DeleteSubKeyTree(@"WMPShopMusic");
                                _mus.Close();

                                RegistryKey _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.3ds\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.DeleteSubKeyTree(@"3D Print");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.3mf\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.DeleteSubKeyTree(@"3D Print");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.bmp\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.dae\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Print");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.dxf\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Print");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.fbx\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.gif\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.glb\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jfif\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpe\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpeg\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpg\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.obj\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.DeleteSubKeyTree(@"3D Print");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.ply\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.DeleteSubKeyTree(@"3D Print");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.png\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.stl\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.DeleteSubKeyTree(@"3D Print");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.tif\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.tiff\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.wrl\Shell", true);
                                _3d.DeleteSubKeyTree(@"3D Edit");
                                _3d.DeleteSubKeyTree(@"3D Print");
                                _3d.Close();

                            }
                            else
                            {
                                RegistryKey _share = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\*\shellex\ContextMenuHandlers", true);
                                _share.CreateSubKey(@"ModernSharing").SetValue("", "{e2bf9676-5f8f-435c-97eb-11607a5bedf7}", RegistryValueKind.String);
                                _share.Close();

                                RegistryKey _send = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\CLSID", true);
                                _send.CreateSubKey(@"{7AD84985-87B4-4a16-BE58-8B72A5B390F7}").SetValue("", "Play To menu", RegistryValueKind.String);
                                _send.CreateSubKey(@"{7AD84985-87B4-4a16-BE58-8B72A5B390F7}").SetValue("ContextMenuOptIn", "", RegistryValueKind.String);
                                _send.Close();

                                _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\CLSID\{7AD84985-87B4-4a16-BE58-8B72A5B390F7}", true);

                                RegistryKey _photo = _classesRootKey.OpenSubKey(@"AppX43hnxtbyyps62jhe9sqpdzxn1790zetc\Shell\ShellEdit", true);
                                _photo.DeleteValue("ProgrammaticAccessOnly");
                                _photo.Close();

                                RegistryKey _lib = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\Folder\shellex\ContextMenuHandlers", true);
                                _lib.CreateSubKey(@"Library Location").SetValue("", "{3dad6c5d-2167-4cae-9914-f99e41c12cfa}", RegistryValueKind.String);
                                _lib.Close();

                                RegistryKey _mus = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\Directory.Audio\shellex\ContextMenuHandlers", true);
                                _mus.CreateSubKey(@"WMPShopMusic").SetValue("", "{8A734961-C4AA-4741-AC1E-791ACEBF5B39}", RegistryValueKind.String);
                                _mus.Close();

                                RegistryKey _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.3ds\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.CreateSubKey(@"3D Print").SetValue("", @"@%SystemRoot%\\system32\\PrintDialogs3D.dll,-5039", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Print\command").SetValue("DelegateExecute", "{1A68CF90-753A-4523-A4A4-40CAB4BC6EFF}", RegistryValueKind.String);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.3mf\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.CreateSubKey(@"3D Print").SetValue("", @"@%SystemRoot%\\system32\\PrintDialogs3D.dll,-5039", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Print\command").SetValue("DelegateExecute", "{1A68CF90-753A-4523-A4A4-40CAB4BC6EFF}", RegistryValueKind.String);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.bmp\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.dae\Shell", true);
                                _3d.CreateSubKey(@"3D Print").SetValue("", @"@%SystemRoot%\\system32\\PrintDialogs3D.dll,-5039", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Print\command").SetValue("DelegateExecute", "{1A68CF90-753A-4523-A4A4-40CAB4BC6EFF}", RegistryValueKind.String);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.dxf\Shell", true);
                                _3d.CreateSubKey(@"3D Print").SetValue("", @"@%SystemRoot%\\system32\\PrintDialogs3D.dll,-5039", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Print\command").SetValue("DelegateExecute", "{1A68CF90-753A-4523-A4A4-40CAB4BC6EFF}", RegistryValueKind.String);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.fbx\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.gif\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.glb\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jfif\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpe\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpeg\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpg\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.obj\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.CreateSubKey(@"3D Print").SetValue("", @"@%SystemRoot%\\system32\\PrintDialogs3D.dll,-5039", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Print\command").SetValue("DelegateExecute", "{1A68CF90-753A-4523-A4A4-40CAB4BC6EFF}", RegistryValueKind.String);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.ply\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.CreateSubKey(@"3D Print").SetValue("", @"@%SystemRoot%\\system32\\PrintDialogs3D.dll,-5039", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Print\command").SetValue("DelegateExecute", "{1A68CF90-753A-4523-A4A4-40CAB4BC6EFF}", RegistryValueKind.String);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.png\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.stl\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.CreateSubKey(@"3D Print").SetValue("", @"@%SystemRoot%\\system32\\PrintDialogs3D.dll,-5039", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Print\command").SetValue("DelegateExecute", "{1A68CF90-753A-4523-A4A4-40CAB4BC6EFF}", RegistryValueKind.String);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.tif\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.tiff\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.Close();

                                _3d = _localMachineKey.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.wrl\Shell", true);
                                _3d.CreateSubKey(@"3D Edit").SetValue("", @"@%SystemRoot%\system32\mspaint.exe,-59500", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Edit\command").SetValue("", @"%SystemRoot%\system32\mspaint.exe ""%1"" /ForceBootstrapPaint3D", RegistryValueKind.ExpandString);
                                _3d.CreateSubKey(@"3D Print").SetValue("", @"@%SystemRoot%\\system32\\PrintDialogs3D.dll,-5039", RegistryValueKind.String);
                                _3d.CreateSubKey(@"3D Print\command").SetValue("DelegateExecute", "{1A68CF90-753A-4523-A4A4-40CAB4BC6EFF}", RegistryValueKind.String);
                                _3d.Close();
                            }
                            break;
                        }
                    case 3:
                        {
                            if (_choose)
                            {
                                _localMachineKey.DeleteSubKey(@"SOFTWARE\Classes\.bmp\ShellNew");
                                _localMachineKey.DeleteSubKey(@"SOFTWARE\Classes\.bmp\PersistentHandler");
                                _localMachineKey.DeleteSubKey(@"SOFTWARE\Classes\.contact\ShellNew");
                                _localMachineKey.DeleteSubKey(@"SOFTWARE\Classes\.rtf\ShellNew");
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.bmp\ShellNew").SetValue("ItemName", @"@%systemroot%\system32\mspaint.exe,-59414", RegistryValueKind.ExpandString);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.bmp\ShellNew").SetValue("NullFile", @"", RegistryValueKind.String);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.bmp\PersistentHandler").SetValue("", @"{098f2470-bae0-11cd-b579-08002b30bfeb}", RegistryValueKind.String);

                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.contact\ShellNew").SetValue("command", @"""%programFiles%\Windows Mail\Wab.exe"" /CreateContact ""%1""", RegistryValueKind.ExpandString);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.contact\ShellNew").SetValue("Data", @"{\rtf1}", RegistryValueKind.String);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.contact\ShellNew").SetValue("iconpath", @"%ProgramFiles%\Windows Mail\wab.exe,1", RegistryValueKind.ExpandString);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.contact\ShellNew").SetValue("ItemName", @"@%ProgramFiles%\Windows NT\Accessories\WORDPAD.EXE,-213", RegistryValueKind.ExpandString);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.contact\ShellNew").SetValue("MenuText", @"@%CommonProgramFiles%\system\wab32res.dll,-10203", RegistryValueKind.ExpandString);

                                _localMachineKey.CreateSubKey(@"SOFTWARE\Classes\.rtf\ShellNew");
                            }
                            break;
                        }
                    case 4:
                        {
                            if (_choose)
                            {
                                _usersKey.CreateSubKey(@".DEFAULT\Control Panel\Colors").SetValue("InfoWindow", "240 255 255", RegistryValueKind.String);
                                _usersKey.CreateSubKey(@"S-1-5-19\Control Panel\Colors").SetValue("InfoWindow", "240 255 255", RegistryValueKind.String);
                                _usersKey.CreateSubKey(@"S-1-5-20\Control Panel\Colors").SetValue("InfoWindow", "240 255 255", RegistryValueKind.String);
                            }
                            else
                            {
                                _usersKey.CreateSubKey(@".DEFAULT\Control Panel\Colors").SetValue("InfoWindow", "255 255 255", RegistryValueKind.String);
                                _usersKey.CreateSubKey(@"S-1-5-19\Control Panel\Colors").SetValue("InfoWindow", "255 255 255", RegistryValueKind.String);
                                _usersKey.CreateSubKey(@"S-1-5-20\Control Panel\Colors").SetValue("InfoWindow", "255 255 255", RegistryValueKind.String);
                            }
                            break;
                        }
                    case 5:
                        {
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop\WindowMetrics").SetValue("CaptionHeight", "-270", RegistryValueKind.String);
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop\WindowMetrics").SetValue("CaptionWidth", "-270", RegistryValueKind.String);
                            }
                            else
                            {
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop\WindowMetrics").SetValue("CaptionHeight", "-330", RegistryValueKind.String);
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop\WindowMetrics").SetValue("CaptionWidth", "-330", RegistryValueKind.String);
                            }
                            break;
                        }
                    case 6:
                        {
                            string _arguments = default;
                            if (_choose)
                            {
                                _localMachineKey.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
                                _localMachineKey.DeleteSubKeyTree(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
                                _arguments = @"rd /s /q "" % userprofile %\3D Objects\""";
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0DB7E03F-FC29-4DC6-9020-FF41B59E513A}");
                                _arguments = @"md /s /q "" % userprofile %\3D Objects\""";
                            }

                            Process _process = new Process();
                            _process.StartInfo.UseShellExecute = false;
                            _process.StartInfo.RedirectStandardOutput = true;
                            _process.StartInfo.CreateNoWindow = true;
                            _process.StartInfo.FileName = "cmd.exe";
                            _process.StartInfo.Arguments = string.Format(_arguments);
                            _process.Start();
                            _process.Dispose();
                            break;
                        }
                    case 7:
                        {
                            if (_choose)
                            {
                                _localMachineKey.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{088e3905-0323-4b02-9826-5d99428e115f}");
                                _localMachineKey.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}");
                                _localMachineKey.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}");
                                _localMachineKey.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");
                                _localMachineKey.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{d3162b92-9365-467a-956b-92703aca08af}");
                                _localMachineKey.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}");
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{088e3905-0323-4b02-9826-5d99428e115f}");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{24ad3ad4-a569-4530-98e1-ab02f9417aa8}");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{3dfdf296-dbec-4fb4-81d1-6a3438bcf4de}");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{d3162b92-9365-467a-956b-92703aca08af}");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{f86fa3ab-70d2-4fc7-9c99-fcbf05467f3a}");
                            }
                            break;
                        }
                    case 8:
                        {
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            Environment.SpecialFolder _folderWindows = Environment.SpecialFolder.Windows;
                            string _pathToWinF = Environment.GetFolderPath(_folderWindows);
                            try
                            {
                                if (_choose)
                                {
                                    byte[] _iconByte = default;
                                    using (MemoryStream fileOut = new MemoryStream(Properties.Resources.Blank))
                                    using (GZipStream gz = new GZipStream(fileOut, CompressionMode.Decompress))
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        gz.CopyTo(ms);
                                        _iconByte = ms.ToArray();
                                    }
                                    File.WriteAllBytes(_pathToWinF + @"\Blank.ico", _iconByte);

                                    _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons").SetValue("29", @"%systemroot%\\Blank.ico,0", RegistryValueKind.String);
                                }
                                else
                                {
                                    File.Delete(_pathToWinF + @"\Blank.ico");
                                    RegistryKey _arrowIcon = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", RegistryKeyPermissionCheck.ReadWriteSubTree);
                                    _arrowIcon.DeleteSubKeyTree(@"Shell Icons");
                                    _arrowIcon.Close();
                                }
                            }
                            catch { };
                            break;
                        }
                    case 9:
                        {
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer").SetValue("link", Encoding.Unicode.GetBytes("\0\0"), RegistryValueKind.Binary);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer").DeleteValue("link");
                            break;
                        }
                    case 10:
                        {
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").SetValue("CursorBlinkRate", "250", RegistryValueKind.String);
                            else
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").SetValue("CursorBlinkRate", "530", RegistryValueKind.String);
                            break;
                        }
                    case 11:
                        {
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Control Panel\Mouse").SetValue("MouseHoverTime", "20", RegistryValueKind.String);
                            else
                                _currentUserKey.CreateSubKey(@"Control Panel\Mouse").SetValue("MouseHoverTime", "400", RegistryValueKind.String);
                            break;
                        }
                    case 12:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel").SetValue("{645FF040-5081-101B-9F08-00AA002F954E}", 1, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel").SetValue("{645FF040-5081-101B-9F08-00AA002F954E}", 0, RegistryValueKind.DWord);
                            break;
                        }
                    case 13:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Policies\Microsoft\Windows\Explorer").SetValue("DisableNotificationCenter", 1, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Policies\Microsoft\Windows\Explorer").DeleteValue("DisableNotificationCenter");
                            break;
                        }
                    case 14:
                        {
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop\WindowMetrics").SetValue("ScrollHeight", "-210", RegistryValueKind.String);
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop\WindowMetrics").SetValue("ScrollWidth", "-210", RegistryValueKind.String);
                            }
                            else
                            {
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop\WindowMetrics").SetValue("ScrollHeight", "-255", RegistryValueKind.String);
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop\WindowMetrics").SetValue("ScrollWidth", "-255", RegistryValueKind.String);
                            }
                            break;
                        }
                    case 15:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel").SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel").DeleteValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
                            break;
                        }
                    case 16:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("PersistBrowsers", 1, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").DeleteValue("PersistBrowsers");
                            break;
                        }
                    case 17:
                        {
                            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "11")
                            {
                                if (_choose)
                                {
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("ShowTaskViewButton", 0, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("TaskbarDa", 0, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("TaskbarMn", 0, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search").SetValue("SearchboxTaskbarMode", 0, RegistryValueKind.DWord);
                                }
                                else
                                {
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("ShowTaskViewButton", 1, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("TaskbarDa", 1, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("TaskbarMn", 1, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search").SetValue("SearchboxTaskbarMode", 1, RegistryValueKind.DWord);
                                }
                            }
                            break;
                        }
                    case 18:
                        {
                            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "11")
                            {
                                if (_choose)
                                {
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Privacy").SetValue("TailoredExperiencesWithDiagnosticDataEnabled", 0, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager").SetValue("RotatingLockScreenEnabled", 0, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager").SetValue("RotatingLockScreenOverlayEnabled", 0, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager").SetValue("SubscribedContent-338387Enabled", 0, RegistryValueKind.DWord);
                                }
                                else
                                {
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo").SetValue("Enabled", 1, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Privacy").SetValue("TailoredExperiencesWithDiagnosticDataEnabled", 1, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager").SetValue("RotatingLockScreenEnabled", 1, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager").SetValue("RotatingLockScreenOverlayEnabled", 1, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager").SetValue("SubscribedContent-338387Enabled", 1, RegistryValueKind.DWord);
                                }
                            }
                            break;
                        }
                    case 19:
                        {
                            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "11")
                            {
                                if (_choose)
                                    _currentUserKey.CreateSubKey(@"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32").SetValue("", RegistryValueKind.String);
                                else
                                {
                                    RegistryKey _classic = _currentUserKey.OpenSubKey(@"Software\Classes\CLSID\", RegistryKeyPermissionCheck.ReadWriteSubTree);
                                    _classic.DeleteSubKeyTree(@"{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}");
                                    _classic.Close();
                                }

                                string _explorerPath = string.Format(@"{0}\{1}", Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe");
                                foreach (Process _process in Process.GetProcesses())
                                {
                                    try
                                    {
                                        if (string.Compare(_process.MainModule.FileName, _explorerPath, StringComparison.OrdinalIgnoreCase) == 0)
                                            _process.Kill();
                                    }
                                    catch { }
                                }
                                Process.Start("explorer.exe");
                            }
                            break;
                        }
                    case 20:
                        {
                            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "11")
                            {
                                if (_choose)
                                {
                                    _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager").SetValue("SoftLandingEnabled", 0, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\UserProfileEngagement").SetValue("ScoobeSystemSettingEnabled ", 0, RegistryValueKind.DWord);
                                }
                                else
                                {
                                    _currentUserKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager").SetValue("SoftLandingEnabled", 1, RegistryValueKind.DWord);
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\UserProfileEngagement").SetValue("ScoobeSystemSettingEnabled ", 1, RegistryValueKind.DWord);
                                }
                            }
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
            try
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
            catch { }
        }

        internal void AppOneDrive(bool _choose)
        {
            try
            {
                Parallel.Invoke(() =>
                {
                    if (_choose)
                    {
                        string[] _onedrive = new string[6] { @"taskkill /f /im OneDrive.exe", @"%systemroot%\System32\OneDriveSetup.exe /uninstall", @"%systemroot%\SysWOW64\OneDriveSetup.exe /uninstall", @"rd /s /q %userprofile%\OneDrive", @"rd /s /q %userprofile%\AppData\Local\Microsoft\OneDrive", @"rd /s /q "" % allusersprofile %\Microsoft OneDrive""" };
                        Process _process = new Process();
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

                        RegistryKey _keyOneDrive = _classesRootKey.OpenSubKey(@"CLSID", true);
                        _keyOneDrive.DeleteSubKeyTree(@"{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                        _keyOneDrive = _classesRootKey.OpenSubKey(@"Wow6432Node\CLSID", true);
                        _keyOneDrive.DeleteSubKeyTree(@"{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                        _keyOneDrive.Close();
                    }
                    else
                    {
                        string[] _onedrive = new string[2] { @"%systemroot%\System32\OneDriveSetup.exe", @"%systemroot%\SysWOW64\OneDriveSetup.exe" };
                        Process _process = new Process();
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

                        _classesRootKey.CreateSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                        _classesRootKey.CreateSubKey(@"Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                    }
                });
            }
            catch { }
        }
        #endregion

        #region Services
        internal void GetSettingServices(in ServicesPage _servicesPage)
        {
            //#1
            _key[94] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WSearch");
            _key[95] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\fhsvc");

            if (_key[94] == null || _key[94].GetValue("Start", null) == null || _key[94].GetValue("Start").ToString() != "4" ||
                _key[95] == null || _key[95].GetValue("Start", null) == null || _key[95].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton1.State = true;
                _servicesPage.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton1.State = false;
                _servicesPage.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#2
            _key[96] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XboxGipSvc");
            _key[97] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XblAuthManager");
            _key[98] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XboxNetApiSvc");
            _key[99] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XblGameSave");

            if (_key[96] == null || _key[96].GetValue("Start", null) == null || _key[96].GetValue("Start").ToString() != "4" || _key[97] == null || _key[97].GetValue("Start", null) == null || _key[97].GetValue("Start").ToString() != "4" ||
                _key[98] == null || _key[98].GetValue("Start", null) == null || _key[98].GetValue("Start").ToString() != "4" || _key[99] == null || _key[99].GetValue("Start", null) == null || _key[99].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton2.State = true;
                _servicesPage.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton2.State = false;
                _servicesPage.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#3
            _key[100] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WwanSvc");
            _key[101] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wlpasvc");
            _key[102] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\icssvc");
            _key[103] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DusmSvc");
            _key[104] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\autotimesvc");

            if (_key[100] == null || _key[100].GetValue("Start", null) == null || _key[100].GetValue("Start").ToString() != "4" || _key[101] == null || _key[101].GetValue("Start", null) == null || _key[101].GetValue("Start").ToString() != "4" || _key[102] == null || _key[102].GetValue("Start", null) == null || _key[102].GetValue("Start").ToString() != "4" ||
                _key[103] == null || _key[103].GetValue("Start", null) == null || _key[103].GetValue("Start").ToString() != "4" || _key[104] == null || _key[104].GetValue("Start", null) == null || _key[104].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton3.State = true;
                _servicesPage.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton3.State = false;
                _servicesPage.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#4
            _key[105] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WalletService");
            _key[189] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\VacSvc");
            _key[190] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\spectrum");
            _key[191] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SharedRealitySvc");
            _key[192] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\perceptionsimulation");
            _key[193] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\MixedRealityOpenXRSvc");
            _key[194] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\MapsBroker");
            _key[195] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\EntAppSvc");
            _key[196] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\embeddedmode");
            _key[197] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wlidsvc");
            _key[198] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WEPHOSTSVC");
            _key[199] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\StorSvc");
            _key[200] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ClipSVC");
            _key[201] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\InstallService");

            if (_key[105] == null || _key[105].GetValue("Start", null) == null || _key[105].GetValue("Start").ToString() != "4" || _key[189] == null || _key[189].GetValue("Start", null) == null || _key[189].GetValue("Start").ToString() != "4" || _key[190] == null || _key[190].GetValue("Start", null) == null || _key[190].GetValue("Start").ToString() != "4" ||
                _key[191] == null || _key[191].GetValue("Start", null) == null || _key[191].GetValue("Start").ToString() != "4" || _key[192] == null || _key[192].GetValue("Start", null) == null || _key[192].GetValue("Start").ToString() != "4" || _key[193] == null || _key[193].GetValue("Start", null) == null || _key[193].GetValue("Start").ToString() != "4" ||
                _key[194] == null || _key[194].GetValue("Start", null) == null || _key[194].GetValue("Start").ToString() != "4" || _key[195] == null || _key[195].GetValue("Start", null) == null || _key[195].GetValue("Start").ToString() != "4" || _key[196] == null || _key[196].GetValue("Start", null) == null || _key[196].GetValue("Start").ToString() != "4" ||
                _key[197] == null || _key[197].GetValue("Start", null) == null || _key[197].GetValue("Start").ToString() != "4" || _key[198] == null || _key[198].GetValue("Start", null) == null || _key[198].GetValue("Start").ToString() != "4" || _key[199] == null || _key[199].GetValue("Start", null) == null || _key[199].GetValue("Start").ToString() != "4" ||
                _key[200] == null || _key[200].GetValue("Start", null) == null || _key[200].GetValue("Start").ToString() != "4" || _key[201] == null || _key[201].GetValue("Start", null) == null || _key[201].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton4.State = true;
                _servicesPage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton4.State = false;
                _servicesPage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#5
            _key[106] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wmiApSrv");
            _key[107] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\pla");
            _key[108] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PerfHost");

            if (_key[106] == null || _key[106].GetValue("Start", null) == null || _key[106].GetValue("Start").ToString() != "4" || _key[107] == null || _key[107].GetValue("Start", null) == null || _key[107].GetValue("Start").ToString() != "4" ||
                _key[108] == null || _key[108].GetValue("Start", null) == null || _key[108].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton5.State = true;
                _servicesPage.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton5.State = false;
                _servicesPage.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#6
            _key[109] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WbioSrvc");

            if (_key[109] == null || _key[109].GetValue("Start", null) == null || _key[109].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton6.State = true;
                _servicesPage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton6.State = false;
                _servicesPage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#7
            _key[110] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\bthserv");
            _key[111] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BthAvctpSvc");
            _key[112] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BTAGService");

            if (_key[110] == null || _key[110].GetValue("Start", null) == null || _key[110].GetValue("Start").ToString() != "4" || _key[111] == null || _key[111].GetValue("Start", null) == null || _key[111].GetValue("Start").ToString() != "4" ||
                _key[112] == null || _key[112].GetValue("Start", null) == null || _key[112].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton7.State = true;
                _servicesPage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton7.State = false;
                _servicesPage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#8
            _key[113] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Spooler");
            _key[114] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PrintNotify");
            _key[115] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\McpManagementService");

            if (_key[113] == null || _key[113].GetValue("Start", null) == null || _key[113].GetValue("Start").ToString() != "4" || _key[114] == null || _key[114].GetValue("Start", null) == null || _key[114].GetValue("Start").ToString() != "4" ||
                _key[115] == null || _key[115].GetValue("Start", null) == null || _key[115].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton8.State = true;
                _servicesPage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton8.State = false;
                _servicesPage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#9
            _key[116] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WiaRpc");

            if (_key[116] == null || _key[116].GetValue("Start", null) == null || _key[116].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton9.State = true;
                _servicesPage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton9.State = false;
                _servicesPage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#10
            _key[117] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TapiSrv");
            _key[118] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PhoneSvc");
            _key[119] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Fax");

            if (_key[117] == null || _key[117].GetValue("Start", null) == null || _key[117].GetValue("Start").ToString() != "4" || _key[118] == null || _key[118].GetValue("Start", null) == null || _key[118].GetValue("Start").ToString() != "4" ||
                _key[119] == null || _key[119].GetValue("Start", null) == null || _key[119].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton10.State = true;
                _servicesPage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton10.State = false;
                _servicesPage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#11
            _key[120] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensrSvc");
            _key[121] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensorService");
            _key[122] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensorDataService");
            _key[123] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SEMgrSvc");
            _key[124] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\lfsvc");

            if (_key[120] == null || _key[120].GetValue("Start", null) == null || _key[120].GetValue("Start").ToString() != "4" || _key[121] == null || _key[121].GetValue("Start", null) == null || _key[121].GetValue("Start").ToString() != "4" || _key[122] == null || _key[122].GetValue("Start", null) == null || _key[122].GetValue("Start").ToString() != "4" ||
                _key[123] == null || _key[123].GetValue("Start", null) == null || _key[123].GetValue("Start").ToString() != "4" || _key[124] == null || _key[124].GetValue("Start", null) == null || _key[124].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton11.State = true;
                _servicesPage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton11.State = false;
                _servicesPage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#12
            _key[125] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DispBrokerDesktopSvc");
            _key[126] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WFDSConMgrSvc");

            if (_key[125] == null || _key[125].GetValue("Start", null) == null || _key[125].GetValue("Start").ToString() != "4" ||
                _key[126] == null || _key[126].GetValue("Start", null) == null || _key[126].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton12.State = true;
                _servicesPage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton12.State = false;
                _servicesPage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#13
            _key[127] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CDPSvc");
            _key[128] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PushToInstall");
            _key[129] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WpnService");

            if (_key[127] == null || _key[127].GetValue("Start", null) == null || _key[127].GetValue("Start").ToString() != "4" || _key[128] == null || _key[128].GetValue("Start", null) == null || _key[128].GetValue("Start").ToString() != "4" ||
                _key[129] == null || _key[129].GetValue("Start", null) == null || _key[129].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton13.State = true;
                _servicesPage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton13.State = false;
                _servicesPage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#14
            _key[130] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Netlogon");
            _key[131] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CscService");
            _key[132] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\lmhosts");
            _key[133] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\FDResPub");
            _key[134] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\fdPHost");
            _key[135] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanServer");
            _key[136] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanWorkstation");

            if (_key[130] == null || _key[130].GetValue("Start", null) == null || _key[130].GetValue("Start").ToString() != "4" || _key[131] == null || _key[131].GetValue("Start", null) == null || _key[131].GetValue("Start").ToString() != "4" || _key[132] == null || _key[132].GetValue("Start", null) == null || _key[132].GetValue("Start").ToString() != "4" ||
                _key[133] == null || _key[133].GetValue("Start", null) == null || _key[133].GetValue("Start").ToString() != "4" || _key[134] == null || _key[134].GetValue("Start", null) == null || _key[134].GetValue("Start").ToString() != "4" || _key[135] == null || _key[135].GetValue("Start", null) == null || _key[135].GetValue("Start").ToString() != "4" ||
                _key[136] == null || _key[136].GetValue("Start", null) == null || _key[136].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton14.State = true;
                _servicesPage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton14.State = false;
                _servicesPage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#15
            _key[137] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wisvc");
            _key[138] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DmEnrollmentSvc");
            _key[139] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wuauserv");
            _key[140] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WaaSMedicSvc");
            _key[141] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DoSvc");
            _key[142] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\UsoSvc");

            if (_key[137] == null || _key[137].GetValue("Start", null) == null || _key[137].GetValue("Start").ToString() != "4" || _key[138] == null || _key[138].GetValue("Start", null) == null || _key[138].GetValue("Start").ToString() != "4" || _key[139] == null || _key[139].GetValue("Start", null) == null || _key[139].GetValue("Start").ToString() != "4" ||
                _key[140] == null || _key[140].GetValue("Start", null) == null || _key[140].GetValue("Start").ToString() != "4" || _key[141] == null || _key[141].GetValue("Start", null) == null || _key[141].GetValue("Start").ToString() != "4" || _key[142] == null || _key[142].GetValue("Start", null) == null || _key[142].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton15.State = true;
                _servicesPage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton15.State = false;
                _servicesPage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#16
            _key[143] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PolicyAgent");
            _key[144] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\IKEEXT");
            _key[145] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\p2pimsvc");

            if (_key[143] == null || _key[143].GetValue("Start", null) == null || _key[143].GetValue("Start").ToString() != "4" || _key[144] == null || _key[144].GetValue("Start", null) == null || _key[144].GetValue("Start").ToString() != "4" ||
                _key[145] == null || _key[145].GetValue("Start", null) == null || _key[145].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton16.State = true;
                _servicesPage.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton16.State = false;
                _servicesPage.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#17
            _key[146] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WPDBusEnum");
            _key[147] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WMPNetworkSvc");

            if (_key[146] == null || _key[146].GetValue("Start", null) == null || _key[146].GetValue("Start").ToString() != "4" ||
                _key[147] == null || _key[147].GetValue("Start", null) == null || _key[147].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton17.State = true;
                _servicesPage.Tweak17.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton17.State = false;
                _servicesPage.Tweak17.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#18
            _key[148] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\UmRdpService");
            _key[149] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TermService");
            _key[150] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SessionEnv");
            _key[151] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DsSvc");
            _key[152] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\RemoteRegistry");

            if (_key[148] == null || _key[148].GetValue("Start", null) == null || _key[148].GetValue("Start").ToString() != "4" || _key[149] == null || _key[149].GetValue("Start", null) == null || _key[149].GetValue("Start").ToString() != "4" || _key[150] == null || _key[150].GetValue("Start", null) == null || _key[150].GetValue("Start").ToString() != "4" ||
                _key[151] == null || _key[151].GetValue("Start", null) == null || _key[151].GetValue("Start").ToString() != "4" || _key[152] == null || _key[152].GetValue("Start", null) == null || _key[152].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton18.State = true;
                _servicesPage.Tweak18.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton18.State = false;
                _servicesPage.Tweak18.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#19
            _key[153] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WerSvc");
            _key[154] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wercplsupport");
            _key[155] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Wecsvc");

            if (_key[153] == null || _key[153].GetValue("Start", null) == null || _key[153].GetValue("Start").ToString() != "4" || _key[154] == null || _key[154].GetValue("Start", null) == null || _key[154].GetValue("Start").ToString() != "4" ||
                _key[155] == null || _key[155].GetValue("Start", null) == null || _key[155].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton19.State = true;
                _servicesPage.Tweak19.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton19.State = false;
                _servicesPage.Tweak19.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#20
            _key[156] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WebClient");

            if (_key[156] == null || _key[156].GetValue("Start", null) == null || _key[156].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton20.State = true;
                _servicesPage.Tweak20.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton20.State = false;
                _servicesPage.Tweak20.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#21
            _key[157] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SCPolicySvc");
            _key[158] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ScDeviceEnum");
            _key[159] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SCardSvr");
            _key[160] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CertPropSvc");

            if (_key[157] == null || _key[157].GetValue("Start", null) == null || _key[157].GetValue("Start").ToString() != "4" || _key[158] == null || _key[158].GetValue("Start", null) == null || _key[158].GetValue("Start").ToString() != "4" ||
                _key[159] == null || _key[159].GetValue("Start", null) == null || _key[159].GetValue("Start").ToString() != "4" || _key[160] == null || _key[160].GetValue("Start", null) == null || _key[160].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton21.State = true;
                _servicesPage.Tweak21.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton21.State = false;
                _servicesPage.Tweak21.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#22
            _key[161] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AssignedAccessManagerSvc");
            _key[162] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppReadiness");

            if (_key[161] == null || _key[161].GetValue("Start", null) == null || _key[161].GetValue("Start").ToString() != "4" || _key[162] == null || _key[162].GetValue("Start", null) == null || _key[162].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton22.State = true;
                _servicesPage.Tweak22.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton22.State = false;
                _servicesPage.Tweak22.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#23
            _key[163] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BDESVC");
            _key[164] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\EFS");

            if (_key[163] == null || _key[163].GetValue("Start", null) == null || _key[163].GetValue("Start").ToString() != "4" || _key[164] == null || _key[164].GetValue("Start", null) == null || _key[164].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton23.State = true;
                _servicesPage.Tweak23.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton23.State = false;
                _servicesPage.Tweak23.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#24
            _key[165] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LxpSvc");

            if (_key[165] == null || _key[165].GetValue("Start", null) == null || _key[165].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton24.State = true;
                _servicesPage.Tweak24.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton24.State = false;
                _servicesPage.Tweak24.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#25
            _key[166] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WarpJITSvc");
            _key[167] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wscsvc");

            if (_key[166] == null || _key[166].GetValue("Start", null) == null || _key[166].GetValue("Start").ToString() != "4" || _key[167] == null || _key[167].GetValue("Start", null) == null || _key[167].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton25.State = true;
                _servicesPage.Tweak25.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton25.State = false;
                _servicesPage.Tweak25.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#26
            _key[168] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WdiSystemHost");
            _key[169] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WdiServiceHost");
            _key[170] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TroubleshootingSvc");
            _key[171] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DPS");
            _key[172] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service");

            if (_key[168] == null || _key[168].GetValue("Start", null) == null || _key[168].GetValue("Start").ToString() != "4" || _key[169] == null || _key[169].GetValue("Start", null) == null || _key[169].GetValue("Start").ToString() != "4" || _key[170] == null || _key[170].GetValue("Start", null) == null || _key[170].GetValue("Start").ToString() != "4" ||
                _key[171] == null || _key[171].GetValue("Start", null) == null || _key[171].GetValue("Start").ToString() != "4" || _key[172] == null || _key[172].GetValue("Start", null) == null || _key[172].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton26.State = true;
                _servicesPage.Tweak26.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton26.State = false;
                _servicesPage.Tweak26.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#27
            _key[173] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\workfolderssvc");
            _key[174] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\RemoteRegistry");
            _key[175] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Netlogon");
            _key[176] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\EntAppSvc");
            _key[177] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dot3svc");
            _key[178] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DevQueryBroker");
            _key[179] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppMgmt");

            if (_key[173] == null || _key[173].GetValue("Start", null) == null || _key[173].GetValue("Start").ToString() != "4" || _key[174] == null || _key[174].GetValue("Start", null) == null || _key[174].GetValue("Start").ToString() != "4" || _key[175] == null || _key[175].GetValue("Start", null) == null || _key[175].GetValue("Start").ToString() != "4" ||
                _key[176] == null || _key[176].GetValue("Start", null) == null || _key[176].GetValue("Start").ToString() != "4" || _key[177] == null || _key[177].GetValue("Start", null) == null || _key[177].GetValue("Start").ToString() != "4" || _key[178] == null || _key[178].GetValue("Start", null) == null || _key[178].GetValue("Start").ToString() != "4" ||
                _key[179] == null || _key[179].GetValue("Start", null) == null || _key[179].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton27.State = true;
                _servicesPage.Tweak27.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton27.State = false;
                _servicesPage.Tweak27.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#27
            _key[173] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\workfolderssvc");
            _key[174] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\RemoteRegistry");
            _key[175] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Netlogon");
            _key[176] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\EntAppSvc");
            _key[177] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dot3svc");
            _key[178] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DevQueryBroker");
            _key[179] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppMgmt");

            if (_key[173] == null || _key[173].GetValue("Start", null) == null || _key[173].GetValue("Start").ToString() != "4" || _key[174] == null || _key[174].GetValue("Start", null) == null || _key[174].GetValue("Start").ToString() != "4" || _key[175] == null || _key[175].GetValue("Start", null) == null || _key[175].GetValue("Start").ToString() != "4" ||
                _key[176] == null || _key[176].GetValue("Start", null) == null || _key[176].GetValue("Start").ToString() != "4" || _key[177] == null || _key[177].GetValue("Start", null) == null || _key[177].GetValue("Start").ToString() != "4" || _key[178] == null || _key[178].GetValue("Start", null) == null || _key[178].GetValue("Start").ToString() != "4" ||
                _key[179] == null || _key[179].GetValue("Start", null) == null || _key[179].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton27.State = true;
                _servicesPage.Tweak27.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton27.State = false;
                _servicesPage.Tweak27.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#28
            _key[180] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicvmsession");
            _key[181] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmictimesync");
            _key[182] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicshutdown");
            _key[183] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicrdv");
            _key[184] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmickvpexchange");
            _key[185] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicheartbeat");
            _key[186] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicguestinterface");
            _key[187] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\HvHost");
            _key[188] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicvss");

            if (_key[180] == null || _key[180].GetValue("Start", null) == null || _key[180].GetValue("Start").ToString() != "4" || _key[181] == null || _key[181].GetValue("Start", null) == null || _key[181].GetValue("Start").ToString() != "4" || _key[182] == null || _key[182].GetValue("Start", null) == null || _key[182].GetValue("Start").ToString() != "4" ||
                _key[183] == null || _key[183].GetValue("Start", null) == null || _key[183].GetValue("Start").ToString() != "4" || _key[184] == null || _key[184].GetValue("Start", null) == null || _key[184].GetValue("Start").ToString() != "4" || _key[185] == null || _key[185].GetValue("Start", null) == null || _key[185].GetValue("Start").ToString() != "4" ||
                _key[186] == null || _key[186].GetValue("Start", null) == null || _key[186].GetValue("Start").ToString() != "4" || _key[187] == null || _key[187].GetValue("Start", null) == null || _key[187].GetValue("Start").ToString() != "4" || _key[188] == null || _key[188].GetValue("Start", null) == null || _key[188].GetValue("Start").ToString() != "4")
            {
                _servicesPage.TButton28.State = true;
                _servicesPage.Tweak28.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _servicesPage.TButton28.State = false;
                _servicesPage.Tweak28.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }


        }

        internal void ChangeSettingServices(in bool _choose, in byte _select)
        {
            try
            {
                Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                switch (_select)
                {
                    case 1:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WSearch", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\fhsvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WSearch", true).SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\fhsvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XboxGipSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XblAuthManager", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XboxNetApiSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XblGameSave", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XboxGipSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XblAuthManager", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XboxNetApiSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\XblGameSave", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 3:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WwanSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wlpasvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\icssvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DusmSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\autotimesvc", true).SetValue("Start", 4, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DusmSvc", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WwanSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wlpasvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\icssvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DusmSvc", true).SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\autotimesvc", true).SetValue("Start", 3, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DusmSvc", true).SetValue("DelayedAutoStart", 0, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 4:
                        {
                            string[] _winshop = new string[14] { "WalletService", "VacSvc", "spectrum", "SharedRealitySvc", "perceptionsimulation", "MixedRealityOpenXRSvc", "MapsBroker", "EntAppSvc",
                                "embeddedmode", "wlidsvc", "WEPHOSTSVC", "StorSvc", "ClipSVC", "InstallService" };

                            if (_choose)
                            {
                                for (int i = 0; i < _winshop.Length; i++)
                                {
                                    RegistryKey rkey = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + _winshop[i], RegistryKeyPermissionCheck.ReadWriteSubTree);
                                    RegistrySecurity _registrySecurity = new RegistrySecurity();
                                    WindowsIdentity _windowsIdentity = WindowsIdentity.GetCurrent();
                                    RegistryAccessRule _accessRule = new RegistryAccessRule(_windowsIdentity.Name, RegistryRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
                                    _registrySecurity.AddAccessRule(_accessRule);
                                    _registrySecurity.SetAccessRuleProtection(false, true);
                                    rkey.SetAccessControl(_registrySecurity);

                                    _registrySecurity.SetGroup(new NTAccount("SYSTEM"));
                                    NTAccount SID = new NTAccount(Environment.UserDomainName + "\\" + Environment.UserName);
                                    _registrySecurity.SetOwner(SID);
                                    rkey.SetAccessControl(_registrySecurity);
                                    rkey.SetValue("Start", 4, RegistryValueKind.DWord);
                                    rkey.Close();
                                }

                            }
                            else
                            {
                                for (int i = 0; i < _winshop.Length; i++)
                                {
                                    RegistryKey rkey = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + _winshop[i], RegistryKeyPermissionCheck.ReadWriteSubTree);
                                    RegistrySecurity _registrySecurity = new RegistrySecurity();
                                    WindowsIdentity _windowsIdentity = WindowsIdentity.GetCurrent();
                                    RegistryAccessRule _accessRule = new RegistryAccessRule(_windowsIdentity.Name, RegistryRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
                                    _registrySecurity.AddAccessRule(_accessRule);
                                    _registrySecurity.SetAccessRuleProtection(false, true);
                                    rkey.SetAccessControl(_registrySecurity);

                                    _registrySecurity.SetGroup(new NTAccount("SYSTEM"));
                                    NTAccount SID = new NTAccount(Environment.UserDomainName + "\\" + Environment.UserName);
                                    _registrySecurity.SetOwner(SID);
                                    rkey.SetAccessControl(_registrySecurity);
                                    if (_winshop[i] == "MapsBroker")
                                        rkey.SetValue("Start", 2, RegistryValueKind.DWord);
                                    else
                                        rkey.SetValue("Start", 3, RegistryValueKind.DWord);
                                    rkey.Close();
                                }
                            }
                            break;
                        }
                    case 5:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wmiApSrv", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\pla", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PerfHost", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wmiApSrv", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\pla", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PerfHost", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 6:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WbioSrvc", true).SetValue("Start", 4, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WbioSrvc", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WbioSrvc", true).SetValue("Start", 2, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WbioSrvc", true).SetValue("DelayedAutoStart", 0, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 7:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\bthserv", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BthAvctpSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BTAGService", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\bthserv", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BthAvctpSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BTAGService", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 8:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Spooler", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PrintNotify", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\McpManagementService", true).SetValue("Start", 4, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Spooler", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Spooler", true).SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PrintNotify", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\McpManagementService", true).SetValue("Start", 3, RegistryValueKind.DWord);


                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Spooler", true).SetValue("DelayedAutoStart", 0, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 9:
                        {
                            if (_choose)
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WiaRpc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WiaRpc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            break;
                        }
                    case 10:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TapiSrv", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PhoneSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Fax", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TapiSrv", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PhoneSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Fax", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 11:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensrSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensorService", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensorDataService", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SEMgrSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\lfsvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensrSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensorService", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SensorDataService", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SEMgrSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\lfsvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 12:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DispBrokerDesktopSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WFDSConMgrSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DispBrokerDesktopSvc", true).SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WFDSConMgrSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 13:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CDPSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PushToInstall", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WpnService", true).SetValue("Start", 4, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CDPSvc", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WpnService", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CDPSvc", true).SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PushToInstall", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WpnService", true).SetValue("Start", 2, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CDPSvc", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WpnService", true).SetValue("DelayedAutoStart", 0, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 14:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Netlogon", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CscService", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\lmhosts", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\FDResPub", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\fdPHost", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanServer", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanWorkstation", true).SetValue("Start", 4, RegistryValueKind.DWord);


                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanServer", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanWorkstation", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Netlogon", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CscService", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\lmhosts", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\FDResPub", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\fdPHost", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanServer", true).SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanWorkstation", true).SetValue("Start", 2, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanServer", true).SetValue("DelayedAutoStart", 0, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanWorkstation", true).SetValue("DelayedAutoStart", 0, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 15:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wisvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DmEnrollmentSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wuauserv", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WaaSMedicSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DoSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\UsoSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wisvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DmEnrollmentSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wuauserv", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WaaSMedicSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DoSvc", true).SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\UsoSvc", true).SetValue("Start", 2, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 16:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PolicyAgent", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\IKEEXT", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\p2pimsvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PolicyAgent", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\IKEEXT", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\p2pimsvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 17:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WPDBusEnum", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WPDBusEnum", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 18:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\UmRdpService", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TermService", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SessionEnv", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DsSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\RemoteRegistry", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\UmRdpService", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TermService", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SessionEnv", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DsSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\RemoteRegistry", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 19:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WerSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wercplsupport", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Wecsvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WerSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wercplsupport", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Wecsvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 20:
                        {
                            if (_choose)
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WebClient", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WebClient", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            break;
                        }
                    case 21:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SCPolicySvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ScDeviceEnum", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SCardSvr", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CertPropSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SCPolicySvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ScDeviceEnum", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SCardSvr", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\CertPropSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 22:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AssignedAccessManagerSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppReadiness", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AssignedAccessManagerSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppReadiness", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 23:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BDESVC", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\EFS", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BDESVC", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\EFS", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 24:
                        {
                            if (_choose)
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LxpSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            else
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LxpSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            break;
                        }
                    case 25:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WarpJITSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wscsvc", true).SetValue("Start", 4, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wscsvc", true).SetValue("DelayedAutoStart", 1, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WarpJITSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wscsvc", true).SetValue("Start", 2, RegistryValueKind.DWord);

                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wscsvc", true).SetValue("DelayedAutoStart", 0, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 26:
                        {
                            TakingOwnership.GrantAdministratorsAccess(@"MACHINE\SYSTEM\CurrentControlSet\Services\WdiSystemHost", TakingOwnership.SE_OBJECT_TYPE.SE_REGISTRY_KEY);
                            TakingOwnership.GrantAdministratorsAccess(@"MACHINE\SYSTEM\CurrentControlSet\Services\WdiServiceHost", TakingOwnership.SE_OBJECT_TYPE.SE_REGISTRY_KEY);
                            TakingOwnership.GrantAdministratorsAccess(@"MACHINE\SYSTEM\CurrentControlSet\Services\TroubleshootingSvc", TakingOwnership.SE_OBJECT_TYPE.SE_REGISTRY_KEY);
                            TakingOwnership.GrantAdministratorsAccess(@"MACHINE\SYSTEM\CurrentControlSet\Services\DPS", TakingOwnership.SE_OBJECT_TYPE.SE_REGISTRY_KEY);
                            TakingOwnership.GrantAdministratorsAccess(@"MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", TakingOwnership.SE_OBJECT_TYPE.SE_REGISTRY_KEY);

                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WdiSystemHost", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WdiServiceHost", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TroubleshootingSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DPS", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", true).SetValue("Start", 4, RegistryValueKind.DWord);

                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WdiSystemHost", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WdiServiceHost", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\TroubleshootingSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DPS", true).SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 27:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\workfolderssvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\RemoteRegistry", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Netlogon", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\EntAppSvc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dot3svc", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DevQueryBroker", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppMgmt", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\workfolderssvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\RemoteRegistry", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Netlogon", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\EntAppSvc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dot3svc", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DevQueryBroker", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppMgmt", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 28:
                        {
                            if (_choose)
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicvmsession", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmictimesync", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicshutdown", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicrdv", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmickvpexchange", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicheartbeat", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicguestinterface", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\HvHost", true).SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicvss", true).SetValue("Start", 4, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicvmsession", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmictimesync", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicshutdown", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicrdv", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmickvpexchange", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicheartbeat", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicguestinterface", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\HvHost", true).SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\vmicvss", true).SetValue("Start", 3, RegistryValueKind.DWord);
                            }
                            break;
                        }
                }
            }
            catch { };
        }
        #endregion

        #region System
        internal void GetSettingSystem(in SystemPage _systemPage)
        {
            //#1
            _key[202] = _currentUserKey.OpenSubKey(@"Control Panel\Mouse");

            if (_key[202] == null || _key[202].GetValue("MouseSensitivity", null) == null)
                _systemPage.Slider1.Value = 0;
            else
                _systemPage.Slider1.Value = double.Parse(_key[202].GetValue("MouseSensitivity").ToString());

            //#2
            _key[203] = _currentUserKey.OpenSubKey(@"Control Panel\Keyboard");

            if (_key[203] == null || _key[203].GetValue("KeyboardDelay", null) == null)
                _systemPage.Slider2.Value = 0;
            else
                _systemPage.Slider2.Value = double.Parse(_key[203].GetValue("KeyboardDelay").ToString());

            //#3
            _key[204] = _currentUserKey.OpenSubKey(@"Control Panel\Keyboard");

            if (_key[204] == null || _key[204].GetValue("KeyboardSpeed", null) == null)
                _systemPage.Slider3.Value = 0;
            else
                _systemPage.Slider3.Value = double.Parse(_key[204].GetValue("KeyboardSpeed").ToString());

            //#4
            _key[205] = _currentUserKey.OpenSubKey(@"Control Panel\Mouse");

            if (_key[205] == null || _key[205].GetValue("MouseSpeed", null) == null || _key[205].GetValue("MouseSpeed").ToString() != "0" || _key[205] == null || _key[205].GetValue("MouseThreshold1", null) == null || _key[205].GetValue("MouseThreshold1").ToString() != "0" ||
                _key[205] == null || _key[205].GetValue("MouseThreshold2", null) == null || _key[205].GetValue("MouseThreshold2").ToString() != "0")
            {
                _systemPage.TButton4.State = true;
                _systemPage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton4.State = false;
                _systemPage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#6
            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "10")
            {
                _key[206] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.SecurityAndMaintenance");

                if (_key[206] == null || _key[206].GetValue("Enabled", null) == null || _key[206].GetValue("Enabled").ToString() != "0")
                {
                    _systemPage.TButton6.State = true;
                    _systemPage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _systemPage.TButton6.State = false;
                    _systemPage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }
            }
            else
            {
                _key[206] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\PushNotifications");

                if (_key[206] == null || _key[206].GetValue("ToastEnabled", null) == null || _key[206].GetValue("ToastEnabled").ToString() != "0")
                {
                    _systemPage.TButton6.State = true;
                    _systemPage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _systemPage.TButton6.State = false;
                    _systemPage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }
            }

            //#7
            _key[207] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Security");
            _key[208] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3");

            if (_key[207] == null || _key[207].GetValue("DisableSecuritySettingsCheck", null) == null || _key[207].GetValue("DisableSecuritySettingsCheck").ToString() != "1" ||
                _key[208] == null || _key[208].GetValue("1806", null) == null || _key[208].GetValue("1806").ToString() != "0")
            {
                _systemPage.TButton7.State = true;
                _systemPage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton7.State = false;
                _systemPage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#8
            _key[209] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Power\PowerSettings\7516b95f-f776-4464-8c53-06167f40cc99\8EC4B3A5-6868-48c2-BE75-4F3044BE88A7");

            if (_key[209] == null || _key[209].GetValue("Attributes", null) == null || _key[209].GetValue("Attributes").ToString() != "2")
            {
                _systemPage.TButton8.State = true;
                _systemPage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton8.State = false;
                _systemPage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#9
            _key[210] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer");
            _key[211] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\PhishingFilter");
            _key[212] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System");
            _key[213] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender");
            _key[214] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\SmartScreen");
            _key[215] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\SecurityHealthService");
            _key[216] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\wscsvc");
            _key[217] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\AppHost");

            if (_key[210] == null || _key[210].GetValue("SmartScreenEnabled", null) == null || _key[210].GetValue("SmartScreenEnabled").ToString() != "off" || _key[211] == null || _key[211].GetValue("EnabledV9", null) == null || _key[211].GetValue("EnabledV9").ToString() != "0" || _key[212] == null || _key[212].GetValue("EnableSmartScreen", null) == null || _key[212].GetValue("EnableSmartScreen").ToString() != "0" ||
                _key[213] == null || _key[213].GetValue("DisableAntiSpyware", null) == null || _key[213].GetValue("DisableAntiSpyware").ToString() != "1" || _key[214] == null || _key[214].GetValue("ConfigureAppInstallControl", null) == null || _key[214].GetValue("ConfigureAppInstallControl").ToString() != "Anywhere" || _key[214] == null || _key[214].GetValue("ConfigureAppInstallControlEnabled", null) == null || _key[214].GetValue("ConfigureAppInstallControlEnabled").ToString() != "1" ||
                _key[215] == null || _key[215].GetValue("Start", null) == null || _key[215].GetValue("Start").ToString() != "4" || _key[216] == null || _key[216].GetValue("Start", null) == null || _key[216].GetValue("Start").ToString() != "4" || _key[217] == null || _key[217].GetValue("EnableWebContentEvaluation", null) == null || _key[217].GetValue("EnableWebContentEvaluation").ToString() != "0")
            {
                _systemPage.TButton9.State = true;
                _systemPage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton9.State = false;
                _systemPage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#10
            _key[218] = _localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");

            if (_key[218] == null || _key[218].GetValue("ConsentPromptBehaviorAdmin", null) == null || _key[218].GetValue("ConsentPromptBehaviorAdmin").ToString() != "0" || _key[218] == null || _key[218].GetValue("EnableInstallerDetection", null) == null || _key[218].GetValue("EnableInstallerDetection").ToString() != "0" || _key[218] == null || _key[218].GetValue("EnableLUA", null) == null || _key[218].GetValue("EnableLUA").ToString() != "0" ||
                _key[218] == null || _key[218].GetValue("EnableSecureUIAPaths", null) == null || _key[218].GetValue("EnableSecureUIAPaths").ToString() != "0" || _key[218] == null || _key[218].GetValue("EnableVirtualization", null) == null || _key[218].GetValue("EnableVirtualization").ToString() != "0" || _key[218] == null || _key[218].GetValue("FilterAdministratorToken", null) == null || _key[218].GetValue("FilterAdministratorToken").ToString() != "0" ||
                _key[218] == null || _key[218].GetValue("PromptOnSecureDesktop", null) == null || _key[218].GetValue("PromptOnSecureDesktop").ToString() != "0")
            {
                _systemPage.TButton10.State = true;
                _systemPage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton10.State = false;
                _systemPage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#11
            if (_countTaskSystem > 0)
            {
                _systemPage.TButton11.State = true;
                _systemPage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton11.State = false;
                _systemPage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#12
            if (_countProtocolSystem > 0)
            {
                _systemPage.TButton12.State = true;
                _systemPage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton12.State = false;
                _systemPage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#13
            _key[219] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management");

            if (_key[219] == null || _key[219].GetValue("LargeSystemCache", null) == null || _key[219].GetValue("LargeSystemCache").ToString() != "1")
            {
                _systemPage.TButton13.State = true;
                _systemPage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton13.State = false;
                _systemPage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#14
            _key[220] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize");

            if (_key[220] == null || _key[220].GetValue("Startupdelayinmsec", null) == null || _key[220].GetValue("Startupdelayinmsec").ToString() != "0")
            {
                _systemPage.TButton14.State = true;
                _systemPage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton14.State = false;
                _systemPage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#15
            _key[221] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer");
            _key[222] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

            if (_key[221] == null || _key[221].GetValue("ShowFrequent", null) == null || _key[221].GetValue("ShowFrequent").ToString() != "0" || _key[221] == null || _key[221].GetValue("ShowRecent", null) == null || _key[221].GetValue("ShowRecent").ToString() != "0" ||
                _key[222] == null || _key[222].GetValue("Start_TrackDocs", null) == null || _key[222].GetValue("Start_TrackDocs").ToString() != "0" || _key[222] == null || _key[222].GetValue("Start_TrackProgs", null) == null || _key[222].GetValue("Start_TrackProgs").ToString() != "0")
            {
                _systemPage.TButton15.State = true;
                _systemPage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton15.State = false;
                _systemPage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#16
            _key[223] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers");

            if (_key[223] == null || _key[223].GetValue("DisableAutoplay", null) == null || _key[223].GetValue("DisableAutoplay").ToString() != "1")
            {
                _systemPage.TButton16.State = true;
                _systemPage.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _systemPage.TButton16.State = false;
                _systemPage.Tweak16.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        internal void TaskCheckStateSystem()
        {
            string[] TaskName = new string[2] { @"""Microsoft\Windows\MemoryDiagnostic\ProcessMemoryDiagnosticEvents""", @"""Microsoft\Windows\MemoryDiagnostic\RunFullMemoryDiagnostic""" };

            Process _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.FileName = "cmd.exe";
            _countTaskSystem = 0;
            foreach (var _task in TaskName)
            {
                _process.StartInfo.Arguments = string.Format("/c chcp 65001 & schtasks /tn {0}", _task);
                _process.Start();
                _process.StandardOutput.ReadLine();
                string _tbl = _process.StandardOutput.ReadToEnd();
                if (_tbl.Contains("Ready"))
                    _countTaskSystem++;
            }
            _process.Dispose();
        }

        internal void ProtocolCheckStateSystem()
        {
            string[] Protocols = new string[2] { @"netsh interface isatap show state", "netsh int ipv6 isatap show state" };

            Process _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.FileName = "cmd.exe";
            _countProtocolSystem = 0;
            foreach (var _protocol in Protocols)
            {
                _process.StartInfo.Arguments = string.Format("/c chcp 65001 & {0}", _protocol);
                _process.Start();
                _process.StandardOutput.ReadLine();
                string _tbl = _process.StandardOutput.ReadToEnd();
                if (_tbl.Contains("default"))
                    _countProtocolSystem++;
            }
            _process.Dispose();
        }

        [DllImport("User32.dll")]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

        internal void ChangeSettingSystem(in bool _choose, in byte _select, in uint _value)
        {
            try
            {
                switch (_select)
                {
                    case 1:
                        {
                            SystemParametersInfo(SPI_SETMOUSESPEED, _value, _value, 2);
                            _currentUserKey.OpenSubKey(@"Control Panel\Mouse", true).SetValue("MouseSensitivity", _value, RegistryValueKind.String);
                            break;
                        }
                    case 2:
                        {
                            SystemParametersInfo(SPI_SETKEYBOARDDELAY, _value, _value, 2);
                            _currentUserKey.OpenSubKey(@"Control Panel\Keyboard", true).SetValue("KeyboardDelay", _value, RegistryValueKind.String);
                            break;
                        }
                    case 3:
                        {
                            SystemParametersInfo(SPI_SETKEYBOARDSPEED, _value, _value, 2);
                            _currentUserKey.OpenSubKey(@"Control Panel\Keyboard", true).SetValue("KeyboardSpeed", _value, RegistryValueKind.String);
                            break;
                        }
                    case 4:
                        {
                            if (_choose)
                            {
                                _currentUserKey.OpenSubKey(@"Control Panel\Mouse", true).SetValue("MouseSpeed", 0, RegistryValueKind.String);
                                _currentUserKey.OpenSubKey(@"Control Panel\Mouse", true).SetValue("MouseThreshold1", 0, RegistryValueKind.String);
                                _currentUserKey.OpenSubKey(@"Control Panel\Mouse", true).SetValue("MouseThreshold2", 0, RegistryValueKind.String);
                            }
                            else
                            {
                                _currentUserKey.OpenSubKey(@"Control Panel\Mouse", true).SetValue("MouseSpeed", 1, RegistryValueKind.String);
                                _currentUserKey.OpenSubKey(@"Control Panel\Mouse", true).SetValue("MouseThreshold1", 6, RegistryValueKind.String);
                                _currentUserKey.OpenSubKey(@"Control Panel\Mouse", true).SetValue("MouseThreshold2", 10, RegistryValueKind.String);
                            }
                            break;
                        }
                    case 5:
                        {
                            Process _process = new Process();
                            _process.StartInfo.UseShellExecute = false;
                            _process.StartInfo.RedirectStandardOutput = true;
                            _process.StartInfo.CreateNoWindow = true;
                            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            _process.StartInfo.FileName = "cmd.exe";
                            _process.StartInfo.Arguments = string.Format("/c powercfg -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61");
                            _process.Start();
                            _process.Dispose();
                            Parallel.Invoke(() => { toastNotification.Show("Уведомление", "Схема электропитания «Максимальная производительность» добавлена", 0); });
                            break;
                        }
                    case 6:
                        {
                            if (_choose)
                                if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "10")
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.SecurityAndMaintenance").SetValue("Enabled", 0, RegistryValueKind.DWord);
                                else
                                    _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\PushNotifications").SetValue("ToastEnabled", 0, RegistryValueKind.DWord);
                            else
                                if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "10")
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.SecurityAndMaintenance").SetValue("Enabled", 1, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\PushNotifications").SetValue("ToastEnabled", 1, RegistryValueKind.DWord);
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                            break;
                        }
                    case 7:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Security").SetValue("DisableSecuritySettingsCheck", 1, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3").SetValue("1806", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Security").DeleteValue("DisableSecuritySettingsCheck");
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3").DeleteValue("1806");
                            }
                            break;
                        }
                    case 8:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Power\PowerSettings\7516b95f-f776-4464-8c53-06167f40cc99\8EC4B3A5-6868-48c2-BE75-4F3044BE88A7").SetValue("Attributes", 2, RegistryValueKind.DWord);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Power\PowerSettings\7516b95f-f776-4464-8c53-06167f40cc99\8EC4B3A5-6868-48c2-BE75-4F3044BE88A7").SetValue("Attributes", 1, RegistryValueKind.DWord);
                            break;
                        }
                    case 9:
                        {
                            TakingOwnership.GrantAdministratorsAccess(@"MACHINE\SYSTEM\CurrentControlSet\Services\wscsvc", TakingOwnership.SE_OBJECT_TYPE.SE_REGISTRY_KEY);
                            TakingOwnership.GrantAdministratorsAccess(@"MACHINE\SYSTEM\CurrentControlSet\Services\SecurityHealthService", TakingOwnership.SE_OBJECT_TYPE.SE_REGISTRY_KEY);

                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\wscsvc").SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\SecurityHealthService").SetValue("Start", 4, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer").SetValue("SmartScreenEnabled", "off", RegistryValueKind.String);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\PhishingFilter").SetValue("EnabledV9", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System").SetValue("EnableSmartScreen", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender").SetValue("DisableAntiSpyware", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\SmartScreen").SetValue("ConfigureAppInstallControl", "Anywhere", RegistryValueKind.String);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\SmartScreen").SetValue("ConfigureAppInstallControlEnabled", 1, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AppHost").SetValue("EnableWebContentEvaluation", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\DeviceGuard").SetValue("EnableVirtualizationBasedSecurity", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\wscsvc").SetValue("Start", 2, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\SecurityHealthService").SetValue("Start", 3, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer").DeleteValue("SmartScreenEnabled");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\MicrosoftEdge\PhishingFilter").DeleteValue("EnabledV9");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System").DeleteValue("EnableSmartScreen");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender").DeleteValue("DisableAntiSpyware");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\SmartScreen").DeleteValue("ConfigureAppInstallControl");
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\SmartScreen").DeleteValue("ConfigureAppInstallControlEnabled");
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\AppHost").DeleteValue("EnableWebContentEvaluation");
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\DeviceGuard").DeleteValue("EnableVirtualizationBasedSecurity");
                            }
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                            break;
                        }
                    case 10:
                        {
                            if (_choose)
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("ConsentPromptBehaviorAdmin", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("EnableInstallerDetection", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("EnableLUA", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("EnableSecureUIAPaths", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("EnableVirtualization", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("FilterAdministratorToken", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("PromptOnSecureDesktop", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("ConsentPromptBehaviorAdmin", 5, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("EnableInstallerDetection", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("EnableLUA", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("EnableSecureUIAPaths", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("EnableVirtualization", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("FilterAdministratorToken", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System").SetValue("PromptOnSecureDesktop", 1, RegistryValueKind.DWord);
                            }
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                            break;
                        }
                    case 11:
                        {
                            string _state = default;
                            if (_choose)
                            {
                                _state = "/disable";
                                _countTaskSystem = 0;
                            }
                            else
                            {
                                _state = "/enable";
                                _countTaskSystem = 2;
                            }

                            string[] TaskName = new string[2] { @"""Microsoft\Windows\MemoryDiagnostic\ProcessMemoryDiagnosticEvents""", @"""Microsoft\Windows\MemoryDiagnostic\RunFullMemoryDiagnostic""" };

                            Process _process = new Process();
                            _process.StartInfo.UseShellExecute = false;
                            _process.StartInfo.RedirectStandardOutput = true;
                            _process.StartInfo.CreateNoWindow = true;
                            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            _process.StartInfo.FileName = "cmd.exe";
                            foreach (var _task in TaskName)
                            {
                                Parallel.Invoke(() =>
                                {
                                    _process.StartInfo.Arguments = string.Format(@"/c schtasks /change /tn {0} {1}", _task, _state);
                                    _process.Start();
                                    _process.StandardOutput.ReadLine();
                                });
                            }
                            _process.Dispose();
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                            break;
                        }
                    case 12:
                        {
                            string[] ProtocolName = new string[6];
                            if (_choose)
                            {
                                string[] protocolDisabled = {  @"netsh interface teredo set state disabled", @"netsh interface isatap set state disabled",
                                @"netsh int ipv6 isatap set state disabled", @"netsh int ipv6 6to4 set state disabled", @"netsh interface IPV6 set global randomizeidentifier=disabled", @"netsh interface IPV6 set privacy state=disabled" };
                                ProtocolName = protocolDisabled;
                                _countProtocolSystem = 0;
                            }
                            else
                            {
                                string[] protocolReset = {  @"netsh interface teredo set state default", @"netsh interface isatap set state default",
                                @"netsh int ipv6 isatap set state default", @"netsh int ipv6 6to4 set state default", @"netsh interface IPV6 set global randomizeidentifier=enabled", @"netsh interface IPV6 set privacy state=enabled" };
                                ProtocolName = protocolReset;
                                _countProtocolSystem = 2;
                            }

                            Process _process = new Process();
                            _process.StartInfo.UseShellExecute = false;
                            _process.StartInfo.RedirectStandardOutput = true;
                            _process.StartInfo.CreateNoWindow = true;
                            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            _process.StartInfo.FileName = "cmd.exe";
                            foreach (var _protocol in ProtocolName)
                            {
                                Parallel.Invoke(() =>
                                {
                                    _process.StartInfo.Arguments = string.Format(@"/c {0}", _protocol);
                                    _process.Start();
                                    _process.StandardOutput.ReadLine();
                                });
                            }
                            _process.Dispose();
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                            break;
                        }
                    case 13:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management").SetValue("LargeSystemCache", 1, RegistryValueKind.DWord);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management").SetValue("LargeSystemCache", 0, RegistryValueKind.DWord);
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                            break;
                        }
                    case 14:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize").SetValue("Startupdelayinmsec", 0, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize").DeleteValue("Startupdelayinmsec");
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                            break;
                        }
                    case 15:
                        {
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer").SetValue("ShowFrequent", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer").SetValue("ShowRecent", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("Start_TrackDocs", 0, RegistryValueKind.DWord);
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("Start_TrackProgs", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer").DeleteValue("ShowFrequent");
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer").DeleteValue("ShowRecent");
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").DeleteValue("Start_TrackDocs");
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").DeleteValue("Start_TrackProgs");
                            }
                            break;
                        }
                    case 16:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers").SetValue("DisableAutoplay", 1, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers").SetValue("DisableAutoplay", 0, RegistryValueKind.DWord);
                            break;
                        }
                }
            }
            catch { };
        }
        #endregion

        #region More
        internal void VerificationWindows()
        {
            Process _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _process.StartInfo.FileName = "powershell";
            _process.StartInfo.Arguments = @"Get - CimInstance SoftwareLicensingProduct - Filter “Name like 'Windows%'” | where { $_.PartialProductKey } | select LicenseStatus";
            _process.Start();
            _process.StandardOutput.ReadLine();
            string _tbl = _process.StandardOutput.ReadToEnd();
            if (_tbl.Contains("1"))
            {
                _verificationW = 1;
                _process.Dispose();
            }
            else
            {
                _verificationW = 0;
                _process.Dispose();
            }
        }

        internal void GetSettingMore(MorePage _morePage)
        {
            try
            {
                //#2
                RegistryKey _sound = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e96c-e325-11ce-bfc1-08002be10318}", true);
                foreach (string Keyname in _sound.GetSubKeyNames())
                {
                    RegistryKey key = _sound?.OpenSubKey(Keyname);
                    if (key?.GetValue("DriverDesc")?.ToString() == "Realtek High Definition Audio")
                    {
                        RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e96c-e325-11ce-bfc1-08002be10318}\" + Keyname + @"\PowerSettings", true);
                        byte[] _ConservationIdleTime = (byte[])reg.GetValue(@"ConservationIdleTime");
                        byte[] _IdlePowerState = (byte[])reg.GetValue(@"IdlePowerState");
                        byte[] _PerformanceIdleTime = (byte[])reg.GetValue(@"PerformanceIdleTime");

                        if (_ConservationIdleTime[0].ToString() != "255" || _IdlePowerState[0].ToString() != "0" || _PerformanceIdleTime[0].ToString() != "255")
                        {
                            _morePage.TButton2.State = true;
                            _morePage.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                        }
                        else
                        {
                            _morePage.TButton2.State = false;
                            _morePage.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                        }
                    }
                }
            }
            catch { }

            //#3
            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) == "11")
            {
                _key[224] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

                if (_key[224] == null || _key[224].GetValue("TaskbarAl", null) == null || _key[224].GetValue("TaskbarAl").ToString() != "0")
                {
                    _morePage.TButton3.State = true;
                    _morePage.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
                }
                else
                {
                    _morePage.TButton3.State = false;
                    _morePage.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
                }
            }

            //#4
            _key[225] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

            if (_key[225] == null || _key[225].GetValue("EnableTransparency", null) == null || _key[225].GetValue("EnableTransparency").ToString() != "0")
            {
                _morePage.TButton4.State = true;
                _morePage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton4.State = false;
                _morePage.Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#5
            _key[226] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

            if (_key[226] == null || _key[226].GetValue("SystemUsesLightTheme", null) == null || _key[226].GetValue("SystemUsesLightTheme").ToString() != "1")
            {
                _morePage.TButton5.State = true;
                _morePage.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton5.State = false;
                _morePage.Tweak5.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#6
            _key[227] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

            if (_key[227] == null || _key[227].GetValue("AppsUseLightTheme", null) == null || _key[227].GetValue("AppsUseLightTheme").ToString() != "1")
            {
                _morePage.TButton6.State = true;
                _morePage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton6.State = false;
                _morePage.Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#7
            _key[228] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Power");
            _key[229] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Power");

            if (_key[228] == null || _key[228].GetValue("HiberbootEnabled", null) == null || _key[228].GetValue("HiberbootEnabled").ToString() != "0" || _key[229] == null || _key[229].GetValue("HibernateEnabled", null) == null || _key[229].GetValue("HibernateEnabled").ToString() != "0")
            {
                _morePage.TButton7.State = true;
                _morePage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton7.State = false;
                _morePage.Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#8
            _key[230] = _currentUserKey.OpenSubKey(@"Control Panel\Accessibility\StickyKeys");

            if (_key[230] == null || _key[230].GetValue("Flags", null) == null || _key[230].GetValue("Flags").ToString() != "506")
            {
                _morePage.TButton8.State = true;
                _morePage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton8.State = false;
                _morePage.Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#9
            _key[231] = _currentUserKey.OpenSubKey(@"Control Panel\Accessibility\ToggleKeys");

            if (_key[231] == null || _key[231].GetValue("Flags", null) == null || _key[231].GetValue("Flags").ToString() != "62")
            {
                _morePage.TButton9.State = true;
                _morePage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton9.State = false;
                _morePage.Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#10
            _key[232] = _localMachineKey.OpenSubKey(@"SOFTWARE\Policies\Microsoft\WindowsStore");

            if (_key[232] == null || _key[232].GetValue("AutoDownload", null) == null || _key[232].GetValue("AutoDownload").ToString() != "2")
            {
                _morePage.TButton10.State = true;
                _morePage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton10.State = false;
                _morePage.Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#11
            _key[233] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[233] == null || _key[233].GetValue("AutoEndTasks", null) == null || _key[233].GetValue("AutoEndTasks").ToString() != "1")
            {
                _morePage.TButton11.State = true;
                _morePage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton11.State = false;
                _morePage.Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#12
            _key[234] = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control");

            if (_key[234] == null || _key[234].GetValue("WaitToKillServiceTimeout", null) == null || _key[234].GetValue("WaitToKillServiceTimeout").ToString() != "2000")
            {
                _morePage.TButton12.State = true;
                _morePage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton12.State = false;
                _morePage.Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#13
            _key[235] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[235] == null || _key[235].GetValue("JPEGImportQuality", null) == null || _key[235].GetValue("JPEGImportQuality").ToString() != "100")
            {
                _morePage.TButton13.State = true;
                _morePage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton13.State = false;
                _morePage.Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#14
            _key[236] = _currentUserKey.OpenSubKey(@"Control Panel\Desktop");

            if (_key[236] == null || _key[236].GetValue("HungAppTimeout", null) == null || _key[236].GetValue("HungAppTimeout").ToString() != "1000")
            {
                _morePage.TButton14.State = true;
                _morePage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton14.State = false;
                _morePage.Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#15
            _key[237] = _currentUserKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");

            if (_key[237] == null || _key[237].GetValue("ShowInfoTip", null) == null || _key[237].GetValue("ShowInfoTip").ToString() != "0")
            {
                _morePage.TButton15.State = true;
                _morePage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                _morePage.TButton15.State = false;
                _morePage.Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        internal void ChangeSettingMore(in bool _choose, in byte _select)
        {
            try
            {
                switch (_select)
                {
                    case 1:
                        {
                            Parallel.Invoke(() => { toastNotification.Show("Информация", "Процесс активации Windows начался, это займет некоторое время", 0); });

                            Parallel.Invoke(() =>
                            {
                                string[] _kmsKeyW = new string[8] { @"TX9XD-98N7V-6WMQ6-BX7FG-H8Q99", @"7HNRX-D7KGG-3K4RQ-4WPJ4-YTDFH", @"72RPG-7NV8T-TVQKR-7RRRW-78RBY",
                                @"ND4DX-39KJY-FYWQ9-X6XKT-VCFCF", @"7YMNV-PG77F-K66KT-KG9VQ-TCQGB", @"KTNPV-KTRK4-3RRR8-39X6W-W44T3", @"BT79Q-G7N6G-PGBYW-4YWX6-6F4BT", @"8N67H-M3CY9-QT7C4-2TR7M-TXYCV"};
                                int _index = default;

                                if (CheckWindowsVersion._wedition.Contains("Home"))
                                    _index = 0;
                                else if (CheckWindowsVersion._wedition.Contains("Home Single Language"))
                                    _index = 2;
                                else if (CheckWindowsVersion._wedition.Contains("Education"))
                                    _index = 3;
                                else if (CheckWindowsVersion._wedition.Contains("Enterprise"))
                                    _index = 4;
                                else if (CheckWindowsVersion._wedition.Contains("Enterprise LSTB"))
                                    _index = 5;
                                else if (CheckWindowsVersion._wedition.Contains("Core"))
                                    _index = 6;
                                else if (CheckWindowsVersion._wedition.Contains("Core Single Language"))
                                    _index = 7;
                                else if (CheckWindowsVersion._wedition.Contains("Pro"))
                                    _index = 8;

                                string _kmsName = default;

                                if (CheckWindowsVersion._wedition.Contains("Pro"))
                                    _kmsName = "slmgr /skms kms.xspace.in";
                                else
                                    _kmsName = "slmgr /skms kms.digiboy.ir";

                                string _kmsActivated = "slmgr /skms zh.us.to";


                                Process _process = new Process();
                                _process.StartInfo.UseShellExecute = false;
                                _process.StartInfo.RedirectStandardOutput = true;
                                _process.StartInfo.CreateNoWindow = true;
                                _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                _process.Exited += new EventHandler(Process_Exited);
                                _process.StartInfo.FileName = "cmd";
                                _process.StartInfo.Arguments = string.Format("/c slmgr /ipk {0} && {1} && {2}", _kmsKeyW[_index], _kmsName, _kmsActivated);
                                _process.Start();

                                void Process_Exited(object sender, EventArgs e)
                                {
                                    Parallel.Invoke(() =>
                                    {
                                        _process = new Process();
                                        _process.StartInfo.UseShellExecute = false;
                                        _process.StartInfo.RedirectStandardOutput = true;
                                        _process.StartInfo.CreateNoWindow = true;
                                        _process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                                        _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                        _process.StartInfo.FileName = "powershell";
                                        _process.StartInfo.Arguments = @"Get - CimInstance SoftwareLicensingProduct - Filter “Name like 'Windows%'” | where { $_.PartialProductKey } | select LicenseStatus";
                                        _process.Start();
                                        _process.StandardOutput.ReadLine();
                                        string _tbl = _process.StandardOutput.ReadToEnd();
                                        if (_tbl.Contains("1"))
                                        {
                                            _verificationW = 1;
                                            _process.Dispose();
                                            Parallel.Invoke(() => { toastNotification.Show("Информация", "Активация Windows прошла успешно", 0); });
                                        }
                                        else
                                        {
                                            _verificationW = 0;
                                            _process.Dispose();
                                            Parallel.Invoke(() => { toastNotification.Show("Информация", "Не удалось активировать Windows", 0); });
                                        }
                                    });
                                }
                            });
                            break;
                        }
                    case 2:
                        {        
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходима перезагрузка, нажмите на данный текст, чтобы произвести её", 2); });
                            if (_choose)
                            {
                                byte[] _data = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };

                                RegistryKey _key = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e96c-e325-11ce-bfc1-08002be10318}", true);

                                foreach (string Keyname in _key.GetSubKeyNames())
                                {
                                    RegistryKey key = _key?.OpenSubKey(Keyname);
                                    if (key?.GetValue("DriverDesc")?.ToString() == "Realtek High Definition Audio")
                                    {

                                        RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e96c-e325-11ce-bfc1-08002be10318}\" + Keyname + @"\PowerSettings", true);
                                        reg.SetValue("ConservationIdleTime", _data, RegistryValueKind.Binary);
                                        reg.SetValue("IdlePowerState", Encoding.Unicode.GetBytes("\0\0"), RegistryValueKind.Binary);
                                        reg.SetValue("PerformanceIdleTime", _data, RegistryValueKind.Binary);
                                    }
                                }
                            }
                            else
                            {
                                byte[] _data0 = new byte[] { 0x0a, 0x00, 0x00, 0x00 };
                                byte[] _data1 = new byte[] { 0x03, 0x00, 0x00, 0x00 };

                                RegistryKey _key = _localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e96c-e325-11ce-bfc1-08002be10318}", true);

                                foreach (string Keyname in _key.GetSubKeyNames())
                                {
                                    RegistryKey key = _key?.OpenSubKey(Keyname);
                                    if (key?.GetValue("DriverDesc")?.ToString() == "Realtek High Definition Audio")
                                    {

                                        RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e96c-e325-11ce-bfc1-08002be10318}\" + Keyname + @"\PowerSettings", true);
                                        reg.SetValue("ConservationIdleTime", _data0, RegistryValueKind.Binary);
                                        reg.SetValue("IdlePowerState", _data1, RegistryValueKind.Binary);
                                        reg.SetValue("PerformanceIdleTime", _data0, RegistryValueKind.Binary);
                                    }
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("TaskbarAl", 0, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("TaskbarAl", 1, RegistryValueKind.DWord);
                            break;
                        }
                    case 4:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").SetValue("EnableTransparency", 0, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").SetValue("EnableTransparency", 1, RegistryValueKind.DWord);
                            break;
                        }
                    case 5:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").SetValue("SystemUsesLightTheme", 1, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").SetValue("SystemUsesLightTheme", 0, RegistryValueKind.DWord);
                            break;
                        }
                    case 6:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").SetValue("AppsUseLightTheme", 1, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").SetValue("AppsUseLightTheme", 0, RegistryValueKind.DWord);
                            break;
                        }
                    case 7:
                        {
                            if (_choose)
                            {

                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Power").SetValue("HiberbootEnabled", 0, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Power").SetValue("HibernateEnabled", 0, RegistryValueKind.DWord);
                            }
                            else
                            {

                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Power").SetValue("HiberbootEnabled", 1, RegistryValueKind.DWord);
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Power").SetValue("HibernateEnabled", 1, RegistryValueKind.DWord);
                            }
                            break;
                        }
                    case 8:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Control Panel\Accessibility\StickyKeys").SetValue("Flags", "506", RegistryValueKind.String);
                            else
                                _currentUserKey.CreateSubKey(@"Control Panel\Accessibility\StickyKeys").SetValue("Flags", "26", RegistryValueKind.String);
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            break;
                        }
                    case 9:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Control Panel\Accessibility\ToggleKeys").SetValue("Flags", "62", RegistryValueKind.String);
                            else
                                _currentUserKey.CreateSubKey(@"Control Panel\Accessibility\ToggleKeys").SetValue("Flags", "63", RegistryValueKind.String);
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            break;
                        }
                    case 10:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SOFTWARE\Policies\Microsoft\WindowsStore").SetValue("AutoDownload", 2, RegistryValueKind.DWord);
                            else
                                _localMachineKey.DeleteSubKey(@"SOFTWARE\Policies\Microsoft\WindowsStore");
                            break;
                        }
                    case 11:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").SetValue("AutoEndTasks", "1", RegistryValueKind.String);
                            else
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").DeleteValue("AutoEndTasks");
                            break;
                        }
                    case 12:
                        {
                            if (_choose)
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control").SetValue("WaitToKillServiceTimeout", "2000", RegistryValueKind.String);
                            else
                                _localMachineKey.CreateSubKey(@"SYSTEM\CurrentControlSet\Control").SetValue("WaitToKillServiceTimeout", "5000", RegistryValueKind.String);
                            break;
                        }
                    case 13:
                        {
                            if (_choose)
                            {
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").SetValue("JPEGImportQuality", 100, RegistryValueKind.DWord);
                                Parallel.Invoke(() => { toastNotification.Show("Информация", "Установите нужные обои формата JPEG, чтобы убедиться", 0); });
                            }
                            else
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").DeleteValue("JPEGImportQuality");
                            break;
                        }
                    case 14:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").SetValue("HungAppTimeout", "1000", RegistryValueKind.String);
                            else
                                _currentUserKey.CreateSubKey(@"Control Panel\Desktop").DeleteValue("HungAppTimeout");
                            break;
                        }
                    case 15:
                        {
                            if (_choose)
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("ShowInfoTip", 0, RegistryValueKind.DWord);
                            else
                                _currentUserKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced").SetValue("ShowInfoTip", 1, RegistryValueKind.DWord);
                            Parallel.Invoke(() => { toastNotification.Show("Внимание", "Необходим выход, нажмите на данный текст, чтобы произвести его", 1); });
                            break;
                        }

                }
            }
            catch { };
        }
        #endregion
    }
}
