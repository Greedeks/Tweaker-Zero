using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tweaker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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
            _animation.Duration = TimeSpan.FromSeconds(0.12);

            Slider.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            Slider.Opacity = 0;
        }

        private void SliderON()
        {
            var _animation = new DoubleAnimation();
            _animation.From = Slider.Opacity;
            _animation.To = 1;
            _animation.Duration = TimeSpan.FromSeconds(0.12);

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
            Button_Confidentiality.Style = (Style)Application.Current.Resources["CustomB"];
            Button_Interface.Style = (Style)Application.Current.Resources["CustomB"];
            Button_Application.Style = (Style)Application.Current.Resources["CustomB"];
            Button_Services.Style = (Style)Application.Current.Resources["CustomB"];
            Button_System.Style = (Style)Application.Current.Resources["CustomB"];
            Button_SystemInfo.Style = (Style)Application.Current.Resources["CustomB"];
            Button_More.Style = (Style)Application.Current.Resources["CustomB"];
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
            DeffStyleButtons();
            Button_Confidentiality.Style = (Style)Application.Current.Resources["CustomB1"];
            if (!_confidentialityB)
            {

                SliderON();
                Grid.SetColumn(Slider, 0);

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
            DeffStyleButtons();
            if (!_interfaceB)
            {
                Button_Interface.Style = (Style)Application.Current.Resources["CustomB1"];
                SliderON();
                Grid.SetColumn(Slider, 1);

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
            DeffStyleButtons();
            if (!_applicationB)
            {
                Button_Application.Style = (Style)Application.Current.Resources["CustomB1"];
                SliderON();
                Grid.SetColumn(Slider, 2);

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
            DeffStyleButtons();
            if (!_servicesB)
            {
                Button_Services.Style = (Style)Application.Current.Resources["CustomB1"];
                SliderON();
                Grid.SetColumn(Slider, 3);

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
            DeffStyleButtons();
            if (!_systemB)
            {
                Button_System.Style = (Style)Application.Current.Resources["CustomB1"];
                SliderON();
                Grid.SetColumn(Slider, 4);

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
            DeffStyleButtons();
            if (!_systeminfoB)
            {
                Button_SystemInfo.Style = (Style)Application.Current.Resources["CustomB1"];
                SliderON();
                Grid.SetColumn(Slider, 5);

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
            DeffStyleButtons();
            if (!_moreB)
            {
                Button_More.Style = (Style)Application.Current.Resources["CustomB1"];
                SliderON();
                Grid.SetColumn(Slider, 6);

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
    }
}
