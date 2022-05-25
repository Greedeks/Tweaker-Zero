using System;
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
    public partial class InterfacePage : Page
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);
        public InterfacePage()
        {
            InitializeComponent();

            if (GetSystemInformation._windowsV.Substring(0, GetSystemInformation._windowsV.LastIndexOf(' ')) != "11")
            {
                TButton17.IsEnabled = false; TButton18.IsEnabled = false; TButton19.IsEnabled = false; TButton20.IsEnabled = false;
            }

            #region Update
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds % 60 == 0)
                    _settingsWindows.GetSettingInterface(this);
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);
            #endregion
        }
        private Dictionary<string, string> TweaksHover = new Dictionary<string, string>
        {
            ["Tweak1"] = "По умолчанию, дополнительные пункты контекстного меню появляются с задержкой 400 мс. Если вы хотите быстрее, можно эту задежку сократить до 20 мс.",
            ["Tweak2"] = "Данный твик уберет пункты контекстного меню, которые мало кто использует: Отправить (поделиться), Передать на устройство, Изменить с помощью приложения «Фотографии», Добавить в библиотеку, Поиск музыки в Интернете, Изменить в Paint 3D / Print 3D.",
            ["Tweak3"] = "Удаляет из контекстного меню Создать пункты: Точечный рисунок, Контакт, Документ в формате RTF.",
            ["Tweak4"] = "Изменят цвет всплывающей подсказки с жёлтого на «Небесная лазурь».",
            ["Tweak5"] = "Твик уменьшит размер кнопок Свернуть, Развернуть, Закрыть в окнах на 20%. Кнопки станут аккуратнее не только у Проводника, но и у всех окон, включая изображения, браузеры и прочие программы.",
            ["Tweak6"] = "Уберёт из Обзора Проводника в левом списке меню (из дерева списка проводника) пункт 3D Objects (Объёмные объекты)",
            ["Tweak7"] = "Твик скроет из «Этот компьютер» папки: Рабочий стол, Видео, Документы, Загрузки, Изображения, Музыка.",
            ["Tweak8"] = "Твик позволит убрать стрелки с ярлыков с помощью подмены файла стрелки.",
            ["Tweak9"] = "Удаление слишком интуитивного префикса-постфикса для ярлыков.",
            ["Tweak10"] = "Ускоряет частоту мерцания курсора, по умолчанию значение: 530. Данный твик изменит значение на 250.",
            ["Tweak11"] = "По умолчанию, предпросмотр на Панели задач показывает превью запущенных приложений слишком долго. Чтобы превьюшки показывались быстрее, примените данный твик.",
            ["Tweak12"] = "Если вам не нужен значок Корзины на рабочем столе и вы не хотите тратить время на поиски отключение, то можете воспользоваться твикам.",
            ["Tweak13"] = "Для тех, кому Центр уведомлений не нужен вовсе, твик позволит полностью отключить его и убрать иконку в трее.",
            ["Tweak14"] = "Твик сделает scroll-bar в окнах, включая браузеры и программы, тоньше на 17%.",
            ["Tweak15"] = "Значок Компьютера, в отличие от ярлыка Компьютера, лучше тем, что он имеет нужные свойства контекстного меню и избавлен от лишних.",
            ["Tweak16"] = "По умолчанию, Windows не сохраняет позиции окон, как и их состояние при Выключении. Этот твик позволит запомнить вид открытых папок, чтобы восстановить их после Выхода, Перезагрузки и Выключения.",
            ["Tweak17"] = "Нет времени на поиски? Твик уберет значки с панели задач: Поиск, Предаставление задач, Чат.",
            ["Tweak18"] = "Вы можете отключить персонализированную рекламу во всех приложениях Магазина. По умолчанию Windows использует собранную информацию для таргетинга рекламы, т. е. для показа рекламы, соответствующей вашим интересам.",
            ["Tweak19"] = "Непонятно зачем, но Windows 11 заменяет нормальное контекстное меню, новым урезанным. В катором даже нет пукнта:Свойства. И для того чтобы его открыть, необходимо воспользоваться старым меню. Твик исправит данный велосипед, и вы сможете сразу открыть нормальное контекстное меню.",
            ["Tweak20"] = "Windows 11 иногда показывает подсказки о том, как использовать ту или иную функцию, например, новое меню «Пуск» или «Быстрые настройки». Они полезны, если вы впервые видите Windows 11, а так это бесполезный хлам.",
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
                _settingsWindows.ChangeSettingInterface(TButton1.State, 1);
            }
        }

        private void TButton2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak2.Style = !TButton2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton2.State, 2);
            }
        }

        private void TButton3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak3.Style = !TButton3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton3.State, 3);
            }
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak4.Style = !TButton4.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton4.State, 4);
            }
        }

        private void TButton5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak5.Style = !TButton5.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton5.State, 5);
            }
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak6.Style = !TButton6.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton6.State, 6);
            }
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak7.Style = !TButton7.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton7.State, 7);
            }
        }

        private void TButton8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak8.Style = !TButton8.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton8.State, 8);
            }
        }

        private void TButton9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak9.Style = !TButton9.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton9.State, 9);
            }
        }

        private void TButton10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak10.Style = !TButton10.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton10.State, 10);
            }
        }

        private void TButton11_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak11.Style = !TButton11.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton11.State, 11);
            }
        }

        private void TButton12_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak12.Style = !TButton12.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton12.State, 12);
            }
        }

        private void TButton13_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak13.Style = !TButton13.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton13.State, 13);
            }
        }
        private void TButton14_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak14.Style = !TButton14.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton14.State, 14);
            }
        }

        private void TButton15_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak15.Style = !TButton15.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton15.State, 15);
            }
        }

        private void TButton16_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak16.Style = !TButton16.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton16.State, 16);
            }
        }

        private void TButton17_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak17.Style = !TButton17.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton17.State, 17);
            }
        }

        private void TButton18_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak18.Style = !TButton18.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton18.State, 18);
            }
        }

        private void TButton19_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak19.Style = !TButton19.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton19.State, 19);
            }
        }

        private void TButton20_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak20.Style = !TButton20.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                _settingsWindows.ChangeSettingInterface(TButton20.State, 20);
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
                        for (byte _tweak = 1; _tweak <= 20; _tweak++)
                            _settingsWindows.ChangeSettingInterface(false, _tweak);
                    }
                    else
                        for (byte _tweak = 1; _tweak <= 20; _tweak++)
                            _settingsWindows.ChangeSettingInterface(true, _tweak);

                    _settingsWindows.GetSettingInterface(this);
                    _timer.Start();
                }
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) => Parallel.Invoke(() => { _settingsWindows.GetSettingInterface(this); });

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
