using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class ApplicationsUL : Page
    {
        private readonly ApplicationsSystem _applicationsSystem = new ApplicationsSystem();
        public ApplicationsUL()
        {
            InitializeComponent();
            _applicationsSystem.SetImageApps(this);
        }

        private void App_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _AppImage = (Image)sender;
            DiscriptionAnim(_AppImage.Name);
        }

        private void App_MouseLeave(object sender, MouseEventArgs e) => DiscriptionAnim("Наведите курсор на любое приложения, чтобы получить его название");

        private void DiscriptionAnim(string _text)
        {
            Discription.Text = _text;
            DoubleAnimation _animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
             };
             Discription.BeginAnimation(ContextMenu.OpacityProperty, _animation);
             Discription.Opacity = 1;
        }

        private void BRecovery_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if(e.LeftButton == MouseButtonState.Pressed)
             
        }

        private void AppClick_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image _image = (Image)sender;
                foreach(var item in _applicationsSystem.ApplicationRemoval(_image.Name)) {
                    MessageBox.Show(item);
                }
            }
        }
    }
}
