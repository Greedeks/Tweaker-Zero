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

        private void Button1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if(StatusVerf.Text == "Неактивно")
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
    }
}
