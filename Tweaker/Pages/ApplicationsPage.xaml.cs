using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Tweaker.Windows;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class ApplicationsUL : Page
    {
        private readonly ApplicationsSystem _applicationsSystem = new ApplicationsSystem();
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private NotificationWindow notificationWindow = new NotificationWindow();
        private BackgroundWorker _worker;
        private string _nameApp = default;
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);

        public ApplicationsUL()
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

        private void DiscriptionAnim(string _text)
        {
            Discription.Text = _text;
            DoubleAnimation _animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                SpeedRatio = 2.7
            };
            Discription.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            Discription.Opacity = 1;
        }

        private void App_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _AppImage = (Image)sender;
            DiscriptionAnim(_AppImage.Name);
        }

        private void App_MouseLeave(object sender, MouseEventArgs e) => DiscriptionAnim("Наведите курсор на любое приложения, чтобы получить его название");

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
                Application.Current.Dispatcher.Invoke(() => { ShowNotification("Информация", "Процесс восстановления приложений начался, это займет некоторое время"); });

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
                ShowNotification("Информация", "Процесс удаления приложений начался, это займет некоторое время");
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

        private void ShowNotification(string _Tittle, string _Text)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Parallel.Invoke(() =>
                {
                    if (notificationWindow.IsLoaded == false)
                    {
                        notificationWindow = new NotificationWindow
                        {
                            AddTitle = _Tittle,
                            AddText = _Text
                        };
                        notificationWindow.Show();
                    }
                });
            });
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                e.Handled = true;
            }
        }
    }
}
