﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class SettingsPage : Page
    {
        private SettingsTweaker settingsTweaker = new SettingsTweaker();
        public SettingsPage()
        {
            InitializeComponent();

            TSettings1.State = SettingsTweaker.Notification;
            ToastShow.Style = TSettings1.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            TSettings2.State = SettingsTweaker.NotificationSound;
            ToastSound.Style = TSettings2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
            Slider1.Value = SettingsTweaker.NotificationVolume;
            TSettings3.State = SettingsTweaker.TopMost;
            PinOfTop.Style = TSettings3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
        }

        private void TSettings1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                ToastShow.Style = !TSettings1.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                if(TSettings1.State == false)
                    settingsTweaker.ChangeNotificationState(true);
                else
                    settingsTweaker.ChangeNotificationState(false);
            }
        }

        private void TSettings2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ToastSound.Style = !TSettings2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                if (TSettings2.State == false)
                    settingsTweaker.ChangeNotificationSoundState(true);
                else
                    settingsTweaker.ChangeNotificationSoundState(false);
            }
        }

        private void Slider1_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            settingsTweaker.ChangeNotificationVolume((byte)Slider1.Value);
        }

        private void TSettings3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                PinOfTop.Style = !TSettings3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                if (TSettings3.State == false)
                    settingsTweaker.ChangeTopMostState(true);
                else
                    settingsTweaker.ChangeTopMostState(false);
            }
        }

        private void TSettings4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                settingsTweaker.SelfRemoval();
            }
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
