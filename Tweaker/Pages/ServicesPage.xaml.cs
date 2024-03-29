﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class ServicesPage : Page
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);
        public ServicesPage()
        {
            InitializeComponent();

            #region Update
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds % 60 == 0)
                    _settingsWindows.GetSettingServices(this);
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);
            #endregion
        }

        private Dictionary<string, string> TweaksHover = new Dictionary<string, string>
        {
            ["Tweak1"] = "Отключайте если вы не используете историю файлов и индексирование контента.",
            ["Tweak2"] = "Отключайте если вы не используете Xbox и геймпады Microsoft.",
            ["Tweak3"] = "Отключайте если вы подключены к интернету через кабель или Wi-Fi.",
            ["Tweak4"] = "Отключайте если вы не используете Магазин Windows и его UWP приложения.",
            ["Tweak5"] = "Отключайте если вы не хотите, чтобы службы анализировали производительность вашей системы.",
            ["Tweak6"] = "Отключайте если вы не используете авторизацию с помощью Windows Hello, по отпечатку пальца или распзования лица.",
            ["Tweak7"] = "Отключайте если вы не используете Bluetooth и устройства чья работа связана с Bluetooth",
            ["Tweak8"] = "Отключайте если у вас нет принтера.",
            ["Tweak9"] = "Отключайте если у вас нет сканера.",
            ["Tweak10"] = "Отключайте если у вас нет факса.",
            ["Tweak11"] = "Отключайте если у вас стационарный компьютер, ноутбук или нетбук.",
            ["Tweak12"] = "Отключайте если у вас только один монитор и вы не планируете подключать дополнительные.",
            ["Tweak13"] = "Отключайте если вы не используете все эти приложения.",
            ["Tweak14"] = "Отключайте если вы не используете и не планируете использовать локальную сеть",
            ["Tweak15"] = "Отключайте если вы не планируете обновлять Windows.",
            ["Tweak16"] = "Отключайте если вы не используете VPN-клиенты. Может повлиять на работу Xbox.",
            ["Tweak17"] = "Отключайте если вы не используете Проигрыватель Windows Media.",
            ["Tweak18"] = "Отключайте если вы не пользуетесь удалённым рабочим столом и не используете серверное подключение к ним.",
            ["Tweak19"] = "Отключайте если вы не хотите чтобы службы нагружали ваш ПК, пока они занимаются обработкой ошибок.",
            ["Tweak20"] = "Отключайте если вы не управляете удаленно файлами через Общий доступ или с помощью WebDAV.",
            ["Tweak21"] = "Отключайте если вы не используете смарт-карты или не знаете что это.",
            ["Tweak22"] = "Отключайте если вы не используете Windows Киоск или не знаете что это.",
            ["Tweak23"] = "Отключайте если вы не используете шифрования дисков BitLocker и шифрования файлов или папок EFS",
            ["Tweak24"] = "Отключайте если вы настроили все языки, которые будете использовать и не планируете добавлять новые.",
            ["Tweak25"] = "Отключайте если вы не используете Защитник Windows и всю дополнительные безопасность Windows.",
            ["Tweak26"] = "Отключайте если вы не хотите, чтобы в фоновом режиме Microsoft искала, что нибудь для диагностики.",
            ["Tweak27"] = "Отключайте если вы не используете удалённое управления корпоративными приложениями.",
            ["Tweak28"] = "Отключайте если вы не используете встроенную систему виртуализации. На работу VirtualBox и ему подобных, никак не влияет."
        };

        private void DiscriptionAnim(string _text)
        {
            DescriptionT.Text = _text;

            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(0)
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
            };

            EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame(1)
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150))
            };

            doubleAnimation.KeyFrames.Add(_fromFrame);
            doubleAnimation.KeyFrames.Add(_toFrame);
            DescriptionT.BeginAnimation(ContextMenu.OpacityProperty, doubleAnimation);
        }

        private void Tweaks_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            DiscriptionAnim(TweaksHover[label.Name]);
        }

        private void Tweaks_MouseLeave(object sender, MouseEventArgs e) => DiscriptionAnim("Наведите указатель мыши на любую функцию, чтобы получить ее описание");

        #region Tweaks
        private void TButton1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak1.Style = !TButton1.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton1.State, 1);
            }
        }

        private void TButton2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak2.Style = !TButton2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton2.State, 2);
            }
        }

        private void TButton3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak3.Style = !TButton3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton3.State, 3);
            }
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak4.Style = !TButton4.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton4.State, 4);
            }
        }

        private void TButton5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak5.Style = !TButton5.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton5.State, 5);
            }
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak6.Style = !TButton6.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton6.State, 6);
            }
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak7.Style = !TButton7.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton7.State, 7);
            }
        }

        private void TButton8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak8.Style = !TButton8.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton8.State, 8);
            }
        }

        private void TButton9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak9.Style = !TButton9.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton9.State, 9);
            }
        }

        private void TButton10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak10.Style = !TButton10.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton10.State, 10);
            }
        }

        private void TButton11_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak11.Style = !TButton11.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton11.State, 11);
            }
        }

        private void TButton12_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak12.Style = !TButton12.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton12.State, 12);
            }
        }

        private void TButton13_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak13.Style = !TButton13.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton13.State, 13);
            }
        }
        private void TButton14_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak14.Style = !TButton14.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton14.State, 14);
            }
        }

        private void TButton15_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak15.Style = !TButton15.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton15.State, 15);
            }
        }

        private void TButton16_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak16.Style = !TButton16.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton16.State, 16);
            }
        }

        private void TButton17_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak17.Style = !TButton17.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton17.State, 17);
            }
        }

        private void TButton18_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak18.Style = !TButton18.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton18.State, 18);
            }
        }

        private void TButton19_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak19.Style = !TButton19.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton19.State, 19);
            }
        }

        private void TButton20_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak20.Style = !TButton20.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton20.State, 20);
            }
        }

        private void TButton21_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak21.Style = !TButton21.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton21.State, 21);
            }
        }

        private void TButton22_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak22.Style = !TButton22.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton22.State, 22);
            }
        }

        private void TButton23_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak23.Style = !TButton23.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton23.State, 23);
            }
        }

        private void TButton24_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak24.Style = !TButton24.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton24.State, 24);
            }
        }

        private void TButton25_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak25.Style = !TButton25.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton25.State, 25);
            }
        }

        private void TButton26_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak26.Style = !TButton26.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton26.State, 26);
            }
        }

        private void TButton27_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak27.Style = !TButton27.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton27.State, 27);
            }
        }

        private void TButton28_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak28.Style = !TButton28.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingServices(TButton28.State, 28);
            }
        }
        #endregion

        private void BtnOnOff_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Parallel.Invoke(() =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Button btn = (Button)sender;
                    if (btn.Name == "TweaksON")
                    {
                        for (byte _tweak = 1; _tweak <= 28; _tweak++)
                            _settingsWindows.ChangeSettingServices(false, _tweak);
                    }
                    else
                        for (byte _tweak = 1; _tweak <= 28; _tweak++)
                            _settingsWindows.ChangeSettingServices(true, _tweak);

                    _settingsWindows.GetSettingServices(this);
                    _timer.Start();
                }
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) => Parallel.Invoke(() => { _settingsWindows.GetSettingServices(this); });

        private void Page_Unloaded(object sender, RoutedEventArgs e) => _timer.Stop();

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                e.Handled = true;
            }
        }
    }
}
