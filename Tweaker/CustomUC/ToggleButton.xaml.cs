using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ToggleSwitch
{
    public partial class ToggleButton : UserControl
    {
        Thickness _LeftSide = new Thickness(-39, 0, 0, 0);
        Thickness _RightSide = new Thickness(0, 0, -39, 0);
        SolidColorBrush _OffColor = new SolidColorBrush(Color.FromRgb(115, 115, 115));
        SolidColorBrush _OnColor = new SolidColorBrush(Colors.Red);
        private bool _Toggle = false;

        public ToggleButton()
        {
            InitializeComponent();
        }

        internal bool State { get => _Toggle; set => _Toggle = value; }

        private void Toggle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_Toggle)
            {
                Back.Fill = _OnColor;
                _Toggle = true;
                AnimMargin(true);

            }
            else
            {

                Back.Fill = _OffColor;
                _Toggle = false;
                AnimMargin(false);

            }
        }

        private void AnimMargin(bool cheack)
        {
            ThicknessAnimation _animation = new ThicknessAnimation
            {
                From = !cheack ? _RightSide : _LeftSide,
                To = !cheack ? _LeftSide : _RightSide,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            ElasticEase _elasticEase = new ElasticEase();
            _elasticEase.EasingMode = EasingMode.EaseOut;
            _elasticEase.Springiness = 9;
            _elasticEase.Oscillations = 2;
            _animation.EasingFunction = _elasticEase;
            Timeline.SetDesiredFrameRate(_animation, 240);

            Dot.BeginAnimation(ContentControl.MarginProperty, _animation);

        }

        private void CheckState()
        {
            if (!_Toggle)
            {
                Back.Fill = _OffColor;
                _Toggle = false;
                Dot.Margin = _LeftSide;
            }
            else
            {
                Back.Fill = _OnColor;
                _Toggle = true;
                Dot.Margin = _RightSide;
            }
        }

        private void Dot_Loaded(object sender, RoutedEventArgs e)
        {
            CheckState();
        }
    }
}
