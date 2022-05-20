using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Tweaker
{
    public partial class MessageForUser : Window
    {
        private readonly DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(4);

        public MessageForUser(string _Text)
        {
            InitializeComponent();

            #region Window closing timer
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                string _texttimer = _time.ToString("ss");
                ButtonOKText.Content = "Понятно (" + _texttimer + ")";
                if (_time == TimeSpan.Zero) { _timer.Stop(); Application.Current.Shutdown(); }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            #endregion

            TextMessage.Text = _Text;
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                Application.Current.Shutdown();
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
