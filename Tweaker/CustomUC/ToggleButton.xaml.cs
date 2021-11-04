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

namespace ToggleSwitch
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -39, 0);
        SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(115, 115, 115));
        SolidColorBrush On = new SolidColorBrush(Color.FromRgb(184, 32, 21));
        private bool Toggled = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = Off;
            Toggled = false;
            Dot.Margin = LeftSide;
        }

        public bool State { get => Toggled; set => Toggled = value; }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                AnimMargin(true);

            }
            else
            {

                Back.Fill = Off;
                Toggled = false;
                AnimMargin(false);

            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                AnimMargin(true);

            }
            else
            {

                Back.Fill = Off;
                Toggled = false;
                AnimMargin(false);

            }
        }

        private void AnimMargin(bool cheack)
        {
            ThicknessAnimation _animation = new ThicknessAnimation
            {
                From = !cheack ? RightSide : LeftSide,
                To = !cheack ? LeftSide : RightSide,
                Duration = TimeSpan.FromSeconds(0.1)
            };

            Dot.BeginAnimation(ContentControl.MarginProperty, _animation);
        }
    }
}
