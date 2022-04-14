using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Tweaker.Windows
{
    public partial class NotificationWindow : Window
    {
        private static string _titleNotificatio, _textNotificatio;
        private readonly DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(4);

        internal string AddTitle { get => _titleNotificatio; set => _titleNotificatio = value; }
        internal new string AddText { get => _textNotificatio; set => _textNotificatio = value; }
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
            if(AddTitle == null) NotificationTitle.Text = "Информация";
            else NotificationTitle.Text = AddTitle;
            NotificationText.Text = AddText;

            Rect primaryMonitorArea = SystemParameters.WorkArea;
            NotificationW.Top = primaryMonitorArea.Bottom - this.Height - 10;

            DoubleAnimation _animationLeft = new DoubleAnimation
            {
                From = primaryMonitorArea.Right,
                To = primaryMonitorArea.Right - this.Width - 10,
                SpeedRatio = 6,
                Duration = TimeSpan.FromSeconds(1)
            };

            DoubleAnimation _animation = new DoubleAnimation
            {
                From =  0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            NotificationW.BeginAnimation(UIElement.OpacityProperty, _animation);
            NotificationW.BeginAnimation(Canvas.LeftProperty, _animationLeft);


            NotificationW.Left = primaryMonitorArea.Right - this.Width - 10;
            NotificationW.Opacity = 1;
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
    }
}
