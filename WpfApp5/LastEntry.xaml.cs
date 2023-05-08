using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для LastEntry.xaml
    /// </summary>
    public partial class LastEntry : Page
    {
        private readonly string time;
        private readonly string id;
        private readonly string firstName;
        private readonly string surrName;
        private readonly string lastName;


        public LastEntry()
        {
            InitializeComponent();

            time = GlobalVar.selectionChangedInitTimes.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitTimes) : "Список пуст.";
            id = GlobalVar.selectionChangedInitData.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitData) : "Список пуст";
            firstName = GlobalVar.selectionChangedInitFirstName.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitFirstName) : "Список пуст";
            surrName = GlobalVar.selectionChangedInitSurrName.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitSurrName) : "Список пуст";
            lastName = GlobalVar.selectionChangedInitLastName.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitLastName) : "Список пуст";
            string rootPass = string.Join("\r\n", GlobalVar.selectionChangedInitRootPass);

            // обновляем значение Content у label
            Time.Content = $"Время входа\r\n{time}";
            Id.Content = $"ID\r\n{id}";
            FirstName.Content = $"Имена\r\n{firstName}";
            SurrName.Content = $"Фамилии\r\n{surrName}";
            LastName.Content = $"Отчества\r\n{lastName}";
            RootPass.Content = $"Допуск\r\n{rootPass}";

            BrushConverter converter = new BrushConverter();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 1), DispatcherPriority.Normal, delegate
            {

                if (GlobalVar.ThemeNegr == true)
                {

                    Time.Foreground = (Brush)converter.ConvertFromString("White");
                    Id.Foreground = (Brush)converter.ConvertFromString("White");
                    FirstName.Foreground = (Brush)converter.ConvertFromString("White");
                    SurrName.Foreground = (Brush)converter.ConvertFromString("White");
                    LastName.Foreground = (Brush)converter.ConvertFromString("White");
                    RootPass.Foreground = (Brush)converter.ConvertFromString("White");
                }

                if (GlobalVar.ThemeNegr == false)
                {

                    Time.Foreground = (Brush)converter.ConvertFromString("Black");
                    Id.Foreground = (Brush)converter.ConvertFromString("Black");
                    FirstName.Foreground = (Brush)converter.ConvertFromString("Black");
                    SurrName.Foreground = (Brush)converter.ConvertFromString("Black");
                    LastName.Foreground = (Brush)converter.ConvertFromString("Black");
                    RootPass.Foreground = (Brush)converter.ConvertFromString("Black");
                }
            }, Dispatcher);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Main());
        }

        private void ToTxtButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    FileName = "Данные о последних входах", // Default file name
                    DefaultExt = ".txt", // Default file extension
                    Filter = "Text documents (.txt)|*.txt" // Filter files by extension
                };

                // Show save file dialog box
                bool? result = dialog.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    File.WriteAllText(dialog.FileName, $"\r\n {time}" + $"\r\n {id}", Encoding.Default);
                    MessageBox.Show("Данные выгруженны успешно");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
