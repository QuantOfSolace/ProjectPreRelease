using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для TableUsers.xaml
    /// </summary>
    public partial class TableUsers : Page
    {
        public TableUsers()
        {
            InitializeComponent();

            myLabel.Content = "Уровень доступа: " + GlobalMethods.GetUserNameAdmin(GlobalVar.PanelLogin);

            dataBase = new TESTEntities();
            dataBase.users.Load();

            usersGrid.ItemsSource = dataBase.users.Local.ToBindingList();
        }

        private readonly TESTEntities dataBase = new TESTEntities();

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Вернутся на главное окно", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                GlobalVar.StatusAuth = false;
                dataBase.Dispose();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Сохранение изменений", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
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
        private void Button_Click(object sender,RoutedEventArgs e)
        {

        }
    }
}
