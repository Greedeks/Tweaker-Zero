using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;


namespace Tweaker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            /* Проверка запущенного приложения */
            CheakApplicationCopy.CheakAC();

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

        #region Кнопки Навигации

        /* Анимация Слайдера */
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


        /* Активность Кнопак */
        private void NavButtonsOFF()
        {
            _confidentialityB = false;
            _interfaceB = false;
            _applicationB = false;
            _servicesB = false;
            _systemB = false;
            _systeminfoB = false;
            _moreB = false;
        }

        /* Обычный Стиль Кнопак */
        private void DeffStyleButtons()
        {
            Button_Confidentiality.Style = (Style)Application.Current.Resources["ButtonNav"];
            Button_Interface.Style = (Style)Application.Current.Resources["ButtonNav"];
            Button_Application.Style = (Style)Application.Current.Resources["ButtonNav"];
            Button_Services.Style = (Style)Application.Current.Resources["ButtonNav"];
            Button_System.Style = (Style)Application.Current.Resources["ButtonNav"];
            Button_SystemInfo.Style = (Style)Application.Current.Resources["ButtonNav"];
            Button_More.Style = (Style)Application.Current.Resources["ButtonNav"];
        }


        bool _confidentialityB = false;
        bool _interfaceB = false;
        bool _applicationB = false;
        bool _servicesB = false;
        bool _systemB = false;
        bool _systeminfoB = false;
        bool _moreB = false;

        private void Button_Confidentiality_Click(object sender, RoutedEventArgs e)
        {
            SliderOFF();
            DeffStyleButtons();
            if (!_confidentialityB)
            {
                Button_Confidentiality.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                Grid.SetColumn(Slider, 0);
                SliderON();

                NavButtonsOFF();
                _confidentialityB = true;
            }
            else
            {
                NavButtonsOFF();
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

                NavButtonsOFF();
                _interfaceB = true;
            }
            else
            {
                NavButtonsOFF();
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

                NavButtonsOFF();
                _applicationB = true;
            }
            else
            {
                NavButtonsOFF();
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

                NavButtonsOFF();
                _servicesB = true;
            }
            else
            {
                NavButtonsOFF();
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

                NavButtonsOFF();
                _systemB = true;
            }
            else
            {
                NavButtonsOFF();
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

                NavButtonsOFF();
                _systeminfoB = true;
            }
            else
            {
                NavButtonsOFF();
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

                NavButtonsOFF();
                _moreB = true;
            }
            else
            {
                NavButtonsOFF();
                SliderOFF();
                DeffStyleButtons();
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Анимация загрузки
            var _leftInit = Canvas.GetLeft(TweakerWPF);
            var _topInit = Canvas.GetTop(TweakerWPF);

            var _animationLeft = new DoubleAnimation();
            _animationLeft.From = -1000;
            _animationLeft.To = _leftInit;
            _animationLeft.Duration = TimeSpan.FromSeconds(0.25);

            var _animationTop = new DoubleAnimation();
            _animationTop.From = 1000;
            _animationTop.To = _topInit;
            _animationTop.Duration = TimeSpan.FromSeconds(0.25);

            TweakerWPF.BeginAnimation(Canvas.LeftProperty, _animationLeft);
            TweakerWPF.BeginAnimation(Canvas.TopProperty, _animationTop);
            #endregion
        }
    }
}
