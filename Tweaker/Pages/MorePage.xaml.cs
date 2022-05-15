using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class MorePage : Page
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        public MorePage()
        {
            InitializeComponent();

            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) != "11")
                TButton3.IsEnabled = false;
        }

        #region Tweaks
        private void Button1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (StatusVerf.Text == "Неактивно")
                    _settingsWindows.ChangeSettingMore(true, 1);
            }
        }

        private void TButton2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak2.Style = !TButton2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton2.State, 2);
            }
        }

        private void TButton3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak3.Style = !TButton3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton3.State, 3);
            }
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak4.Style = !TButton4.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton4.State, 4);
            }
        }

        private void TButton5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak5.Style = !TButton5.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton5.State, 5);
            }
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak6.Style = !TButton6.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton6.State, 6);
            }
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak7.Style = !TButton7.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton7.State, 7);
            }
        }

        private void TButton8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak8.Style = !TButton8.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton8.State, 8);
            }
        }

        private void TButton9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak9.Style = !TButton9.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton9.State, 9);
            }
        }

        private void TButton10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak10.Style = !TButton10.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton10.State, 10);
            }
        }

        private void TButton11_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak11.Style = !TButton11.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton11.State, 11);
            }
        }

        private void TButton12_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak12.Style = !TButton12.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton12.State, 12);
            }
        }

        private void TButton13_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak13.Style = !TButton13.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton13.State, 13);
            }
        }

        private void TButton14_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak14.Style = !TButton14.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton14.State, 14);
            }
        }

        private void TButton15_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak15.Style = !TButton14.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingMore(TButton15.State, 15);
            }
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StatusVerf.Text = SettingsWindows._verificationW == 1 ? "Активно" : "Неактивно";
            _settingsWindows.GetSettingMore(this);
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                e.Handled = true;
            }
        }
    }
}
