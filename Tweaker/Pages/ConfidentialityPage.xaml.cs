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
    public partial class ConfidentialityPage : Page
    {
        private readonly SettingsWindows settingsWindows = new SettingsWindows();
        private DispatcherTimer _timer = default;
        private TimeSpan _time = TimeSpan.FromSeconds(0);

        public ConfidentialityPage()
        {
            InitializeComponent();

            #region Update
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time.TotalSeconds % 60 == 0)
                    settingsWindows.GetSettingConfidentiality(this);
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);
            #endregion
        }
        private Dictionary<string, string> TweaksHover = new Dictionary<string, string>
        {
            ["Tweak1"] = "В дополнение к объявлениям, которые вы видите в меню «Пуск» Windows, вам также присваивается уникальный идентификатор для отслеживания каждого вашего шага в магазине Windows и в приложениях, предоставляя вам конкретные рекламные предложения.",
            ["Tweak2"] = "Если вы используете локальную учетную запись, а не Microsoft, то вам следует знать: Windows даже в этом случае всегда пытается синхронизировать ваши данные с серверами Microsoft. В частности, следующее: темы, пароли, настройки Windows, настройки браузера, настройки языковой панели, настройки упрощённого доступа, дополнительные настройки.",
            ["Tweak3"] = "Телеметрия Windows – это совокупность процедур, направленных на сбор и обработку информации о вас и вашем поведении за компьютером. Microsoft с каждой новой версией Windows собирает и изучает всё больше и больше информации о том, какие приложения вы используете, как часто их запускаете, чем больше интересуетесь в интернете и многое-многое другое.",
            ["Tweak4"] = "В «Планировщике заданий» есть целый ряд служб, которые после своей работы собирают данные о своей работе и отправляют их в Microsoft.",
            ["Tweak5"] = "Особое внимание Microsoft уделяет изучению программного обеспечения пользователей. Составляет, так сказать, собственный топ программ и приложений, что даёт возможность обучаться на вас в реализации новых продуктов. Как показывает практика, Microsoft не использует полученные сведения для улучшения программного обеспечения и не развивает платформы для написания программ, если они занимают даже лидирующие позиции. ",
            ["Tweak6"] = "Microsoft не достаточно списка ваших приложений, она изучает ещё и успешность вашей работы в этих приложениях: как часто вы пользуетесь теми или иными приложениями, сколько вы тратите времени на работу в том или ином приложении.",
            ["Tweak7"] = "После внедрения в Windows такой опции, как рукописный ввод, Microsoft однажды решила, что можно улучшать технологию рукописного ввода в своей операционной системе, обучаясь на тех, кто пользуется этой самой Windows. То есть, даже если вы не используете рукописный ввод, Microsoft по умолчанию всё равно узнаёт об этом.",
            ["Tweak8"] = "Инструменты отслеживания действий пользователя не знают предела. Windows имеет ещё один сервис – UAR, призванный выполнять пошаговое протоколирование ваших действий за компьютером, среди которых: снятие скриншотов, набор текста, запуск программ, доступ к веб-камере на заблокированном экране и многое другое.",
            ["Tweak9"] = "Microsoft Windows получает ваше местоположение и использует данную информацию в рекламных целях.",
            ["Tweak10"] = "Даже если вы не используете «Обратную связь», Microsoft всё равно регистрирует частоту ваших обращений в официальную поддержку.",
            ["Tweak11"] = "Microsoft Windows умеет распознавать голос специальным модулем распознавания речи и синтеза речи. Даже если вы не используете Cortana или «Специальные возможности» Windows, ваша операционная система по умолчанию всегда занимается проверкой обновления своего движка распознавания голоса.",
            ["Tweak12"] = "Windows изучает пользователя по разным причинам. Но для пользователя важно одно – этот модуль не приносит пользы и не делает ничего ради пользователя.",
            ["Tweak13"] = "В групповой политике есть служба, направленная на тестирование ещё не вышедших функций на определённых конфигурациях ПК.",
            ["Tweak14"] = "DiagTruck и dmwappushservice - служат они, конечно, не пользователю а компании Microsoft и собирают данные о действиях и бездействиях пользователя.",
            ["Tweak15"] = "А что будет, если сеть недоступна, а всё то добро, которое собирают шпионы, нужно отправить... Есть на этот случай бэкап данных. И бэкапить Windows решила в «Журнал событий Windows».",
            ["Tweak16"] = "Телеметрия NVIDIA – это совокупность процедур, направленных на сбор и обработку информации о вас и вашем поведении за компьютером. NVIDIA решили, что, если Microsoft сходит с рук такой наглый сбор всего и вся с компьютеров пользователя, то почему бы и им не заняться тем же.",
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
                settingsWindows.ChangeSettingConfidentiality(TButton1.State, 1);
            }
        }

        private void TButton2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak2.Style = !TButton2.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton2.State, 2);
            }
        }

        private void TButton3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak3.Style = !TButton3.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton3.State, 3);
            }
        }

        private void TButton4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak4.Style = !TButton4.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton4.State, 4);
                _timer.Start();
            }
        }

        private void TButton5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak5.Style = !TButton5.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton5.State, 5);
            }
        }

        private void TButton6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak6.Style = !TButton6.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton6.State, 6);
            }
        }

        private void TButton7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak7.Style = !TButton7.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton7.State, 7);
            }
        }

        private void TButton8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak8.Style = !TButton8.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton8.State, 8);
            }
        }

        private void TButton9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak9.Style = !TButton9.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton9.State, 9);
            }
        }

        private void TButton10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak10.Style = !TButton10.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton10.State, 10);
            }
        }

        private void TButton11_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak11.Style = !TButton11.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton11.State, 11);
            }
        }

        private void TButton12_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak12.Style = !TButton12.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton12.State, 12);
            }
        }

        private void TButton13_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak13.Style = !TButton13.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton13.State, 13);
            }
        }
        private void TButton14_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak14.Style = !TButton14.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton14.State, 14);
            }
        }

        private void TButton15_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak15.Style = !TButton15.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton15.State, 15);
            }
        }

        private void TButton16_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Tweak16.Style = !TButton16.State ? (Style)Application.Current.Resources["Tweaks_ON"] : (Style)Application.Current.Resources["Tweaks_OFF"];
                settingsWindows.ChangeSettingConfidentiality(TButton16.State, 16);
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
                    if (btn.Name == "BTweaksON")
                    {
                        for (byte _tweak = 1; _tweak <= 16; _tweak++)
                            settingsWindows.ChangeSettingConfidentiality(false, _tweak);
                    }
                    else
                        for (byte _tweak = 1; _tweak <= 16; _tweak++)
                            settingsWindows.ChangeSettingConfidentiality(true, _tweak);

                    settingsWindows.GetSettingConfidentiality(this);
                    _timer.Start();
                }
            });
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) => settingsWindows.GetSettingConfidentiality(this);

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
