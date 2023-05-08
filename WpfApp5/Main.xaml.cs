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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {   
            InitializeComponent();   

            myLabel.Content = "Уровень доступа: " + GlobalMethods.GetUserNameUser(GlobalVar.PanelLogin);

            comboBox.SelectionChanged += new SelectionChangedEventHandler(ComboBox_SelectionChanged);

            using (TESTEntities DataBase = new TESTEntities())
            {
                List<string> items = DataBase.mainUsers.Select(x => x.Id_Name).ToList();
                foreach (string item in items)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem
                    {
                        Content = item
                    };
                    comboBox.Items.Add(comboBoxItem);
                };
            }
        }

        private readonly TESTEntities dataBase = new TESTEntities();

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BrushConverter converter = new BrushConverter();

            // получаем выбранное значение ComboBox
            string str = comboBox.SelectedItem.ToString();
            string selectedValue = str.Substring(str.Length - 3);

            // создаем запросы, которые выбирают значение исходя из выбранного Id
            string result1 = dataBase.mainUsers.Where(x => x.Id_Name == selectedValue).Select(x => x.FirstName).FirstOrDefault();
            string result2 = dataBase.mainUsers.Where(x => x.Id_Name == selectedValue).Select(x => x.SurrName).FirstOrDefault();
            string result3 = dataBase.mainUsers.Where(x => x.Id_Name == selectedValue).Select(x => x.LastName).FirstOrDefault();
            string result4 = dataBase.mainUsers.Where(x => x.Id_Name == selectedValue).Select(x => x.PhoneNumber).FirstOrDefault();
            string result5 = dataBase.mainUsers.Where(x => x.Id_Name == selectedValue).Select(x => x.Id_Name).FirstOrDefault();
            string result6 = dataBase.mainUsers.Where(x => x.Id_Name == selectedValue).Select(x => x.rootPass).FirstOrDefault();

            // обновляем значение Label
            firstName.Content = "Имя: " + result1;
            surrName.Content = "Фамилия: " + result2;
            lastName.Content = "Отчество: " + result3;
            phoneNubmer.Content = "Номер телефона: " + result4;
            idName.Content = "ID: " + result5;

            GlobalVar.selectionChangedInitTimes.Add(DateTime.Now);
            GlobalVar.selectionChangedInitData.Add(result5);
            GlobalVar.selectionChangedInitFirstName.Add(result1);
            GlobalVar.selectionChangedInitSurrName.Add(result2);
            GlobalVar.selectionChangedInitLastName.Add(result3);
            GlobalVar.selectionChangedInitRootPass.Add(result6);

            if (result6 == "true")
            {
                StatusSignal.Background = (Brush)converter.ConvertFromString("Green");
                StatusSignalText.Text = "Проход разрешён";
            }
            else
            {
                StatusSignal.Background = (Brush)converter.ConvertFromString("Red");
                StatusSignalText.Text = "Проход запрещён";
            }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassChangePage.frame1.Navigate(new LastEntry());
        }
    }
}
