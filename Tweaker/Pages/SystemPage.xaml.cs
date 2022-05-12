using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class SystemPage : Page
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);
        private const UInt32 SPI_SETMOUSE = 0x0004;

        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(UInt32 uiAction, UInt32[] uiParam, UInt32[] pvParam, UInt32 fWinIni);
        public SystemPage()
        {
            InitializeComponent();

            #region Update
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds % 60 == 0)
                    _settingsWindows.GetSettingSystem(this);
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);
            #endregion
        }

        #region Tweaks
        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _settingsWindows.ChangeSettingSystem(true, 1, Convert.ToUInt32(Slider1.Value));
        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _settingsWindows.ChangeSettingSystem(true, 2, Convert.ToUInt32(Slider2.Value));
        }

        private void Slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _settingsWindows.ChangeSettingSystem(true, 3, Convert.ToUInt32(Slider3.Value));
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (TButton4.State)
                {
                    uint[] _acc = new uint[3] { 0, 0, 0 };
                    SystemParametersInfo(SPI_SETMOUSE, _acc, _acc, 2);
                }
                else
                {
                    uint[] _acc = new uint[3] { 1, 6, 10 };
                    SystemParametersInfo(SPI_SETMOUSE, _acc, _acc, 2);
                }
                Tweak4.Style = !TButton4.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton4.State, 4, 0);
            }
        }

        private void Button5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _settingsWindows.ChangeSettingSystem(true, 5, 0);
            }
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak6.Style = !TButton6.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton6.State, 6, 0);
            }
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak7.Style = !TButton7.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton7.State, 7, 0);
            }
        }

        private void TButton8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak8.Style = !TButton8.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton8.State, 8, 0);
            }
        }

        private void TButton9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak9.Style = !TButton9.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton9.State, 9, 0);
            }
        }

        private void TButton10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak10.Style = !TButton10.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton10.State, 10, 0);
            }
        }

        private void TButton11_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak11.Style = !TButton11.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton11.State, 11, 0);
            }
        }

        private void TButton12_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak12.Style = !TButton12.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton12.State, 12, 0);
            }
        }

        private void TButton13_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak13.Style = !TButton13.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton13.State, 13, 0);
            }
        }
        private void TButton14_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak14.Style = !TButton14.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton14.State, 14, 0);
            }
        }

        private void TButton15_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak15.Style = !TButton15.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton15.State, 15, 0);
            }
        }

        private void TButton16_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak16.Style = !TButton16.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingSystem(TButton16.State, 16, 0);
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

                    //_settingsWindows.GetSettingServices(this);
                    _timer.Start();
                }
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) => Parallel.Invoke(() => { _settingsWindows.GetSettingSystem(this); });

        private void Page_Unloaded(object sender, RoutedEventArgs e) => _timer.Stop();

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                e.Handled = true;
            }
        }
    }
}
