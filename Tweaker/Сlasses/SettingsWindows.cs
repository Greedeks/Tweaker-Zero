using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Tweaker.Pages;

namespace Tweaker.Сlasses
{
    internal class SettingsWindows
    {
        private readonly RegistryKey classesRootKey = Registry.ClassesRoot, currentUserKey = Registry.CurrentUser,
            localMachineKey = Registry.LocalMachine, usersKey = Registry.Users,
            currentConfigKey = Registry.CurrentConfig;  
        private readonly RegistryKey[] _key = new RegistryKey[100];

        internal void GetSettingConfidentiality(Confidentiality confidentiality)
        {
            //#1
            _key[0] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            _key[1] = localMachineKey.OpenSubKey(@"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth");

            if (_key[0] != null && _key[0].GetValue("Enabled").ToString() != "0" || _key[1] != null && _key[1].GetValue("AllowAdvertising").ToString() != "0")
            {
                confidentiality.TButton1.State = true;
                confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton1.State = false;
                confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#2
            _key[2] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings");
            _key[3] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials");
            _key[4] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language");
            _key[5] = currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization");
            _key[6] = currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows");
            _key[7] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility");

            if (_key[2] != null && _key[2].GetValue("Enabled").ToString() != "0" || _key[3] != null && _key[3].GetValue("Enabled").ToString() != "0" ||
                _key[4] != null && _key[4].GetValue("Enabled").ToString() != "0" || _key[5] != null && _key[5].GetValue("Enabled").ToString() != "0" ||
                _key[6] != null && _key[6].GetValue("Enabled").ToString() != "0" || _key[7] != null && _key[7].GetValue("Enabled").ToString() != "0")
            {
                confidentiality.TButton2.State = true;
                confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton2.State = false;
                confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#3
            _key[8] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener");
            _key[9] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments");
            _key[10] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\DiagTrack");
            _key[11] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\dmwappushservice");

            if (_key[8] != null && _key[8].GetValue("Start").ToString() != "0" || _key[9] != null && _key[9].GetValue("SaveZoneInformation").ToString() != "1" || 
                _key[10] != null && _key[10].GetValue("Start").ToString() != "4" || _key[11] != null && _key[11].GetValue("Start").ToString() != "4")
            {
                confidentiality.TButton3.State = true;
                confidentiality.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton3.State = false;
                confidentiality.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            //#4

        }
    }
}
