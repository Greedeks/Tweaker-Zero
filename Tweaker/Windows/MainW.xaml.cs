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
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private readonly GetSystemInformation _getsystemInformation = new GetSystemInformation();
        private readonly ApplicationsSystem _applicationsSystem = new ApplicationsSystem();
        #endregion

        public MainWindow()
        {
            _checkApplicationCopy.CheckAC();

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
            ActivePageAnim(false);
            CleaningPages();

            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "Button_Confidentiality":
                    if (e.LeftButton == MouseButtonState.Pressed && !_confidentialityB)
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
                    if (e.LeftButton == MouseButtonState.Pressed && !_interfaceB)
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
                    if (e.LeftButton == MouseButtonState.Pressed && !_applicationB)
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
                    if (e.LeftButton == MouseButtonState.Pressed && !_servicesB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(ActivePage, 3);

                        ActivePageAnim(true);
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
                    if (e.LeftButton == MouseButtonState.Pressed && !_systemB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(ActivePage, 4);

                        ActivePageAnim(true);
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
                    if (e.LeftButton == MouseButtonState.Pressed && !_systeminfoB)
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
                    if (e.LeftButton == MouseButtonState.Pressed && !_moreB)
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

        private void Button_Settings_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !_settings)
            {
                StandStateBtnN();
                ActivePageAnim(false);
                CleaningPages();

            }
            else
            {
                StandStateBtnN();
                CleaningPages();
            }
        }
        #endregion

        private  async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetSystemInformation._urlImage = _getsystemInformation.SetImageUser();
            _getsystemInformation.GetInormationPC();
            _settingsWindows.TaskCheckStateConfidentiality();
            _applicationsSystem.CheckInstalledApps();

            #region Анимация загрузки
            this.Opacity = 0;
            await Task.Delay(1);
            this.Opacity = 1;

            DoubleAnimation _animationTop = new DoubleAnimation
            {
                From = SystemParameters.PrimaryScreenHeight,
                To = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2),
                Duration = TimeSpan.FromSeconds(0.15)
            };

            DoubleAnimation _animationLeft = new DoubleAnimation
            {
                From = -SystemParameters.PrimaryScreenWidth,
                To = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2),
                Duration = TimeSpan.FromSeconds(0.15)
            };

            TweakerWPF.BeginAnimation(Canvas.TopProperty, _animationTop);
            TweakerWPF.BeginAnimation(Canvas.LeftProperty, _animationLeft);

            TweakerWPF.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
            TweakerWPF.Top = (SystemParameters.PrimaryScreenHeight / 2) -(this.Height / 2);
            #endregion
        }
    }
}
