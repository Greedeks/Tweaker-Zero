using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Tweaker.Сlasses;

namespace Tweaker
{
    public partial class MainWindow : Window
    {
        #region Параметры
        private bool _confidentialityB = false, _interfaceB = false, _applicationB = false, _servicesB = false,
            _systemB = false, _systeminfoB = false, _moreB = false, _settings = false;
        private readonly CheckApplicationCopy _checkApplicationCopy = new CheckApplicationCopy();
        private readonly StartScanner _startScanner = new StartScanner();
        private readonly CheckWindowsVersion _checkWindowsVersion = new CheckWindowsVersion();
        #endregion

        public MainWindow()
        {
            Parallel.Invoke(() => { _checkApplicationCopy.CheckAC(); });
            Parallel.Invoke(() => { _checkWindowsVersion.CheckVersion(); });

            InitializeComponent();
        }

        #region Перемещения/Закрытие/Сворачивание Формы
        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Exit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.Close();
        }

        private void Button_Minimized_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.WindowState = WindowState.Minimized;
        }
        #endregion

        private void ActivePageAnim(bool _stateAnimActivePage)
        {
            DoubleAnimation _animation = new DoubleAnimation
            {
                From = !_stateAnimActivePage ? ActivePage.Opacity : 0,
                To = !_stateAnimActivePage ? 0 : 1,
                Duration = TimeSpan.FromSeconds(0.15)
            };
            Timeline.SetDesiredFrameRate(_animation, 60);
            ActivePage.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            ActivePage.Opacity = !_stateAnimActivePage ? 0 : 1;
        }

        private void StandStateBtnN()
        {
            _confidentialityB = _interfaceB = _applicationB
            = _servicesB = _systemB = _systeminfoB = _moreB = _settings = false;

            Button_Confidentiality.Style = Button_Interface.Style = Button_Application.Style = Button_Services.Style = Button_System.Style
            = Button_SystemInfo.Style = Button_More.Style = (Style)Application.Current.Resources["ButtonNav"];
        }

        private void CleaningPages()
        {
            while (MainContainer.NavigationService.RemoveBackEntry() != null) ;
            MainContainer.Content = null;
        }

        #region Кнопки
        private void Button_Navigations_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ActivePageAnim(false);
                CleaningPages();

                Button btn = (Button)sender;
                switch (btn.Name)
                {
                    case "Button_Confidentiality":
                        if (!_confidentialityB)
                        {
                            StandStateBtnN();
                            btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                            Grid.SetColumn(ActivePage, 0);

                            ActivePageAnim(true);
                            MainContainer.Content = new Pages.Confidentiality();
                            _confidentialityB = true;
                        }
                        else
                        {
                            StandStateBtnN();
                            ActivePageAnim(false);
                            CleaningPages();
                        }
                        break;
                    case "Button_Interface":
                        if (!_interfaceB)
                        {
                            StandStateBtnN();
                            btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                            Grid.SetColumn(ActivePage, 1);

                            ActivePageAnim(true);
                            MainContainer.Content = new Pages.Interface();
                            _interfaceB = true;
                        }
                        else
                        {
                            StandStateBtnN();
                            ActivePageAnim(false);
                            CleaningPages();
                        }
                        break;
                    case "Button_Application":
                        if (!_applicationB)
                        {
                            StandStateBtnN();
                            btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                            Grid.SetColumn(ActivePage, 2);

                            ActivePageAnim(true);
                            MainContainer.Content = new Pages.ApplicationsUL();
                            _applicationB = true;
                        }
                        else
                        {
                            StandStateBtnN();
                            ActivePageAnim(false);
                            CleaningPages();
                        }
                        break;
                    case "Button_Services":
                        if (!_servicesB)
                        {
                            StandStateBtnN();
                            btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                            Grid.SetColumn(ActivePage, 3);

                            ActivePageAnim(true);
                            MainContainer.Content = new Pages.ServicesPage();
                            _servicesB = true;
                        }
                        else
                        {
                            StandStateBtnN();
                            ActivePageAnim(false);
                            CleaningPages();
                        }
                        break;
                    case "Button_System":
                        if (!_systemB)
                        {
                            StandStateBtnN();
                            btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                            Grid.SetColumn(ActivePage, 4);

                            ActivePageAnim(true);
                            MainContainer.Content = new Pages.SystemPage();
                            _systemB = true;
                        }
                        else
                        {
                            StandStateBtnN();
                            ActivePageAnim(false);
                            CleaningPages();
                        }
                        break;
                    case "Button_SystemInfo":
                        if (!_systeminfoB)
                        {
                            StandStateBtnN();
                            btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                            Grid.SetColumn(ActivePage, 5);

                            ActivePageAnim(true);
                            MainContainer.Content = new Pages.SystemInfromation();
                            _systeminfoB = true;
                        }
                        else
                        {
                            StandStateBtnN();
                            ActivePageAnim(false);
                            CleaningPages();
                        }
                        break;
                    case "Button_More":
                        if (!_moreB)
                        {
                            StandStateBtnN();
                            btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                            Grid.SetColumn(ActivePage, 6);

                            ActivePageAnim(true);
                            _moreB = true;
                        }
                        else
                        {
                            StandStateBtnN();
                            ActivePageAnim(false);
                            CleaningPages();
                        }
                        break;
                    default:
                        StandStateBtnN();
                        ActivePageAnim(false);
                        CleaningPages();
                        break;
                }
            }
        }

        private void Button_Settings_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !_settings)
            {
                StandStateBtnN();
                ActivePageAnim(false);
                CleaningPages();

            }
        }
        #endregion

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _startScanner.ScantheSystem();

            #region Анимация загрузки
            this.Opacity = 0;
            await Task.Delay(1);
            this.Opacity = 1;

            Rect _primaryMonitorArea = SystemParameters.WorkArea;

            DoubleAnimation _animationTop = new DoubleAnimation
            {
                From = _primaryMonitorArea.Bottom,
                To = (_primaryMonitorArea.Bottom / 2) - (this.Height / 2),
                Duration = TimeSpan.FromSeconds(0.2),
                SpeedRatio = 1
            };

            DoubleAnimation _animationLeft = new DoubleAnimation
            {
                From = -_primaryMonitorArea.Right,
                To = (_primaryMonitorArea.Right / 2) - (this.Width / 2),
                Duration = TimeSpan.FromSeconds(0.2),
                SpeedRatio = 1
            };

            TweakerWPF.BeginAnimation(Canvas.TopProperty, _animationTop);
            TweakerWPF.BeginAnimation(Canvas.LeftProperty, _animationLeft);

            TweakerWPF.Left = (_primaryMonitorArea.Bottom / 2) - (this.Width / 2);
            TweakerWPF.Top = (_primaryMonitorArea.Right / 2) - (this.Height / 2);
            #endregion
        }
    }
}
