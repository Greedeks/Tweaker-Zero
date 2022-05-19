﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Tweaker.Сlasses;

namespace Tweaker
{
    public partial class MainWindow : Window
    {
        #region Параметры
        private bool _confidentialityB = false, _interfaceB = false, _applicationB = false, _servicesB = false,
            _systemB = false, _systeminfoB = false, _moreB = false, _settings = false;
        private readonly StartScanner _startScanner = new StartScanner();
        #endregion

        public MainWindow()
        {
            _startScanner.BeforeLoadingCheck();

            InitializeComponent();
        }

        #region Перемещения/Закрытие/Сворачивание Формы
        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Exit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.Close();
        }

        private void Button_Minimized_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Анимации
        private void ActivePageAnim(bool _stateAnimActivePage)
        {
            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();

            if (_stateAnimActivePage)
            {
                EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(0)
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
                };

                EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame(1)
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200))
                };

                doubleAnimation.KeyFrames.Add(_fromFrame);
                doubleAnimation.KeyFrames.Add(_toFrame);
                ActivePage.BeginAnimation(ContextMenu.OpacityProperty, doubleAnimation);
            }
            else
            {
                EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(1)
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
                };

                EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame(0)
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200))
                };

                doubleAnimation.KeyFrames.Add(_fromFrame);
                doubleAnimation.KeyFrames.Add(_toFrame);
                ActivePage.BeginAnimation(ContextMenu.OpacityProperty, doubleAnimation);

            }
        }
        private void SettingsPanelAnim(bool _stateAnimSettingsPanel)
        {
            Parallel.Invoke(() =>
            {
                Unclickable.Width = _stateAnimSettingsPanel ? 1084 : 0;
                Unclickable.Height = _stateAnimSettingsPanel ? 509 : 0;
            });

            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();

            if (_stateAnimSettingsPanel)
            {
                EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(0)
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
                };

                EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame(400)
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200))
                };

                doubleAnimation.KeyFrames.Add(_fromFrame);
                doubleAnimation.KeyFrames.Add(_toFrame);
                SettingsPanel.BeginAnimation(FrameworkElement.WidthProperty, doubleAnimation);

                doubleAnimation = new DoubleAnimationUsingKeyFrames();
                _fromFrame = new EasingDoubleKeyFrame(1)
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
                };

                _toFrame = new EasingDoubleKeyFrame(0.5)
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100))
                };

                doubleAnimation.KeyFrames.Add(_fromFrame);
                doubleAnimation.KeyFrames.Add(_toFrame);
                MainContainer.BeginAnimation(ContextMenu.OpacityProperty, doubleAnimation);
            }
            else
            {
                EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(400)
                {

                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
                };

                EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame(0)
                {

                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(60))
                };

                doubleAnimation.KeyFrames.Add(_fromFrame);
                doubleAnimation.KeyFrames.Add(_toFrame);
                SettingsPanel.BeginAnimation(FrameworkElement.WidthProperty, doubleAnimation);

                doubleAnimation = new DoubleAnimationUsingKeyFrames();
                _fromFrame = new EasingDoubleKeyFrame(0.5)
                {

                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
                };

                _toFrame = new EasingDoubleKeyFrame(1)
                {

                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(250))
                };

                doubleAnimation.KeyFrames.Add(_fromFrame);
                doubleAnimation.KeyFrames.Add(_toFrame);
                MainContainer.BeginAnimation(ContextMenu.OpacityProperty, doubleAnimation);
            }


        }
        #endregion

        private void StandStateBtnN()
        {
            _confidentialityB = _interfaceB = _applicationB
            = _servicesB = _systemB = _systeminfoB = _moreB = _settings = false;

            Button_Confidentiality.Style = Button_Interface.Style = Button_Application.Style = Button_Services.Style = Button_System.Style
            = Button_SystemInfo.Style = Button_More.Style = (Style)Application.Current.Resources["ButtonNav"];
        }

        private void CleaningPages()
        {
            Parallel.Invoke(() =>
            {
                while (MainContainer.NavigationService.RemoveBackEntry() != null) ;
                MainContainer.Content = null;
            });
        }

        #region Кнопки
        private void Button_Navigations_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed & !_settings)
            {
                Parallel.Invoke(() =>
                {
                    ActivePageAnim(false);
                    CleaningPages();

                    Button btn = (Button)sender;
                    switch (btn.Name)
                    {
                        case "Button_Confidentiality":
                            if (!_confidentialityB)
                            {
                                StandStateBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 0);

                                ActivePageAnim(true);
                                MainContainer.Content = new Pages.ConfidentialityPage();
                                _confidentialityB = true;
                            }
                            else
                            {
                                StandStateBtnN();
                                ActivePageAnim(false);
                                CleaningPages();
                            }
                            break;
                        case "Button_Interface":
                            if (!_interfaceB)
                            {
                                StandStateBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 1);

                                ActivePageAnim(true);
                                MainContainer.Content = new Pages.InterfacePage();
                                _interfaceB = true;
                            }
                            else
                            {
                                StandStateBtnN();
                                ActivePageAnim(false);
                                CleaningPages();
                            }
                            break;
                        case "Button_Application":
                            if (!_applicationB)
                            {
                                StandStateBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 2);

                                ActivePageAnim(true);
                                MainContainer.Content = new Pages.ApplicationsPage();
                                _applicationB = true;
                            }
                            else
                            {
                                StandStateBtnN();
                                ActivePageAnim(false);
                                CleaningPages();
                            }
                            break;
                        case "Button_Services":
                            if (!_servicesB)
                            {
                                StandStateBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 3);

                                ActivePageAnim(true);
                                MainContainer.Content = new Pages.ServicesPage();
                                _servicesB = true;
                            }
                            else
                            {
                                StandStateBtnN();
                                ActivePageAnim(false);
                                CleaningPages();
                            }
                            break;
                        case "Button_System":
                            if (!_systemB)
                            {
                                StandStateBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 4);

                                ActivePageAnim(true);
                                MainContainer.Content = new Pages.SystemPage();
                                _systemB = true;
                            }
                            else
                            {
                                StandStateBtnN();
                                ActivePageAnim(false);
                                CleaningPages();
                            }
                            break;
                        case "Button_SystemInfo":
                            if (!_systeminfoB)
                            {
                                StandStateBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 5);

                                ActivePageAnim(true);
                                MainContainer.Content = new Pages.SystemInfromation();
                                _systeminfoB = true;
                            }
                            else
                            {
                                StandStateBtnN();
                                ActivePageAnim(false);
                                CleaningPages();
                            }
                            break;
                        case "Button_More":
                            if (!_moreB)
                            {
                                StandStateBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 6);

                                ActivePageAnim(true);
                                MainContainer.Content = new Pages.MorePage();
                                _moreB = true;
                            }
                            else
                            {
                                StandStateBtnN();
                                ActivePageAnim(false);
                                CleaningPages();
                            }
                            break;
                        default:
                            StandStateBtnN();
                            ActivePageAnim(false);
                            CleaningPages();
                            break;
                    }
                });
            }

        }

        private void Button_Settings_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!_settings)
                {
                    Parallel.Invoke(() =>
                    {
                        SettingsPanelAnim(true);
                        _settings = true;
                        SettingsContrainer.Content = new Pages.SettingsPage();
                    });
                }
                else
                {
                    Parallel.Invoke(() =>
                    {
                        while (SettingsContrainer.NavigationService.RemoveBackEntry() != null) ;
                        SettingsContrainer.Content = null;

                        SettingsPanelAnim(false);
                        _settings = false;
                    });
                }
            }
        }
        #endregion

        private async void TweakerWPF_Loaded(object sender, RoutedEventArgs e)
        {
            _startScanner.ScantheSystem();
            TweakerWPF.Topmost = SettingsTweaker.TopMost;

            #region Анимация загрузки
            this.Opacity = 0;
            await Task.Delay(100);
            this.Opacity = 1;

            Rect _primaryMonitorArea = SystemParameters.WorkArea;

            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(_primaryMonitorArea.Bottom)
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
            };

            EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame((_primaryMonitorArea.Bottom / 2) - (this.Height / 2))
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(270))
            };

            doubleAnimation.KeyFrames.Add(_fromFrame);
            doubleAnimation.KeyFrames.Add(_toFrame);
            TweakerWPF.BeginAnimation(Canvas.TopProperty, doubleAnimation);

            doubleAnimation = new DoubleAnimationUsingKeyFrames();
            _fromFrame = new EasingDoubleKeyFrame(-_primaryMonitorArea.Right)
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
            };

            _toFrame = new EasingDoubleKeyFrame((_primaryMonitorArea.Right / 2) - (this.Width / 2))
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(270))
            };

            doubleAnimation.KeyFrames.Add(_fromFrame);
            doubleAnimation.KeyFrames.Add(_toFrame);
            TweakerWPF.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            #endregion

        }

        private void TweakerWPF_Closed(object sender, EventArgs e) => _startScanner._toastNotification.Unloading();

        private void SettingsContrainer_QueryCursor(object sender, QueryCursorEventArgs e) => TweakerWPF.Topmost = SettingsTweaker.TopMost;
    }
}
