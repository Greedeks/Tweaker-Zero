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
    internal class ApplicationsSystem
    {
        private static List<int> _CountCheck = new List<int> (26);
        private static string _result = default;
        internal void CheckInstalledApps()
        {
            Process process = Process.Start(new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "powershell.exe",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.GetEncoding(866),
                WindowStyle = ProcessWindowStyle.Hidden
            });
            process.StartInfo.Arguments = string.Format(@"Get-AppxPackage | select Name");
            process.Start();
            process.StandardOutput.ReadLine();
            _result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();


            _CountCheck.Add(_result.Split(new string[] { "Microsoft.WindowsStore" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.Todos" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.BingWeather" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.Microsoft3DViewer" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.ZuneMusic" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.GetHelp" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.MicrosoftOfficeHub" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.MicrosoftSolitaireCollection" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.MixedReality.Portal" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.XboxApp" }, StringSplitOptions.None).Count() - 1 + 
                _result.Split(new string[] { "Microsoft.GamingApp" }, StringSplitOptions.None).Count() - 1 + _result.Split(new string[] { "Microsoft.Xbox.TCUI" }, StringSplitOptions.None).Count() - 1 +
                _result.Split(new string[] { "Microsoft.XboxGameOverlay" }, StringSplitOptions.None).Count() - 1+ _result.Split(new string[] { "Microsoft.XboxGamingOverlay" }, StringSplitOptions.None).Count() - 1
                + _result.Split(new string[] { "Microsoft.XboxIdentityProvider" }, StringSplitOptions.None).Count() - 1 + _result.Split(new string[] { "Microsoft.XboxSpeechToTextOverlay" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.MSPaint" }, StringSplitOptions.None).Count() - 1 + _result.Split(new string[] { "Microsoft.Paint3D" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.Office.OneNote" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.People" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.MicrosoftStickyNotes" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "MicrosoftWindows.Client.WebExperience" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.ScreenSketch" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.YourPhone" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.Windows.Photos" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.WindowsFeedbackHub" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.WindowsSoundRecorder" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.WindowsAlarms" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.SkypeApp" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.WindowsMaps" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.WindowsCamera" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.ZuneVideo" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.BingNews" }, StringSplitOptions.None).Count() - 1);

            _CountCheck.Add(_result.Split(new string[] { "Microsoft.windowscommunicationsapps" }, StringSplitOptions.None).Count() - 1);
        }

        internal void SetImageApps(ApplicationsUL applicationsPages)
        {
            applicationsPages.MicrosoftStore.Source =  _CountCheck[0] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftStoreImage"] : (DrawingImage)Application.Current.Resources["MicrosoftStoreImageU"];
            applicationsPages.Todos.Source = _CountCheck[1] == 1 ? (DrawingImage)Application.Current.Resources["TodosImage"] : (DrawingImage)Application.Current.Resources["TodosImageU"];
            applicationsPages.BingWeather.Source = _CountCheck[2] == 1 ? (DrawingImage)Application.Current.Resources["BingWeatherImage"] : (DrawingImage)Application.Current.Resources["BingWeatherImageU"];
            applicationsPages.Microsoft3DViewer.Source = _CountCheck[3] == 1 ? (DrawingImage)Application.Current.Resources["Microsoft3DViewerImage"] : (DrawingImage)Application.Current.Resources["Microsoft3DViewerImageU"];
            applicationsPages.ZoneMusic.Source = _CountCheck[4] == 1 ? (DrawingImage)Application.Current.Resources["MusicImage"] : (DrawingImage)Application.Current.Resources["MusicImageU"];
            applicationsPages.GetHelp.Source = _CountCheck[5] == 1 ? (DrawingImage)Application.Current.Resources["GetHelpImage"] : (DrawingImage)Application.Current.Resources["GetHelpImageU"];
            applicationsPages.MicrosoftOfficeHub.Source = _CountCheck[6] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftOfficeHubImage"] : (DrawingImage)Application.Current.Resources["MicrosoftOfficeHubImageU"];
            applicationsPages.MicrosoftSolitaireCollection.Source = _CountCheck[7] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftSolitaireCollectionImage"] : (DrawingImage)Application.Current.Resources["MicrosoftSolitaireCollectionImageU"];
            applicationsPages.MixedReality.Source = _CountCheck[8] == 1 ? (DrawingImage)Application.Current.Resources["MixedRealityImage"] : (DrawingImage)Application.Current.Resources["MixedRealityImageU"];
            applicationsPages.Xbox.Source = _CountCheck[9] >=1 ? (DrawingImage)Application.Current.Resources["XboxImage"] : (DrawingImage)Application.Current.Resources["XboxImageU"];
            applicationsPages.Paint3D.Source = _CountCheck[10] >= 1 ? (DrawingImage)Application.Current.Resources["Paint3DImage"] : (DrawingImage)Application.Current.Resources["Paint3DImageU"];
            applicationsPages.OneNote.Source = _CountCheck[11] == 1 ? (DrawingImage)Application.Current.Resources["OneNoteImage"] : (DrawingImage)Application.Current.Resources["OneNoteImageU"];
            applicationsPages.People.Source = _CountCheck[12] == 1 ? (DrawingImage)Application.Current.Resources["PeopleImage"] : (DrawingImage)Application.Current.Resources["PeopleImageU"];
            applicationsPages.MicrosoftStickyNotes.Source = _CountCheck[13] == 1 ? (DrawingImage)Application.Current.Resources["MicrosoftStickyNotesImage"] : (DrawingImage)Application.Current.Resources["MicrosoftStickyNotesImageU"];
            applicationsPages.Widgets.Source = _CountCheck[14] == 1 ? (DrawingImage)Application.Current.Resources["WidgetsImage"] : (DrawingImage)Application.Current.Resources["WidgetsImageU"];
            applicationsPages.ScreenSketch.Source = _CountCheck[15] == 1 ? (DrawingImage)Application.Current.Resources["ScreenSketchImage"] : (DrawingImage)Application.Current.Resources["ScreenSketchImageU"];
            applicationsPages.Phone.Source = _CountCheck[16] == 1 ? (DrawingImage)Application.Current.Resources["PhoneImage"] : (DrawingImage)Application.Current.Resources["PhoneImageU"];
            applicationsPages.Photos.Source = _CountCheck[17] == 1 ? (DrawingImage)Application.Current.Resources["PhotosImage"] : (DrawingImage)Application.Current.Resources["PhotosImageU"];
            applicationsPages.FeedbackHub.Source = _CountCheck[18] == 1 ? (DrawingImage)Application.Current.Resources["FeedbackHubImage"] : (DrawingImage)Application.Current.Resources["FeedbackHubImageU"];
            applicationsPages.SoundRecorder.Source = _CountCheck[19] == 1 ? (DrawingImage)Application.Current.Resources["SoundRecorderImage"] : (DrawingImage)Application.Current.Resources["SoundRecorderImageU"];
            applicationsPages.Alarms.Source = _CountCheck[20] == 1 ? (DrawingImage)Application.Current.Resources["AlarmsImage"] : (DrawingImage)Application.Current.Resources["AlarmsImageU"];
            applicationsPages.SkypeApp.Source = _CountCheck[21] == 1 ? (DrawingImage)Application.Current.Resources["SkypeAppImage"] : (DrawingImage)Application.Current.Resources["SkypeAppImageU"];
            applicationsPages.Maps.Source = _CountCheck[22] == 1 ? (DrawingImage)Application.Current.Resources["MapsImage"] : (DrawingImage)Application.Current.Resources["MapsImageU"];
            applicationsPages.Camera.Source = _CountCheck[23] == 1 ? (DrawingImage)Application.Current.Resources["CameraImage"] : (DrawingImage)Application.Current.Resources["CameraImageU"];
            applicationsPages.ZoneVideo.Source = _CountCheck[24] == 1 ? (DrawingImage)Application.Current.Resources["VideoImage"] : (DrawingImage)Application.Current.Resources["VideoImageU"];
            applicationsPages.BingNews.Source = _CountCheck[25] == 1 ? (DrawingImage)Application.Current.Resources["BingNewsImage"] : (DrawingImage)Application.Current.Resources["BingNewsImageU"];
            applicationsPages.Mail.Source = _CountCheck[26] == 1 ? (DrawingImage)Application.Current.Resources["MailImage"] : (DrawingImage)Application.Current.Resources["MailImageU"];
        }
    }
}
