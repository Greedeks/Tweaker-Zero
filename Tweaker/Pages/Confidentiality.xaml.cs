using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tweaker.Pages
{
    public partial class Confidentiality : Page
    {
        public Confidentiality()
        {
            InitializeComponent();
        }

        private void TButton1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!TButton1.State)
            {
                Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
        }
    }
}
