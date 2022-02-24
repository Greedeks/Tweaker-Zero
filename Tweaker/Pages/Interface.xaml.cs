using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class Interface : Page
    {
        private readonly SettingsWindows settingsWindows = new SettingsWindows();
        public Interface()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            settingsWindows.GetSettingInterface(this);
        }
    }
}
