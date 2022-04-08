﻿using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class ApplicationsUL : Page
    {
        private ApplicationsSystem applicationsSystem = new ApplicationsSystem();
        public ApplicationsUL()
        {
            InitializeComponent();
            applicationsSystem.SetImageApps(this);
        }

        private void App_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _AppImage = (Image)sender;
            DiscriptionAnim(_AppImage.Name);
        }

        private void App_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Discription.Text != "Наведите курсор на любое приложения, чтобы получить его название")
                DiscriptionAnim("Наведите курсор на любое приложения, чтобы получить его название");
        }

        private void DiscriptionAnim(string _text)
        {
             Discription.Text = _text;
            DoubleAnimation _animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5)
             };
             Discription.BeginAnimation(ContextMenu.OpacityProperty, _animation);
             Discription.Opacity = 1;
        }
    }
}