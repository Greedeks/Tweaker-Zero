using System;
using System.Windows;
using System.Windows.Threading;

namespace Tweaker
{
    /// <summary>
    /// Логика взаимодействия для MessageForUser.xaml
    /// </summary>
    public partial class MessageForUser : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time; 

        public MessageForUser()
        {
            InitializeComponent();

            #region Таймер закрытия окна
            _time = TimeSpan.FromSeconds(4);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                string _texttimer = _time.ToString("ss");
                TextTimer.Content = "Автозакрытия  окна через "+_texttimer+" секунд";
                if (_time == TimeSpan.Zero) { _timer.Stop(); this.Close(); }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            #endregion
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
