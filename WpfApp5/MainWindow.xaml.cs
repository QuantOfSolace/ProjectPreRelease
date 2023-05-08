using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            public MainWindow()
            {
                InitializeComponent();
                ClassChangePage.frame1 = Frame;
                ClassChangePage.frame1.Navigate(new Login());
                DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    label_time.Content = DateTime.Now.ToString("dd MMMM HH:mm");
                }, Dispatcher);

                if (GlobalVar.ThemeNegr)
                {
                    Checker.IsChecked = true;
                }

                //LoadCheckBoxState();
            }

            private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
            {
                WindowState = WindowState.Minimized;
            }

            private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
            {
                Close();
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show("Оператор скоро прибудет, ожидайте.");
            }

            private void ChangeTheme_Click(object sender, RoutedEventArgs e)
            {
                new BrushConverter();

                if (GlobalVar.FlagToAction)
                {
                    ColorAnimationUsingKeyFrames animation = new ColorAnimationUsingKeyFrames
                    {
                        Duration = new Duration(TimeSpan.FromSeconds(2.4))
                    };
                    animation.KeyFrames.Add(new LinearColorKeyFrame(Colors.Gray, TimeSpan.FromSeconds(0.8)));
                    animation.KeyFrames.Add(new LinearColorKeyFrame(Colors.DarkGray, TimeSpan.FromSeconds(0.6)));
                    animation.KeyFrames.Add(new LinearColorKeyFrame(((SolidColorBrush)Bebra.Background).Color, TimeSpan.FromSeconds(1.2)));

                    SolidColorBrush brush = new SolidColorBrush();
                    Frame.Background = brush;
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);

                    GlobalVar.ThemeNegr = true;
                }
                else
                {
                    ColorAnimationUsingKeyFrames animation = new ColorAnimationUsingKeyFrames
                    {
                        Duration = new Duration(TimeSpan.FromSeconds(1))
                    };
                    animation.KeyFrames.Add(new LinearColorKeyFrame(((SolidColorBrush)Frame.Background).Color, TimeSpan.FromSeconds(0)));
                    animation.KeyFrames.Add(new LinearColorKeyFrame(Colors.Transparent, TimeSpan.FromSeconds(2)));

                    SolidColorBrush brush = new SolidColorBrush();
                    Frame.Background = brush;
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);

                    GlobalVar.ThemeNegr = false;
                    //"#FF1F1F1F"
                }
                GlobalVar.FlagToAction = !GlobalVar.FlagToAction;

            }

            private void MainFrame_OnNavigating(object sender, NavigatingCancelEventArgs e)
            {
                ThicknessAnimation ta = new ThicknessAnimation
                {
                    Duration = TimeSpan.FromSeconds(0.5),
                    DecelerationRatio = 1,
                    To = new Thickness(0, 0, 0, 0)
                };
                if (e.NavigationMode == NavigationMode.New)
                {
                    ta.From = new Thickness(0, 200, 0, 100);
                }
                else if (e.NavigationMode == NavigationMode.Back)
                {
                    ta.From = new Thickness(0, 100, 0, 200);
                }
                     (e.Content as Page).BeginAnimation(MarginProperty, ta);
            }

            private void CheckBox_Checked(object sender, RoutedEventArgs e)
            {
                if (true)
                {
                    GlobalVar.ThemeNegr = true;
                }

                /*
                using (var store = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    try
                    {
                        lock (store)
                        {
                            using (var stream = new IsolatedStorageFileStream("CheckBoxState.dat", System.IO.FileMode.Create, store))
                            using (var writer = new StreamWriter(stream))
                            {
                                writer.WriteLine(Checker.IsChecked);
                            }

                            // Добавляем права доступа для текущего пользователя
                            var fileSecurity = File.GetAccessControl(store.GetType().GetField("m_RootDir", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(store).ToString() + "\\" + "CheckBoxState.dat");
                            var user = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                            var fileRule = new FileSystemAccessRule(user, FileSystemRights.ReadAndExecute | FileSystemRights.Write, AccessControlType.Allow);
                            fileSecurity.AddAccessRule(fileRule);
                            File.SetAccessControl(store.GetType().GetField("m_RootDir", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(store).ToString() + "\\" + "CheckBoxState.dat", fileSecurity);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                */
            }
            /*
              private void LoadCheckBoxState()
            {
                using (var store = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    lock (store)
                    {
                        if (store.FileExists("CheckBoxState.dat"))
                        {
                            using (var stream = new IsolatedStorageFileStream("CheckBoxState.dat", System.IO.FileMode.Open, store))
                            using (var reader = new StreamReader(stream))
                            {
                                bool isCheckBoxChecked = bool.Parse(reader.ReadLine());
                                Checker.IsChecked = isCheckBoxChecked;
                            }
                        }
                    }
                }
            */

        }
    }
