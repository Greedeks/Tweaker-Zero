using System;
using System.Threading.Tasks;
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
        private readonly Thickness _LeftSide = new Thickness(-40, 0, 0, 0), _RightSide = new Thickness(0, 0, -40, 0);
        private readonly LinearGradientBrush _OffColor = new LinearGradientBrush(), _OnColor = new LinearGradientBrush();
        private bool _Toggle = false;
        private TimeSpan _timeline = TimeSpan.FromMilliseconds(0);

        public ToggleButton()
        {
            InitializeComponent();

            _OnColor.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 36, 0), 0.2));
            _OnColor.GradientStops.Add(new GradientStop(Color.FromArgb(255, 158, 22, 0), 0.9));
            _OnColor.StartPoint = new Point(0, 0);
            _OnColor.EndPoint = new Point(0, 1);

            _OffColor.GradientStops.Add(new GradientStop(Color.FromArgb(255, 80, 80, 80), 1.0));
            _OffColor.GradientStops.Add(new GradientStop(Color.FromArgb(255, 105, 105, 105), 1.0));

            Back.Fill = _OffColor;
            _Toggle = false;
            Dot.Margin = _LeftSide;
        }

        internal bool State { get => _Toggle; set { _Toggle = value; AnimToggleB(_Toggle,true); } }

        private void Toggle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_Toggle)
            {
                Back.Fill = _OnColor;
                _Toggle = true;
                AnimToggleB(true, false);
            }
            else
            {
                Back.Fill = _OffColor;
                _Toggle = false;
                AnimToggleB(false, false);
            }
        }

        private void AnimToggleB(bool _cheack, bool _firstStart)
        {
            Parallel.Invoke(() =>
            {
                ThicknessAnimationUsingKeyFrames doubleAnimation = new ThicknessAnimationUsingKeyFrames();

                if (_cheack && Dot.Margin != _RightSide)
                {
                    EasingThicknessKeyFrame _fromFrame = new EasingThicknessKeyFrame(_LeftSide)
                    {
                        KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
                    };

                    EasingThicknessKeyFrame _toFrame = new EasingThicknessKeyFrame(_RightSide)
                    {
                        KeyTime = !_firstStart ? KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(50)) : _timeline
                    };

                    doubleAnimation.KeyFrames.Add(_fromFrame);
                    doubleAnimation.KeyFrames.Add(_toFrame);
                    Dot.BeginAnimation(ContentControl.MarginProperty, doubleAnimation);

                    BrushAnimation _brushanimation = new BrushAnimation
                    {
                        From = _OffColor,
                        To = _OnColor,
                        SpeedRatio = 1,
                        Duration = !_firstStart ? TimeSpan.FromSeconds(0.1) : _timeline
                    };
                    Back.BeginAnimation(Rectangle.FillProperty, _brushanimation);
                }

                else if (!_cheack && Dot.Margin != _LeftSide)
                {
                    EasingThicknessKeyFrame _fromFrame = new EasingThicknessKeyFrame(_RightSide)
                    {
                        KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
                    };

                    EasingThicknessKeyFrame _toFrame = new EasingThicknessKeyFrame(_LeftSide)
                    {
                        KeyTime = !_firstStart ? KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(60)) : _timeline
                    };

                    doubleAnimation.KeyFrames.Add(_fromFrame);
                    doubleAnimation.KeyFrames.Add(_toFrame);
                    Dot.BeginAnimation(ContentControl.MarginProperty, doubleAnimation);

                    BrushAnimation _brushanimation = new BrushAnimation
                    {
                        From = _OnColor,
                        To = _OffColor,
                        SpeedRatio = 1,
                        Duration = !_firstStart ? TimeSpan.FromSeconds(0.1) : _timeline
                    };
                    Back.BeginAnimation(Rectangle.FillProperty, _brushanimation);
                }
            });

        }

        private void Dot_Loaded(object sender, RoutedEventArgs e) =>  _timeline = TimeSpan.FromMilliseconds(60);

    }
}
