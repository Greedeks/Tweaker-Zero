using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;


namespace Tweaker
{
    public partial class MainWindow : Window
    {
        #region Переменные
        private bool _confidentialityB = false, _interfaceB = false, _applicationB = false, _servicesB = false, 
            _systemB = false, _systeminfoB = false, _moreB = false, _settings=false;
        #endregion

        public MainWindow()
        {
            CheakApplicationCopy cheakApplicationCopy= new CheakApplicationCopy();
            cheakApplicationCopy.CheakAC();
            InitializeComponent();
        }

        #region Перемещения/Закрытие/Сворачивание Формы
        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.Close();
        }

        private void Button_Minimized_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.WindowState = WindowState.Minimized;
        }
        #endregion

        private void SliderAnim(bool _stateAnimSlider)
        {
            DoubleAnimation _animation = new DoubleAnimation
            {
                From = !_stateAnimSlider ? Slider.Opacity : 0,
                To = !_stateAnimSlider ? 0 : 1,
                Duration = TimeSpan.FromSeconds(0.15)
            };

            Slider.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            Slider.Opacity = !_stateAnimSlider ? 0 : 1;
        }

        private void StandStateBtnN()
        {
            _confidentialityB = _interfaceB = _applicationB
            = _servicesB = _systemB = _systeminfoB = _moreB = _settings = false;

            Button_Confidentiality.Style = Button_Interface.Style = Button_Application.Style = Button_Services.Style = Button_System.Style
            = Button_SystemInfo.Style = Button_More.Style = (Style)Application.Current.Resources["ButtonNav"];
        }

        private void CleaningWindows()
        {
            MainContainer.Children.Clear();
            if (MainContainer.Children.Count != 0)
                MainContainer.Children.RemoveAt(MainContainer.Children.Count);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #region Кнопки
        private void Button_Navigations_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SliderAnim(false);
            CleaningWindows();

            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "Button_Confidentiality":
                    if (e.LeftButton == MouseButtonState.Pressed && !_confidentialityB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(Slider, 0);

                        SliderAnim(true);
                        MainContainer.Children.Add(new ConfidentialityW());
                        _confidentialityB = true;
                    }
                    else
                    {
                        StandStateBtnN();
                        SliderAnim(false);
                        CleaningWindows();
                    }
                    break;
                case "Button_Interface":
                    if (e.LeftButton == MouseButtonState.Pressed && !_interfaceB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(Slider, 1);

                        SliderAnim(true);
                        _interfaceB = true;
                    }
                    else
                    {
                        StandStateBtnN();
                        SliderAnim(false);
                        CleaningWindows();
                    }
                    break;
                case "Button_Application":
                    if (e.LeftButton == MouseButtonState.Pressed && !_applicationB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(Slider, 2);

                        SliderAnim(true);
                        _applicationB = true;
                    }
                    else
                    {
                        StandStateBtnN();
                        SliderAnim(false);
                        CleaningWindows();
                    }
                    break;
                case "Button_Services":
                    if (e.LeftButton == MouseButtonState.Pressed && !_servicesB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(Slider, 3);

                        SliderAnim(true);
                        _servicesB = true;
                    }
                    else
                    {
                        StandStateBtnN();
                        SliderAnim(false);
                        CleaningWindows();
                    }
                    break;
                case "Button_System":
                    if (e.LeftButton == MouseButtonState.Pressed && !_systemB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(Slider, 4);

                        SliderAnim(true);
                        _systemB = true;
                    }
                    else
                    {
                        StandStateBtnN();
                        SliderAnim(false);
                        CleaningWindows();
                    }
                    break;
                case "Button_SystemInfo":
                    if (e.LeftButton == MouseButtonState.Pressed && !_systeminfoB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(Slider, 5);

                        SliderAnim(true);
                        _systeminfoB = true;
                    }
                    else
                    {
                        StandStateBtnN();
                        SliderAnim(false);
                        CleaningWindows();
                    }
                    break;
                case "Button_More":
                    if (e.LeftButton == MouseButtonState.Pressed && !_moreB)
                    {
                        StandStateBtnN();
                        btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                        Grid.SetColumn(Slider, 6);

                        SliderAnim(true);
                        _moreB = true;
                    }
                    else
                    {
                        StandStateBtnN();
                        SliderAnim(false);
                        CleaningWindows();
                    }
                    break;
                default:
                    StandStateBtnN();
                    SliderAnim(false);
                    CleaningWindows();
                    break;
            }
        }

        private void Button_Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !_settings)
            {
                StandStateBtnN();
                SliderAnim(false);
                CleaningWindows();

            }
            else
            {
                StandStateBtnN();
                CleaningWindows();
            }
        }

        private void Button_TextHeader_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 3)
                TextHeader.Content = "Tweaker Zero";
            if (e.RightButton == MouseButtonState.Pressed && e.ClickCount == 3) 
                TextHeader.Content = "Tweaker Z";
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Анимация загрузки
            double _topInit = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2), _leftInit = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);

            DoubleAnimation _animationLeft = new DoubleAnimation
            {
                From = -1000,
                To = _leftInit,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            DoubleAnimation _animationTop = new DoubleAnimation
            {
                From = 1000,
                To = _topInit,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            TweakerWPF.BeginAnimation(Canvas.LeftProperty, _animationLeft);
            TweakerWPF.BeginAnimation(Canvas.TopProperty, _animationTop);
            #endregion
        }
    }
}
