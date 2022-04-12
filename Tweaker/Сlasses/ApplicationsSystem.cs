using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Tweaker.Pages;

namespace Tweaker.Сlasses
{
    internal sealed class ApplicationsSystem
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private static Dictionary<byte, byte> _CountCheck = new Dictionary<byte, byte>(29)
        {
            [0] = 0,
            [1] = 0,
            [2] = 0,
            [3] = 0,
            [4] = 0,
            [5] = 0,
            [6] = 0,
            [7] = 0,
            [8] = 0,
            [9] = 0,
            [10] = 0,
            [11] = 0,
            [12] = 0,
            [13] = 0,
            [14] = 0,
            [15] = 0,
            [16] = 0,
            [17] = 0,
            [18] = 0,
            [19] = 0,
            [20] = 0,
            [21] = 0,
            [22] = 0,
            [23] = 0,
            [24] = 0,
            [25] = 0,
            [26] = 0,
            [27] = 0,
            [28] = 0,
        };

        private static string _result = default;
        private Process _process;
        private readonly Dictionary<string, List<string>> _appValue = new Dictionary<string, List<string>>(29)
        {
            ["MicrosoftStore"] = new List<string>(1) { "Microsoft.WindowsStore" },
            ["Todos"] = new List<string>(1) { "Microsoft.Todos" },
            ["BingWeather"] = new List<string>(1) { "Microsoft.BingWeather" },
            ["Microsoft3DViewer"] = new List<string>(1) { "Microsoft.Microsoft3DViewer" },
            ["ZoneMusic"] = new List<string>(1) { "Microsoft.ZuneMusic" },
            ["GetHelp"] = new List<string>(1) { "Microsoft.GetHelp" },
            ["MicrosoftOfficeHub"] = new List<string>(1) { "Microsoft.MicrosoftOfficeHub" },
            ["MicrosoftSolitaireCollection"] = new List<string>(1) { "Microsoft.MicrosoftSolitaireCollection" },
            ["MixedReality"] = new List<string>(1) { "Microsoft.MixedReality.Portal" },
            ["Xbox"] = new List<string>(7) { "Microsoft.XboxApp", "Microsoft.GamingApp", "Microsoft.XboxGamingOverlay", "Microsoft.XboxGameOverlay", "Microsoft.XboxIdentityProvider", "Microsoft.Xbox.TCUI", "Microsoft.XboxSpeechToTextOverlay" },
            ["Paint3D"] = new List<string>(2) { "Microsoft.MSPaint", "Microsoft.Paint3D" },
            ["OneNote"] = new List<string>(1) { "Microsoft.Office.OneNote" },
            ["People"] = new List<string>(1) { "Microsoft.People" },
            ["MicrosoftStickyNotes"] = new List<string>(1) { "Microsoft.MicrosoftStickyNotes" },
            ["Widgets"] = new List<string>(1) { "MicrosoftWindows.Client.WebExperience" },
            ["ScreenSketch"] = new List<string>(1) { "Microsoft.ScreenSketch" },
            ["Phone"] = new List<string>(1) { "Microsoft.YourPhone" },
            ["Photos"] = new List<string>(1) { "Microsoft.Windows.Photos" },
            ["FeedbackHub"] = new List<string>(1) { "Microsoft.WindowsFeedbackHub" },
            ["SoundRecorder"] = new List<string>(1) { "Microsoft.WindowsSoundRecorder" },
            ["Alarms"] = new List<string>(1) { "Microsoft.WindowsAlarms" },
            ["SkypeApp"] = new List<string>(1) { "Microsoft.SkypeApp" },
            ["Maps"] = new List<string>(1) { "Microsoft.WindowsMaps" },
            ["Camera"] = new List<string>(1) { "Microsoft.WindowsCamera" },
            ["ZuneVideo"] = new List<string>(1) { "Microsoft.ZuneVideo" },
            ["BingNews"] = new List<string>(1) { "Microsoft.BingNews" },
            ["Mail"] = new List<string>(1) { "Microsoft.windowscommunicationsapps" },
            ["MicrosoftTeams"] = new List<string>(1) { "MicrosoftTeams" },
            ["PoweraAtomateDesktop"] = new List<string>(1) { "Microsoft.PowerAutomateDesktop" }
        };

        internal void CheckInstalledApps()
        {
            _process = Process.Start(new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "powershell.exe",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                Arguments = string.Format(@"Get-AppxPackage | select Name"),
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.GetEncoding(866),
                WindowStyle = ProcessWindowStyle.Hidden
            });
            _result = _process.StandardOutput.ReadToEnd();
            _process.WaitForExit();
            _process.Dispose();

            byte _count = 0;
            foreach (var _appNm in _appValue)
            {
                byte _value = default;
                foreach (var _appVl in _appNm.Value)
                {
                    _value += Convert.ToByte(_result.Split(new string[] { _appVl }, StringSplitOptions.None).Count() - 1);
                }
                _CountCheck[_count] = _value;
                _count++;
            }
        }

        internal void SetImageApps(in ApplicationsUL _applicationsPages)
        {
            _applicationsPages.MicrosoftStore.Source =  _CountCheck[0] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftStoreImage"] : (DrawingImage)Application.Current.Resources["MicrosoftStoreImageU"];
            _applicationsPages.Todos.Source = _CountCheck[1] == 1 ? (DrawingImage)Application.Current.Resources["TodosImage"] : (DrawingImage)Application.Current.Resources["TodosImageU"];
            _applicationsPages.BingWeather.Source = _CountCheck[2] == 1 ? (DrawingImage)Application.Current.Resources["BingWeatherImage"] : (DrawingImage)Application.Current.Resources["BingWeatherImageU"];
            _applicationsPages.Microsoft3DViewer.Source = _CountCheck[3] == 1 ? (DrawingImage)Application.Current.Resources["Microsoft3DViewerImage"] : (DrawingImage)Application.Current.Resources["Microsoft3DViewerImageU"];
            _applicationsPages.ZuneMusic.Source = _CountCheck[4] == 1 ? (DrawingImage)Application.Current.Resources["MusicImage"] : (DrawingImage)Application.Current.Resources["MusicImageU"];
            _applicationsPages.GetHelp.Source = _CountCheck[5] == 1 ? (DrawingImage)Application.Current.Resources["GetHelpImage"] : (DrawingImage)Application.Current.Resources["GetHelpImageU"];
            _applicationsPages.MicrosoftOfficeHub.Source = _CountCheck[6] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftOfficeHubImage"] : (DrawingImage)Application.Current.Resources["MicrosoftOfficeHubImageU"];
            _applicationsPages.MicrosoftSolitaireCollection.Source = _CountCheck[7] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftSolitaireCollectionImage"] : (DrawingImage)Application.Current.Resources["MicrosoftSolitaireCollectionImageU"];
            _applicationsPages.MixedReality.Source = _CountCheck[8] == 1 ? (DrawingImage)Application.Current.Resources["MixedRealityImage"] : (DrawingImage)Application.Current.Resources["MixedRealityImageU"];
            _applicationsPages.Xbox.Source = _CountCheck[9] >=1 ? (DrawingImage)Application.Current.Resources["XboxImage"] : (DrawingImage)Application.Current.Resources["XboxImageU"];
            _applicationsPages.Paint3D.Source = _CountCheck[10] >= 1 ? (DrawingImage)Application.Current.Resources["Paint3DImage"] : (DrawingImage)Application.Current.Resources["Paint3DImageU"];
            _applicationsPages.OneNote.Source = _CountCheck[11] == 1 ? (DrawingImage)Application.Current.Resources["OneNoteImage"] : (DrawingImage)Application.Current.Resources["OneNoteImageU"];
            _applicationsPages.People.Source = _CountCheck[12] == 1 ? (DrawingImage)Application.Current.Resources["PeopleImage"] : (DrawingImage)Application.Current.Resources["PeopleImageU"];
            _applicationsPages.MicrosoftStickyNotes.Source = _CountCheck[13] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftStickyNotesImage"] : (DrawingImage)Application.Current.Resources["MicrosoftStickyNotesImageU"];
            _applicationsPages.Widgets.Source = _CountCheck[14] == 1 ? (DrawingImage)Application.Current.Resources["WidgetsImage"] : (DrawingImage)Application.Current.Resources["WidgetsImageU"];
            _applicationsPages.ScreenSketch.Source = _CountCheck[15] == 1 ? (DrawingImage)Application.Current.Resources["ScreenSketchImage"] : (DrawingImage)Application.Current.Resources["ScreenSketchImageU"];
            _applicationsPages.Phone.Source = _CountCheck[16] == 1 ? (DrawingImage)Application.Current.Resources["PhoneImage"] : (DrawingImage)Application.Current.Resources["PhoneImageU"];
            _applicationsPages.Photos.Source = _CountCheck[17] == 1 ? (DrawingImage)Application.Current.Resources["PhotosImage"] : (DrawingImage)Application.Current.Resources["PhotosImageU"];
            _applicationsPages.FeedbackHub.Source = _CountCheck[18] == 1 ? (DrawingImage)Application.Current.Resources["FeedbackHubImage"] : (DrawingImage)Application.Current.Resources["FeedbackHubImageU"];
            _applicationsPages.SoundRecorder.Source = _CountCheck[19] == 1 ? (DrawingImage)Application.Current.Resources["SoundRecorderImage"] : (DrawingImage)Application.Current.Resources["SoundRecorderImageU"];
            _applicationsPages.Alarms.Source = _CountCheck[20] == 1 ? (DrawingImage)Application.Current.Resources["AlarmsImage"] : (DrawingImage)Application.Current.Resources["AlarmsImageU"];
            _applicationsPages.SkypeApp.Source = _CountCheck[21] == 1 ? (DrawingImage)Application.Current.Resources["SkypeAppImage"] : (DrawingImage)Application.Current.Resources["SkypeAppImageU"];
            _applicationsPages.Maps.Source = _CountCheck[22] == 1 ? (DrawingImage)Application.Current.Resources["MapsImage"] : (DrawingImage)Application.Current.Resources["MapsImageU"];
            _applicationsPages.Camera.Source = _CountCheck[23] == 1 ? (DrawingImage)Application.Current.Resources["CameraImage"] : (DrawingImage)Application.Current.Resources["CameraImageU"];
            _applicationsPages.ZuneVideo.Source = _CountCheck[24] == 1 ? (DrawingImage)Application.Current.Resources["VideoImage"] : (DrawingImage)Application.Current.Resources["VideoImageU"];
            _applicationsPages.BingNews.Source = _CountCheck[25] == 1 ? (DrawingImage)Application.Current.Resources["BingNewsImage"] : (DrawingImage)Application.Current.Resources["BingNewsImageU"];
            _applicationsPages.Mail.Source = _CountCheck[26] == 1 ? (DrawingImage)Application.Current.Resources["MailImage"] : (DrawingImage)Application.Current.Resources["MailImageU"];
            _applicationsPages.MicrosoftTeams.Source = _CountCheck[27] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftTeamsImage"] : (DrawingImage)Application.Current.Resources["MicrosoftTeamsImageU"];
            _applicationsPages.PoweraAtomateDesktop.Source = _CountCheck[28] == 1 ? (DrawingImage)Application.Current.Resources["PoweraAtomateDesktopImage"] : (DrawingImage)Application.Current.Resources["PoweraAtomateDesktopImageU"];
        }

        internal void ApplicationRemoval(in string _nameApp)
        {
            _process = new Process();
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            _process.StartInfo.FileName = "powershell.exe";
            foreach (var _appDelete in _appValue[_nameApp])
            {
                _process.StartInfo.Arguments = string.Format("Get-AppxPackage -Name "+_appDelete+" -AllUsers | Remove-AppxPackage");
                _process.Start();

                for (byte i = 0; i < _appValue.Count; i++)
                    if (_nameApp == _appValue.ElementAt(i).Key.ToString())
                        _CountCheck[i] = 0;
            }
            _process.WaitForExit();
            _process.Dispose();

            if (_nameApp == "Widgets")
                _settingsWindows.AppWidgetsState(false);

        }

        internal void ApplicationRecovery()
        {
            if (AppCheckCountRemoval() != 0)
            {
                _process = Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = false,
                    FileName = "powershell.exe",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    Arguments = @"Get-AppxPackage -AllUsers| Foreach {Add-AppxPackage -Register “$($_.InstallLocation)\AppXManifest.xml” -DisableDevelopmentMode}",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
                _process.Dispose();

                _settingsWindows.AppWidgetsState(true);
            }
        }

        internal void ApplicationRemovalAll()
        {
            if (AppCheckCountRemoval() != 0)
            {
                _process = new Process();
                _process.StartInfo.UseShellExecute = false;
                _process.StartInfo.RedirectStandardOutput = true;
                _process.StartInfo.CreateNoWindow = true;
                _process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                _process.StartInfo.FileName = "powershell.exe";
                foreach (var _appNm in _appValue)
                {
                    foreach (var _appDelete in _appNm.Value)
                    {
                        _process.StartInfo.Arguments = string.Format("Get-AppxPackage -Name " + _appDelete + " -AllUsers | Remove-AppxPackage");
                        _process.Start();

                        for (byte i = 0; i < _appValue.Count; i++)
                            if (_appDelete == _appValue.ElementAt(i).Key.ToString())
                                _CountCheck[i] = 0;
                    }
                }
                _process.Dispose();

                _settingsWindows.AppWidgetsState(false);
            }
        }

        internal byte AppCheckCountRemoval()
        {
            byte _appCheck = 0;
            foreach (var _countck in _CountCheck)
                _appCheck += _countck.Value;
            return _appCheck;
        }
    }
}
