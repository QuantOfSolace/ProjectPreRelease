using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            myLabel.Content = "Уровень доступа: " + GetUserNameByLogin(GlobalVar.PanelLogin);

            dataBase = new TESTEntities();
            dataBase.users.Load();

            usersGrid.ItemsSource = dataBase.users.Local.ToBindingList();
        }

        private readonly TESTEntities dataBase = new TESTEntities();

        private string GetUserNameByLogin(string login)
        {
            // Получить объект пользователя из БД по login:
            admins user = dataBase.admins.FirstOrDefault(u => u.login == login);

            // Если у пользователя есть имя, то вернуть его:
            return user != null && !string.IsNullOrEmpty(user.root) ? user.root : string.Empty;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ClassChangePage.frame1.Navigate(new Login());
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dataBase.Dispose();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataBase.SaveChanges();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

