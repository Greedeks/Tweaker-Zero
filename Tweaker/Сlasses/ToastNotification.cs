﻿using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Tweaker.Windows;

namespace Tweaker.Сlasses
{
    internal class ToastNotification
    {
        private NotificationWindow notificationWindow = new NotificationWindow();
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();
        string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

        internal void Show(string _Tittle, string _Text, byte _Action)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Parallel.Invoke(() =>
                {
                    if (notificationWindow.IsLoaded == false)
                    {
                        notificationWindow = new NotificationWindow
                        {
                            AddTitle = _Tittle,
                            AddText = _Text,
                            ActionChoice = _Action,
                        };
                        notificationWindow.Show();

                        Parallel.Invoke(() =>
                        {
                            _mediaPlayer.Open(new Uri(_path + @"\Tweaker Zero\ToastSound.mp3"));
                            _mediaPlayer.Play();
                        });
                    }
                });
            });
        }

        internal void Load()
        {
            Parallel.Invoke(() =>
            {
                byte[] _soundbyte = default;
                using (MemoryStream fileOut = new MemoryStream(Properties.Resources.windowsNotf))
                using (GZipStream gz = new GZipStream(fileOut, CompressionMode.Decompress))
                using (MemoryStream ms = new MemoryStream())
                {
                    gz.CopyTo(ms);
                    _soundbyte = ms.ToArray();
                }

                Directory.CreateDirectory(_path + "/Tweaker Zero");
                File.WriteAllBytes(_path + @"\Tweaker Zero\ToastSound.mp3", _soundbyte);

                Volume(0);
                _mediaPlayer.Open(new Uri(_path + @"\Tweaker Zero\ToastSound.mp3"));
                _mediaPlayer.Play();
            });
        }

        internal void Unloading()
        {
            _mediaPlayer.Close();
            Directory.Delete(_path + @"\Tweaker Zero", true);
        }

        internal void Volume(byte _value) => Parallel.Invoke(() => { _mediaPlayer.Volume = _value / 100.0f; });
    }
}
