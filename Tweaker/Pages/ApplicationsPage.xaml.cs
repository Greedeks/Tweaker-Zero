using System.Windows.Controls;
using System.Windows.Input;

namespace Tweaker.Pages
{
    public partial class ApplicationsUL : Page
    {
        public ApplicationsUL()
        {
            InitializeComponent();
        }

        private void App_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _AppImage = (Image)sender;
            Discription.Text = _AppImage.Name;
        }

        private void BingWeather_MouseLeave(object sender, MouseEventArgs e) => Discription.Text = "Наведите курсор на любое приложения, чтобы получить его название";
    }
}
