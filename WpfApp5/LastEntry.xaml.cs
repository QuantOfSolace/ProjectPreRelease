using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
        private readonly string rootPass;


        public LastEntry()
        {
            InitializeComponent();

            for (int i = GlobalVar.selectionChangedInitTimes.Count - 2; i >= 0; i -= 2)
            {
                GlobalVar.selectionChangedInitTimes.RemoveAt(i);
            }
            for (int i = GlobalVar.selectionChangedInitData.Count - 2; i >= 0; i -= 2)
            {
                GlobalVar.selectionChangedInitData.RemoveAt(i);
            }
            for (int i = GlobalVar.selectionChangedInitFirstName.Count - 2; i >= 0; i -= 2)
            {
                GlobalVar.selectionChangedInitFirstName.RemoveAt(i);
            }
            for (int i = GlobalVar.selectionChangedInitSurrName.Count - 2; i >= 0; i -= 2)
            {
                GlobalVar.selectionChangedInitSurrName.RemoveAt(i);
            }
            for (int i = GlobalVar.selectionChangedInitLastName.Count - 2; i >= 0; i -= 2)
            {
                GlobalVar.selectionChangedInitLastName.RemoveAt(i);
            }
            for (int i = GlobalVar.selectionChangedInitRootPass.Count - 2; i >= 0; i -= 2)
            {
                GlobalVar.selectionChangedInitRootPass.RemoveAt(i);
            }

            time = GlobalVar.selectionChangedInitTimes.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitTimes) : "Список пуст.";
            id = GlobalVar.selectionChangedInitData.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitData) : "Список пуст";
            firstName = GlobalVar.selectionChangedInitFirstName.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitFirstName) : "Список пуст";
            surrName = GlobalVar.selectionChangedInitSurrName.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitSurrName) : "Список пуст";
            lastName = GlobalVar.selectionChangedInitLastName.Any() ? string.Join("\r\n", GlobalVar.selectionChangedInitLastName) : "Список пуст";
            rootPass = string.Join("\r\n", GlobalVar.selectionChangedInitRootPass);

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

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void ToTxtButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            // Если пользователь выбрал место для сохранения файла и нажал "ОК"
            if (saveFileDialog.ShowDialog() == true)
            {
                // Создаем файл Excel
                FileInfo newFile = new FileInfo(saveFileDialog.FileName);
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage package = new ExcelPackage(newFile);

                string randomString = RandomString(5);

                // Создаем лист в файле Excel
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(randomString);

                // Получаем текст из Label
                string labelText1 = Time.Content.ToString();
                string labelText2 = Id.Content.ToString();
                string labelText3 = FirstName.Content.ToString();
                string labelText4 = SurrName.Content.ToString();
                string labelText5 = LastName.Content.ToString();
                string labelText6 = RootPass.Content.ToString();

                // Разделяем текст по символу новой строки
                string[] lines1 = labelText1.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                string[] lines2 = labelText2.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                string[] lines3 = labelText3.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                string[] lines4 = labelText4.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                string[] lines5 = labelText5.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                string[] lines6 = labelText6.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                // Записываем каждую строку в отдельную ячейку в Excel-файле
                int row = 1;
                foreach (string line in lines1)
                {
                    worksheet.Cells[row, 1].Value = line;
                    row++;
                }
                row = 1;
                foreach (string line in lines2)
                {
                    worksheet.Cells[row, 2].Value = line;
                    row++;
                }
                row = 1;
                foreach (string line in lines3)
                {
                    worksheet.Cells[row, 3].Value = line;
                    row++;
                }
                row = 1;
                foreach (string line in lines4)
                {
                    worksheet.Cells[row, 4].Value = line;
                    row++;
                }
                row = 1;
                foreach (string line in lines5)
                {
                    worksheet.Cells[row, 5].Value = line;
                    row++;
                }
                row = 1;
                foreach (string line in lines6)
                {
                    worksheet.Cells[row, 6].Value = line;
                    row++;
                }

                // Сохраняем файл Excel
                package.Save();
                MessageBox.Show("Файл сохранен");
            }
            /* 
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
            */
        }
    }
}
