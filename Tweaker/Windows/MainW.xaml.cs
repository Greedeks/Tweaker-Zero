using System;
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
        private readonly StartScanner _startScanner = new StartScanner();
        public MainWindow()
        {
            _startScanner.BeforeLoadingCheck();

            InitializeComponent();

            #region First Page
            Button_Confidentiality.Style = (Style)Application.Current.Resources["ButtonNav_S"];
            Grid.SetColumn(ActivePage, 0);
            ActivePageAnim();
            MainContainer.Navigate(new Pages.ConfidentialityPage());
            #endregion
        }

        #region Movements/Closure/Collapsing the Form
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

        #region Animations
        private void ActivePageAnim()
        {
            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();

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
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100))
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
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(80))
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

                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150))
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

        private void DefaultStyleBtnN()
        {
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

        #region Buttons
        private void Button_Navigations_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed & Unclickable.Width == 0)
            {
                Parallel.Invoke(() =>
                {
                    Button btn = (Button)sender;
                    switch (btn.Name)
                    {
                        case "Button_Confidentiality":
                            if (MainContainer.Content.ToString() != "Tweaker.Pages.ConfidentialityPage")
                            {
                                DefaultStyleBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 0);

                                ActivePageAnim();
                                CleaningPages();
                                MainContainer.Navigate(new Pages.ConfidentialityPage());
                            }
                            break;
                        case "Button_Interface":
                            if (MainContainer.Content.ToString() != "Tweaker.Pages.InterfacePage")
                            {
                                DefaultStyleBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 1);

                                ActivePageAnim();
                                CleaningPages();
                                MainContainer.Navigate(new Pages.InterfacePage());
                            }
                            break;
                        case "Button_Application":
                            if (MainContainer.Content.ToString() != "Tweaker.Pages.ApplicationsPage")
                            {
                                DefaultStyleBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 2);

                                ActivePageAnim();
                                CleaningPages();
                                MainContainer.Navigate(new Pages.ApplicationsPage());
                            }
                            break;
                        case "Button_Services":
                            if (MainContainer.Content.ToString() != "Tweaker.Pages.ServicesPage")
                            {
                                DefaultStyleBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 3);

                                ActivePageAnim();
                                CleaningPages();
                                MainContainer.Navigate(new Pages.ServicesPage());
                            }
                            break;
                        case "Button_System":
                            if (MainContainer.Content.ToString() != "Tweaker.Pages.SystemPage")
                            {
                                DefaultStyleBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 4);

                                ActivePageAnim();
                                CleaningPages();
                                MainContainer.Navigate(new Pages.SystemPage());
                            }
                            break;
                        case "Button_SystemInfo":
                            if (MainContainer.Content.ToString() != "Tweaker.Pages.SystemInfromation")
                            {
                                DefaultStyleBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 5);

                                ActivePageAnim();
                                CleaningPages();
                                MainContainer.Navigate(new Pages.SystemInfromation());
                            }
                            break;
                        case "Button_More":
                            if (MainContainer.Content.ToString() != "Tweaker.Pages.MorePage")
                            {
                                DefaultStyleBtnN();
                                btn.Style = (Style)Application.Current.Resources["ButtonNav_S"];
                                Grid.SetColumn(ActivePage, 6);

                                ActivePageAnim();
                                CleaningPages();
                                MainContainer.Navigate(new Pages.MorePage());
                            }
                            break;
                    }
                });
            }

        }

        private void Button_Settings_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Unclickable.Width==0)
                {
                    Parallel.Invoke(() =>
                    {
                        SettingsPanelAnim(true);
                        SettingsContrainer.Navigate(new Pages.SettingsPage());
                    });
                }
                else
                {
                    Parallel.Invoke(() =>
                    {
                        SettingsPanelAnim(false);
                        while (SettingsContrainer.NavigationService.RemoveBackEntry() != null) ;
                        SettingsContrainer.Content = null;
                    });
                }
            }
        }
        #endregion

        private async void TweakerWPF_Loaded(object sender, RoutedEventArgs e)
        {
            _startScanner.ScantheSystem();
            TweakerWPF.Topmost = SettingsTweaker.TopMost;

            #region Loading animation
            this.Opacity = 0;
            await Task.Delay(200);
            this.Opacity = 1;

            Rect _primaryMonitorArea = SystemParameters.WorkArea;

            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame _fromFrame = new EasingDoubleKeyFrame(_primaryMonitorArea.Bottom)
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))
            };

            EasingDoubleKeyFrame _toFrame = new EasingDoubleKeyFrame((_primaryMonitorArea.Bottom / 2) - (this.Height / 2))
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(250))
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
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(250))
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
