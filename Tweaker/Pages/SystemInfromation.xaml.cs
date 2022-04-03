using System;
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

        public SystemInfromation()
        {
            InitializeComponent();
            if (GetSystemInformation._urlImage != null) UserAvatar.ImageSource = GetSystemInformation._urlImage;
            UserName.Text = getSystemInformation.NameUser();
            getSystemInformation.SetInormationPC(this);
            UpdateDisk();
        }

        private void UpdateDisk()
        {
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds%2==0) { getSystemInformation.UpdateInormationDisk(this); }
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }
    }
}
