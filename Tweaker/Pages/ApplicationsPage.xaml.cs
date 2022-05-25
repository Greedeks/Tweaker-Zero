using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class ApplicationsPage : Page
    {
        private readonly ApplicationsSystem _applicationsSystem = new ApplicationsSystem();
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private BackgroundWorker _worker;
        private string _nameApp = default;
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);

        public ApplicationsPage()
        {
            InitializeComponent();

            _applicationsSystem.SetImageApps(this);

            #region Update
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds % 5 == 0)
                {
                    _worker = new BackgroundWorker();
                    _worker.DoWork += Worker_DoWorkUpdate;
                    _worker.RunWorkerAsync();
                }
                else if (_time.TotalSeconds % 2 == 0) { _applicationsSystem.SetImageApps(this); }
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            #endregion
        }

        private Dictionary<string, string> TweaksHover = new Dictionary<string, string>
        {
            ["MicrosoftStore"] = "Microsoft Store",
            ["Todos"] = "Microsoft To Do",
            ["BingWeather"] = "Bing Weather",
            ["Microsoft3DViewer"] = "Microsoft 3D Viewer",
            ["Music"] = "Microsoft Zune Music",
            ["GetHelp"] = "Get Help",
            ["MicrosoftOfficeHub"] = "Microsoft Office Hub",
            ["MicrosoftSolitaireCollection"] = "Microsoft Solitaire Collection",
            ["MixedReality"] = "Windows Mixed Reality",
            ["Xbox"] = "Microsoft Xbox",
            ["Paint3D"] = "Microsoft Paint3D",
            ["OneNote"] = "Microsoft Office OneNote",
            ["People"] = "Microsoft People",
            ["MicrosoftStickyNotes"] = "Microsoft Sticky Notes",
            ["Widgets"] = "Виджеты (Windows 11)",
            ["ScreenSketch"] = "Screen Sketch",
            ["Phone"] = "Microsoft Phone",
            ["Photos"] = "Microsoft Photos",
            ["FeedbackHub"] = "Windows Feedback Hub",
            ["SoundRecorder"] = "Microsoft Sound Recorder",
            ["Alarms"] = "Windows Alarms & Clock",
            ["SkypeApp"] = "Microsoft SkypeApp",
            ["Maps"] = "Windows Maps",
            ["Camera"] = "Microsoft Camera",
            ["Video"] = "Microsoft Zune Video",
            ["BingNews"] = "Bing News",
            ["Mail"] = "Mail Windows",
            ["MicrosoftTeams"] = "Microsoft Teams",
            ["PoweraAtomateDesktop"] = "Power Atomate",
            ["Cortana"] = "Cortana",
            ["Clipchamp"] = "Windows Clipchamp",
            ["Getstarted"] = "Get Started Windows",
            ["OneDrive"] = "OneDrive"
        };
        private void DiscriptionAnim(string _text)
        {
            Discription.Text = _text;

            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(0)
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
            };

            EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame(1)
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150))
            };

            doubleAnimation.KeyFrames.Add(_fromFrame);
            doubleAnimation.KeyFrames.Add(_toFrame);
            Discription.BeginAnimation(ContextMenu.OpacityProperty, doubleAnimation);
        }

        private void App_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _AppImage = (Image)sender;
            DiscriptionAnim(TweaksHover[_AppImage.Name]);
        }

        private void App_MouseLeave(object sender, MouseEventArgs e) => DiscriptionAnim("Наведите указатель мыши на любое изображение, чтобы получить название приложения");

        #region Click
        private void AppClick_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Image _image = (Image)sender;
            if (e.LeftButton == MouseButtonState.Pressed && _image.Source == (DrawingImage)Application.Current.Resources[_image.Name + "Image"])
            {
                _nameApp = _image.Name;

                if (_nameApp == "OneDrive")
                    _settingsWindows.AppOneDrive(true);

                else
                {
                    _worker = new BackgroundWorker();
                    _worker.DoWork += Worker_DoWorkDeleted;
                    _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                    _worker.RunWorkerAsync();
                }
            }

        }

        private void BRecovery_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try { _applicationsSystem.ApplicationRecovery(); } catch { };

                if (OneDrive.Source == (DrawingImage)Application.Current.Resources["OneDriveImageU"])
                    _settingsWindows.AppOneDrive(false);
            }

        }

        private void BDeleted_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _worker = new BackgroundWorker();
                _worker.DoWork += Worker_DoWorkDeletedAll;
                _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                _worker.RunWorkerAsync();
                _settingsWindows.AppOneDrive(true);
            }
        }
        #endregion

        #region Worker
        private void Worker_DoWorkDeletedAll(object sender, DoWorkEventArgs e)
        {
            try { _applicationsSystem.ApplicationRemovalAll(); } catch { }
        }

        private void Worker_DoWorkDeleted(object sender, DoWorkEventArgs e)
        {
            _applicationsSystem.ApplicationRemoval(_nameApp);
        }

        private void Worker_DoWorkUpdate(object sender, DoWorkEventArgs e)
        {
            _applicationsSystem.CheckInstalledApps();
        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _worker.Dispose();
        }
        #endregion

        private void Page_Unloaded(object sender, RoutedEventArgs e) => _timer.Stop();

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                e.Handled = true;
            }
        }
    }
}
