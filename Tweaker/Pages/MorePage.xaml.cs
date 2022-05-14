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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StatusVerf.Text = SettingsWindows._verificationW == 1 ? "Активно" : "Неактивно";
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
    }
}
