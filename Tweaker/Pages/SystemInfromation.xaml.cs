using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class SystemInfromation : Page
    {
        private readonly GetSystemInformation getSystemInformation = new GetSystemInformation();
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);
        private static string _ipUser = "Пожалуйста немного подождите...";
        private bool _error = false;
        private BackgroundWorker _worker = new BackgroundWorker();

        public SystemInfromation()
        {
            InitializeComponent();

            _worker.DoWork += (s, e) => { GetIpUser(); };
            _worker.RunWorkerCompleted += (s, e) => { IpAddress.Text = _ipUser; };
            _worker.RunWorkerAsync();
            

            if (GetSystemInformation._urlImage != null) UserAvatar.ImageSource = GetSystemInformation._urlImage;
            UserName.Text = getSystemInformation.NameUser();
            getSystemInformation.SetInormationPC(this);
            IpAddress.Text = _ipUser;
            UpdateDisk();
        }

        private void UpdateDisk()
        {
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds%2==0) { getSystemInformation.UpdateInormation(this);}
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _worker.Dispose();
        }

        private void GetIpUser()
        {
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://google.com");
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            }
            catch (Exception ex)
            {

                if (ex.Data != null)
                    _error = true;
            }
            finally
            {
                if (!_error)
                {
                    try
                    {
                        string _exIpString = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
                        var _exlIp = IPAddress.Parse(_exIpString);
                        _ipUser = _exlIp.ToString();
                    }
                    catch
                    {
                        _ipUser = "Нет подключения к интернету или доступ к сети ограничен";
                    }
                }
                else
                {
                    _ipUser = "Нет подключения к интернету или доступ к сети ограничен";
                }
            }
        }
    }
}
