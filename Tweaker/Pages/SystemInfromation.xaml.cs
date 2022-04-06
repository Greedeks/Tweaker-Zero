﻿using System;
using System.ComponentModel;
using System.Net;
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
        private readonly GetSystemInformation getSystemInformation = new GetSystemInformation();
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);
        private static string _ipUser = "Пожалуйста немного подождите...";
        private bool _error = false;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private static bool _sticking = false;

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

        private void AnimNotf(bool _reverse)
        {
            DoubleAnimation _animation = new DoubleAnimation
            {
                From = !_reverse ? 0 :1,
                To = !_reverse ? 1 : 0,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd,
            };
            Timeline.SetDesiredFrameRate(_animation, 60);
            Notf.BeginAnimation(ContextMenu.OpacityProperty, _animation);
            Notf.Opacity = !_reverse ? 1 : 0;
        }

        #region CopyText
        private async void NameOS_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameOS.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void NameBIOS_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameBIOS.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void NameMotherBr_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameMotherBr.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void NameCPU_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameCPU.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void NameGPU_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameGPU.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void NameRAM_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameRAM.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void NameDisk_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameDisk.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void NameSound_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameSound.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void IpAddress_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, IpAddress.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void Ipv4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, Ipv4.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void MACaddress_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, MACaddress.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }

        private async void NameNetAdapter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_sticking)
                {
                    _sticking = true;
                    AnimNotf(false);
                    Clipboard.SetData(DataFormats.UnicodeText, NameNetAdapter.Text);
                    await Task.Delay(500);
                    AnimNotf(true);
                    _sticking = false;
                }
            }
        }
        #endregion
    }
}
