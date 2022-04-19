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
            _settingsWindows.ChangeSettingConfidentiality(TButton1.State, 0);
        }

        private void TButton2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak2.Style = !TButton2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton2.State, 1);
        }

        private void TButton3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tweak3.Style = !TButton3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            _settingsWindows.ChangeSettingConfidentiality(TButton3.State, 3);
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton4.State)
            {
                Tweak4.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak4.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton5.State)
            {
                Tweak5.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak5.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton6.State)
            {
                Tweak6.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak6.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton7.State)
            {
                Tweak7.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak7.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
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
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _settingsWindows.GetSettingConfidentiality(this);
        }
    }
}
