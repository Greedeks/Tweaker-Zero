using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Tweaker.Сlasses;

namespace ToggleSwitch
{

    public partial class ToggleButton : UserControl
    {
        private readonly Thickness _LeftSide = new Thickness(-40, 0, 0, 0);
        private readonly Thickness _RightSide = new Thickness(0, 0, -40, 0);
        private readonly LinearGradientBrush _OffColor = new LinearGradientBrush();
        private readonly LinearGradientBrush _OnColor = new LinearGradientBrush();

        private bool _Toggle = false;

        public ToggleButton()
        {
            InitializeComponent();

            _OnColor.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
            _OnColor.GradientStops.Add(new GradientStop(Color.FromArgb(255, 243, 57, 138), 1.0));

            _OffColor.GradientStops.Add(new GradientStop(Color.FromArgb(255, 80, 80, 80), 1.0));
            _OffColor.GradientStops.Add(new GradientStop(Color.FromArgb(255, 105, 105, 105), 1.0));
        }

        internal bool State { get => _Toggle; set => _Toggle = value; }

        private void Toggle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_Toggle)
            {
                Back.Fill = _OnColor;
                _Toggle = true;
                AnimToggleB(true);

            }
            else
            {

                Back.Fill = _OffColor;
                _Toggle = false;
                AnimToggleB(false);

            }
        }

        private void AnimToggleB(bool cheack)
        {
            ThicknessAnimation _animation = new ThicknessAnimation
            {
                From = !cheack ? _RightSide : _LeftSide,
                To = !cheack ? _LeftSide : _RightSide,
                Duration = TimeSpan.FromSeconds(0.45)
            };
            ElasticEase _elasticEase = new ElasticEase
            {
                EasingMode = EasingMode.EaseOut,
                Springiness = 9,
                Oscillations = 2
            };
            _animation.EasingFunction = _elasticEase;
            Timeline.SetDesiredFrameRate(_animation, 340);

            Dot.BeginAnimation(ContentControl.MarginProperty, _animation);
            DotShadow.BeginAnimation(ContentControl.MarginProperty, _animation);

            BrushAnimation _brushanimation = new BrushAnimation
            {
                From = !cheack ? _OnColor : _OffColor,
                To = !cheack ? _OffColor : _OnColor,
                Duration = TimeSpan.FromSeconds(0.1)
            };

            Timeline.SetDesiredFrameRate(_brushanimation, 340);
            Back.BeginAnimation(Rectangle.FillProperty, _brushanimation);

        }

        private void CheckState()
        {
            if (!_Toggle)
            {
                Back.Fill = _OffColor;
                _Toggle = false;
                Dot.Margin = _LeftSide;
                DotShadow.Margin = _LeftSide;

            }
            else
            {
                Back.Fill = _OnColor;
                _Toggle = true;
                Dot.Margin = _RightSide;
                DotShadow.Margin = _RightSide;
            }
        }

        private void Dot_Loaded(object sender, RoutedEventArgs e)
        {

            CheckState();
        }
    }
}
