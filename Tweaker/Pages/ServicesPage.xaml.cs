using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class ServicesPage : Page
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);

        public ServicesPage()
        {
            InitializeComponent();

            #region Update
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds % 60 == 0)
                    _settingsWindows.GetSettingServices(this);
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);
            #endregion
        }

        #region Tweaks
        private void TButton1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak1.Style = !TButton1.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton1.State, 1);
            }
        }

        private void TButton2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak2.Style = !TButton2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton2.State, 2);
            }
        }

        private void TButton3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak3.Style = !TButton3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton3.State, 3);
            }
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak4.Style = !TButton4.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton4.State, 4);
            }
        }

        private void TButton5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak5.Style = !TButton5.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton5.State, 5);
            }
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak6.Style = !TButton6.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton6.State, 6);
            }
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak7.Style = !TButton7.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton7.State, 7);
            }
        }

        private void TButton8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak8.Style = !TButton8.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton8.State, 8);
            }
        }

        private void TButton9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak9.Style = !TButton9.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton9.State, 9);
            }
        }

        private void TButton10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak10.Style = !TButton10.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton10.State, 10);
            }
        }

        private void TButton11_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak11.Style = !TButton11.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton11.State, 11);
            }
        }

        private void TButton12_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak12.Style = !TButton12.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton12.State, 12);
            }
        }

        private void TButton13_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak13.Style = !TButton13.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton13.State, 13);
            }
        }
        private void TButton14_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak14.Style = !TButton14.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton14.State, 14);
            }
        }

        private void TButton15_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak15.Style = !TButton15.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton15.State, 15);
            }
        }

        private void TButton16_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak16.Style = !TButton16.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton16.State, 16);
            }
        }

        private void TButton17_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak17.Style = !TButton17.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton17.State, 17);
            }
        }

        private void TButton18_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak18.Style = !TButton18.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton18.State, 18);
            }
        }

        private void TButton19_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak19.Style = !TButton19.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton19.State, 19);
            }
        }

        private void TButton20_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak20.Style = !TButton20.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton20.State, 20);
            }
        }

        private void TButton21_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak21.Style = !TButton21.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton21.State, 21);
            }
        }

        private void TButton22_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak22.Style = !TButton22.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton22.State, 22);
            }
        }

        private void TButton23_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak23.Style = !TButton23.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton23.State, 23);
            }
        }

        private void TButton24_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak24.Style = !TButton24.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton24.State, 24);
            }
        }

        private void TButton25_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak25.Style = !TButton25.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton25.State, 25);
            }
        }

        private void TButton26_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak26.Style = !TButton26.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton26.State, 26);
            }
        }

        private void TButton27_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak27.Style = !TButton27.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton27.State, 27);
            }
        }

        private void TButton28_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak28.Style = !TButton28.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton28.State, 28);
            }
        }
        #endregion

        private void BtnOnOff_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Parallel.Invoke(() =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Button btn = (Button)sender;
                    if (btn.Name == "TweaksON")
                    {
                        for (byte _tweak = 1; _tweak <= 28; _tweak++)
                            _settingsWindows.ChangeSettingServices(false, _tweak);
                    }
                    else
                        for (byte _tweak = 1; _tweak <= 28; _tweak++)
                            _settingsWindows.ChangeSettingServices(true, _tweak);

                    _settingsWindows.GetSettingServices(this);
                    _timer.Start();
                }
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) => Parallel.Invoke(() => { _settingsWindows.GetSettingServices(this); });

        private void Page_Unloaded(object sender, RoutedEventArgs e) => _timer.Stop();
    }
}
