using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class Interface : Page
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        public Interface()
        {
            InitializeComponent();
        }

        #region Tweaks
        private void TButton1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak1.Style = !TButton1.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton1.State, 1);
            }
        }

        private void TButton2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak2.Style = !TButton2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton2.State, 2);
            }
        }

        private void TButton3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak3.Style = !TButton3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton3.State, 3);
            }
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak4.Style = !TButton4.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton4.State, 4);
            }
        }

        private void TButton5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak5.Style = !TButton5.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton5.State, 5);
            }
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak6.Style = !TButton6.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton6.State, 6);
            }
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak7.Style = !TButton7.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton7.State, 7);
            }
        }

        private void TButton8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton8.State)
            {
                Tweak8.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak8.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton9.State)
            {
                Tweak9.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak9.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton10.State)
            {
                Tweak10.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak10.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton11_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton11.State)
            {
                Tweak11.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak11.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton12_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton12.State)
            {
                Tweak12.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak12.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton13_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton13.State)
            {
                Tweak13.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak13.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }
        private void TButton14_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton14.State)
            {
                Tweak14.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak14.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton15_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton15.State)
            {
                Tweak15.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak15.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton16_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton16.State)
            {
                Tweak16.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak16.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton17_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton17.State)
            {
                Tweak17.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak17.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton18_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton18.State)
            {
                Tweak18.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak18.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton19_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton19.State)
            {
                Tweak19.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak19.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton20_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton20.State)
            {
                Tweak20.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak20.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _settingsWindows.GetSettingInterface(this);
        }
    }
}
