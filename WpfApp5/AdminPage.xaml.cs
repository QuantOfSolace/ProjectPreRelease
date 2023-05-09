using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();

            myLabel.Content = "Уровень доступа: " + GlobalMethods.GetUserNameAdmin(GlobalVar.PanelLogin);
            Hello.Text = "Здравствуйте, " + GlobalMethods.GetName(GlobalVar.PanelLogin);

            BrushConverter converter = new BrushConverter();

            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (GlobalVar.ThemeNegr == true)
                {
                    Hello.Foreground = (Brush)converter.ConvertFromString("White");
                    Hello.Foreground = (Brush)converter.ConvertFromString("White");
                }
                if (GlobalVar.ThemeNegr == false)
                {
                    Hello.Foreground = (Brush)converter.ConvertFromString("Black");
                    Hello.Foreground = (Brush)converter.ConvertFromString("Black");
                }
            }
           , Dispatcher);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Выход из учётной записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                GlobalVar.StatusAuth = false;
                ClassChangePage.frame1.Navigate(new Login());
            }
        }
        private void AdminPage_Loaded(object sender2, RoutedEventArgs e)
        {
            // Находим элемент Border по его имени
            Border myBorder = (Border)FindName("myBorder");

            // Создаем анимацию для изменения прозрачности
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0, // Начальное значение прозрачности
                To = 1, // Конечное значение прозрачности
                Duration = new Duration(TimeSpan.FromSeconds(1)) // Длительность анимации
            };

            // Создаем анимацию для изменения положения
            ThicknessAnimation positionAnimation = new ThicknessAnimation
            {
                From = new Thickness(50, 100, 0, -100), // Начальное значение положения (скрытое)
                To = new Thickness(50, 0, 0, 0), // Конечное значение положения (отображение на экране)
                Duration = new Duration(TimeSpan.FromSeconds(1)) // Длительность анимации
            };

            // Запускаем анимации
            myBorder.BeginAnimation(OpacityProperty, opacityAnimation);
            myBorder.BeginAnimation(MarginProperty, positionAnimation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassChangePage.frame1.Navigate(new Registration());
        }

        private void Button_Click_User(object sender, RoutedEventArgs e)
        {
            ClassChangePage.frame1.Navigate(new TableUsers());
        }

        private void Button_Click_Oper(object sender, RoutedEventArgs e)
        {
            ClassChangePage.frame1.Navigate(new TableOpers());
        }
    }
}

