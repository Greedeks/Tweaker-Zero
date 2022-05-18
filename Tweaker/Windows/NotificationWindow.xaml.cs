using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Tweaker.Windows
{
    public partial class NotificationWindow : Window
    {
        private readonly DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(4);

        private byte _action = 0;
        internal string AddTitle { get => NotificationTitle.Text; set => NotificationTitle.Text = value; }
        internal new string AddText { get => NotificationText.Text; set => NotificationText.Text = value; }
        internal byte ActionChoice { get => _action; set => _action = value; }
        public NotificationWindow()
        {
            InitializeComponent();

            #region Таймер закрытия окна
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero) { _timer.Stop(); this.Close(); }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            #endregion

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AddTitle == "Title") NotificationTitle.Text = "Информация";
            else NotificationTitle.Text = AddTitle;
            NotificationText.Text = AddText;

            Rect _primaryMonitorArea = SystemParameters.WorkArea;
            NotificationW.Top = _primaryMonitorArea.Bottom - this.Height - 10;

            Storyboard story = new Storyboard();
            Storyboard.SetTargetProperty(story, new PropertyPath("Left"));
            Storyboard.SetTarget(story, NotificationW);

            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();

            EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(_primaryMonitorArea.Right);
            _fromFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));

            EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame(_primaryMonitorArea.Right - this.Width - 10);
            _toFrame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150));

            doubleAnimation.KeyFrames.Add(_fromFrame);
            doubleAnimation.KeyFrames.Add(_toFrame);
            story.Children.Add(doubleAnimation);
            NotificationW.BeginAnimation(Canvas.LeftProperty, doubleAnimation);


            DoubleAnimation _animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            NotificationW.BeginAnimation(UIElement.OpacityProperty, _animation);


            NotificationW.Left = _primaryMonitorArea.Right - this.Width - 10;
        }

        private void NotificationW_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= NotificationW_Closing;
            e.Cancel = true;
            DoubleAnimation _animation = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            _animation.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, _animation);
        }

        private void Button_Exit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.Close();
        }

        private void Notification_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                switch (_action)
                {
                    case 1:
                        Process.Start("logoff");
                        break;
                    case 2:
                        Process.Start("shutdown", "/r /t 0");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
