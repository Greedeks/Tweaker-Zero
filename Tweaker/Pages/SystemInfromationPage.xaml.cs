using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private static string _ipUser = "Пожалуйста немного подождите..";
        private static bool _sticking = false;
        private BackgroundWorker _worker;
        private string _textcopy = string.Empty;

        public SystemInfromation()
        {
            InitializeComponent();

            _worker = new BackgroundWorker();
            _worker.RunWorkerAsync();
            _worker.DoWork += (s, e) => { GetIpUser(); };
            _worker.RunWorkerCompleted += (s, e) => { IpAddress.Text = _ipUser; };

            if (GetSystemInformation._urlImage != null) UserAvatar.ImageSource = GetSystemInformation._urlImage;
            UserName.Text = _getSystemInformation.NameUser();
            _getSystemInformation.SetInormationPC(this);
            IpAddress.Text = _ipUser;

            UpdateDisk();

            #region EventForCopyText
            NameOS.MouseEnter += (s, e) => { _textcopy = NameOS.Text; };
            NameBIOS.MouseEnter += (s, e) => { _textcopy = NameBIOS.Text; };
            NameMotherBr.MouseEnter += (s, e) => { _textcopy = NameMotherBr.Text; };
            NameCPU.MouseEnter += (s, e) => { _textcopy = NameCPU.Text; };
            NameGPU.MouseEnter += (s, e) => { _textcopy = NameGPU.Text; };
            NameRAM.MouseEnter += (s, e) => { _textcopy = NameRAM.Text; };
            NameDisk.MouseEnter += (s, e) => { _textcopy = NameDisk.Text; };
            NameSound.MouseEnter += (s, e) => { _textcopy = NameSound.Text; };
            IpAddress.MouseEnter += (s, e) => { _textcopy = IpAddress.Text; };
            Ipv4.MouseEnter += (s, e) => { _textcopy = Ipv4.Text; };
            MACaddress.MouseEnter += (s, e) => { _textcopy = MACaddress.Text; };
            NameNetAdapter.MouseEnter += (s, e) => { _textcopy = NameNetAdapter.Text; };

            NameOS.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            NameBIOS.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            NameMotherBr.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            NameCPU.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            NameGPU.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            NameRAM.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            NameDisk.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            NameSound.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            IpAddress.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            Ipv4.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            MACaddress.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            NameNetAdapter.MouseLeave += (s, e) => { _textcopy = string.Empty; };
            #endregion
        }

        #region CheckIntCn/getIp
        private bool CheckInternetConnection()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://google.com");
                request.KeepAlive = false;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch { return false; }
        }

        private void GetIpUser()
        {
            if (CheckInternetConnection())
            {
                try
                {
                    string _exIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
                    var _exlIp = IPAddress.Parse(_exIpString);
                    _ipUser = _exlIp.ToString();
                }
                catch { _ipUser = "Доступ к сети ограничен"; }
            }
            else
                _ipUser = "Отсутствует подключения к интернету";
        }
        #endregion

        private void UpdateDisk()
        {
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds%2==0) { _getSystemInformation.UpdateInormation(this); }
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        private void AnimNotf(bool _reverse)
        {
            Notf.Visibility = Visibility.Visible;
            DoubleAnimation _animation = new DoubleAnimation
            {
                From = !_reverse ? 0 :1,
                To = !_reverse ? 1 : 0,
                Duration = TimeSpan.FromSeconds(0.17),
                FillBehavior = FillBehavior.HoldEnd,
            };
            Timeline.SetDesiredFrameRate(_animation, 60);
            Notf.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            Notf.Opacity = !_reverse ? 1 : 0;
        }

        #region CopyText
        private async void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount>=2)
            {
                if (!_sticking)
                {
                    TextBlock _textBlock = (TextBlock)sender;
                    PostionNotf();
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, _textBlock.Text);
                    await Task.Delay(500);
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
                        PostionNotf();
                        _sticking = true;
                        AnimNotf(false);
                        Clipboard.SetData(DataFormats.UnicodeText, _textcopy);
                        await Task.Delay(500);
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

        private void PostionNotf()
        {
            TranslateTransform _translateTransform = new TranslateTransform();
            _translateTransform.X = Mouse.GetPosition(GridContent).X;
            _translateTransform.Y = Mouse.GetPosition(GridContent).Y;
            Notf.RenderTransform = _translateTransform;
        }
    }
}
