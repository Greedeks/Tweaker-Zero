using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class SystemInfromation : Page
    {
        private readonly GetSystemInformation _getSystemInformation = new GetSystemInformation();
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);
        private static bool _sticking = false;
        private BackgroundWorker _worker;
        private string _textcopy = string.Empty;

        public SystemInfromation()
        {
            InitializeComponent();

            _worker = new BackgroundWorker();
            _worker.RunWorkerAsync();
            _worker.DoWork += (s, e) => { _getSystemInformation.GetIpUser(); };
            _worker.RunWorkerCompleted += (s, e) => { IpAddress.Text = GetSystemInformation._ipAddress; };

            if (GetSystemInformation._urlImage != null) UserAvatar.ImageSource = GetSystemInformation._urlImage;
            UserName.Text = _getSystemInformation.NameUser();
            _getSystemInformation.SetInormationPC(this);

            Update();
        }

        private void Update()
        {
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds % 2 == 0) { _getSystemInformation.UpdateInormation();
                    _getSystemInformation.SetInormationPC(this); }
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        private void AnimNotf(in bool _reverse)
        {
            Notf.Visibility = Visibility.Visible;
            DoubleAnimation _animation = new DoubleAnimation
            {
                From = !_reverse ? 0 : 1,
                To = !_reverse ? 1 : 0,
                SpeedRatio = 2,
                Duration = TimeSpan.FromSeconds(0.2),
            };
            Notf.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            Notf.Opacity = !_reverse ? 1 : 0;
        }

        #region CopyText
        private async void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount >= 2)
            {
                if (!_sticking)
                {
                    Clipboard.Clear();
                    TextBlock _textBlock = (TextBlock)sender;
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, _textBlock.Text);
                    await Task.Delay(700);
                    AnimNotf(true);
                    _sticking = false;

                }
            }
        }

        private async void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (_textcopy != string.Empty)
            {
                if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control) && e.Key == Key.C)
                {
                    if (!_sticking)
                    {
                        Clipboard.Clear();
                        _sticking = true;
                        AnimNotf(false);
                        Clipboard.SetData(DataFormats.UnicodeText, _textcopy);
                        await Task.Delay(700);
                        AnimNotf(true);
                        _sticking = false;
                    }
                }
            }
        }
        #endregion

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _worker.Dispose();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock _textBlock = (TextBlock)sender;
            _textcopy = _textBlock.Text;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e) => _textcopy = string.Empty;
    }
}
