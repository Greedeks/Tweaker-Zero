using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class Confidentiality : Page
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        public Confidentiality()
        {
            InitializeComponent();
        }

        #region Tweaks
        private void TButton1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak1.Style = !TButton1.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton1.State, 1);
        }

        private void TButton2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak2.Style = !TButton2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton2.State, 2);
        }

        private void TButton3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak3.Style = !TButton3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton3.State, 3);
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak4.Style = !TButton4.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton4.State, 4);
        }

        private void TButton5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak5.Style = !TButton5.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton5.State, 5);
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak6.Style = !TButton6.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton6.State, 6);
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak7.Style = !TButton7.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton7.State, 7);
        }

        private void TButton8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak8.Style = !TButton8.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton8.State, 8);
        }

        private void TButton9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak9.Style = !TButton9.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton9.State, 9);
        }

        private void TButton10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak10.Style = !TButton10.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton10.State, 10);
        }

        private void TButton11_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak11.Style = !TButton11.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton11.State, 11);
        }

        private void TButton12_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak12.Style = !TButton12.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton12.State, 12);
        }

        private void TButton13_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak13.Style = !TButton13.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton13.State, 13);
        }
        private void TButton14_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak14.Style = !TButton14.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton14.State, 14);
        }

        private void TButton15_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak15.Style = !TButton15.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton15.State, 15);
        }

        private void TButton16_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak16.Style = !TButton16.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton16.State, 16);
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _settingsWindows.GetSettingConfidentiality(this);
        }
    }
}
