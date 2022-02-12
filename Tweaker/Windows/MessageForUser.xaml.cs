using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Tweaker
{
    public partial class MessageForUser : Window
    {
        private readonly DispatcherTimer _timer;
        private TimeSpan _time;

        public MessageForUser()
        {
            InitializeComponent();

            #region Таймер закрытия окна
            _time = TimeSpan.FromSeconds(4);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                string _texttimer = _time.ToString("ss");
                ButtonOK.Content = "Понятно (" + _texttimer + ")";
                if (_time == TimeSpan.Zero) { _timer.Stop(); foreach (Process proc in Process.GetProcessesByName("Tweaker Z")) { proc.Kill(); } }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            #endregion
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.Close();
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
