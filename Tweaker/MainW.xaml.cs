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
            _systemB = false, _systeminfoB = false, _moreB = false;
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

        #region Анимация Слайдера
        private void SliderOFF()
        {
            var _animation = new DoubleAnimation();
            _animation.From = Slider.Opacity;
            _animation.To = 0;
            _animation.Duration = TimeSpan.FromSeconds(0.15);

            Slider.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            Slider.Opacity = 0;
        }

        private void SliderON()
        {
            var _animation = new DoubleAnimation();
            _animation.From = 0;
            _animation.To = 1;
            _animation.Duration = TimeSpan.FromSeconds(0.15);

            Slider.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            Slider.Opacity = 1;
        }
        #endregion

        private void ResetButtonsNav()=> _confidentialityB = _interfaceB = _applicationB 
            = _servicesB = _systemB = _systeminfoB = _moreB = false;

        private void DeffStyleButtons()
        {
            Button_Confidentiality.Style = Button_Interface.Style = Button_Application.Style = Button_Services.Style = Button_System.Style
            = Button_SystemInfo.Style = Button_More.Style = (Style)Application.Current.Resources["ButtonNav"];
            MainContainer.Children.Clear();
        }

        #region Кнопки Навигации
        private void Button_Confidentiality_Click(object sender, RoutedEventArgs e)
        {
            SliderOFF();
            DeffStyleButtons();
            if (!_confidentialityB)
            {
                Button_Confidentiality.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                Grid.SetColumn(Slider, 0);
                SliderON();
                MainContainer.Children.Add(new ConfidentialityW());
                ResetButtonsNav();
                _confidentialityB = true;
            }
            else
            {
                ResetButtonsNav();
                SliderOFF();
                DeffStyleButtons();
            }
        }

        private void Button_Interface_Click(object sender, RoutedEventArgs e)
        {
            SliderOFF();
            DeffStyleButtons();
            if (!_interfaceB)
            {
                Button_Interface.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                Grid.SetColumn(Slider, 1);
                SliderON();

                ResetButtonsNav();
                _interfaceB = true;
            }
            else
            {
                ResetButtonsNav();
                SliderOFF();
                DeffStyleButtons();
            }
        }

        private void Button_Application_Click(object sender, RoutedEventArgs e)
        {
            SliderOFF();
            DeffStyleButtons();
            if (!_applicationB)
            {
                Button_Application.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                Grid.SetColumn(Slider, 2);
                SliderON();

                ResetButtonsNav();
                _applicationB = true;
            }
            else
            {
                ResetButtonsNav();
                SliderOFF();
                DeffStyleButtons();
            }
        }

        private void Button_Services_Click(object sender, RoutedEventArgs e)
        {
            SliderOFF();
            DeffStyleButtons();
            if (!_servicesB)
            {
                Button_Services.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                Grid.SetColumn(Slider, 3);
                SliderON();

                ResetButtonsNav();
                _servicesB = true;
            }
            else
            {
                ResetButtonsNav();
                SliderOFF();
                DeffStyleButtons();
            }
        }

        private void Button_System_Click(object sender, RoutedEventArgs e)
        {
            SliderOFF();
            DeffStyleButtons();
            if (!_systemB)
            {
                Button_System.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                Grid.SetColumn(Slider, 4);
                SliderON();

                ResetButtonsNav();
                _systemB = true;
            }
            else
            {
                ResetButtonsNav();
                SliderOFF();
                DeffStyleButtons();
            }
        }

        private void Button_SystemInfo_Click(object sender, RoutedEventArgs e)
        {
            SliderOFF();
            DeffStyleButtons();
            if (!_systeminfoB)
            {
                Button_SystemInfo.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                Grid.SetColumn(Slider, 5);
                SliderON();

                ResetButtonsNav();
                _systeminfoB = true;
            }
            else
            {
                ResetButtonsNav();
                SliderOFF();
                DeffStyleButtons();
            }
        }

        private void Button_More_Click(object sender, RoutedEventArgs e)
        {
            SliderOFF();
            DeffStyleButtons();
            if (!_moreB)
            {
                Button_More.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                Grid.SetColumn(Slider, 6);
                SliderON();

                ResetButtonsNav();
                _moreB = true;
            }
            else
            {
                ResetButtonsNav();
                SliderOFF();
                DeffStyleButtons();
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Анимация загрузки
            double _topInit = Canvas.GetTop(TweakerWPF), _leftInit = Canvas.GetLeft(TweakerWPF);

            var _animationLeft = new DoubleAnimation();
            _animationLeft.From = -1000;
            _animationLeft.To = _leftInit;
            _animationLeft.Duration = TimeSpan.FromSeconds(0.20);

            var _animationTop = new DoubleAnimation();
            _animationTop.From = 1000;
            _animationTop.To = _topInit;
            _animationTop.Duration = TimeSpan.FromSeconds(0.20);

            TweakerWPF.BeginAnimation(Canvas.LeftProperty, _animationLeft);
            TweakerWPF.BeginAnimation(Canvas.TopProperty, _animationTop);
            #endregion
        }
    }
}
