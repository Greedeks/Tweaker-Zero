using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweaker.Pages;

namespace Tweaker.Сlasses
{
    internal class ApplicationsSystem
    {
        private static List<int> _CountCheck = new List<int> ();
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
            process.StartInfo.Arguments = String.Format(@"Get-AppxPackage | select Name");
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

        }
    }
}
