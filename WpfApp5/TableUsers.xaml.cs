﻿using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            string header;

            if (GlobalVar.StatusSave1)
            {
                header = "Изменения сохранены. Вы уверены?";
            }
            else header = "Изменения не сохранены. Вы уверены?";

            MessageBoxResult result = MessageBox.Show(header, "Вернутся на главное окно", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                GlobalVar.StatusAuth = false;
                ClassChangePage.frame1.Navigate(new AdminPage());
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
                    GlobalVar.StatusSave1 = true;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new TESTEntities())
            {
                var users = db.users.ToList();

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                var excelPackage = new ExcelPackage();
                var worksheet = excelPackage.Workbook.Worksheets.Add("Users");

                // Добавляем заголовки столбцов
                worksheet.Cells[1, 1].Value = "Login";
                worksheet.Cells[1, 2].Value = "Password";
                worksheet.Cells[1, 3].Value = "Root";

                // Заполняем таблицу данными
                for (int i = 0; i < users.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = users[i].login;
                    worksheet.Cells[i + 2, 2].Value = users[i].password;
                    worksheet.Cells[i + 2, 3].Value = users[i].root;
                }

                // Сохраняем файл
                var dialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    DefaultExt = ".xlsx",
                    FileName = "Users"
                };

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    // Сохраняем файл
                    var file = new FileInfo(dialog.FileName);
                    excelPackage.SaveAs(file);
                }
            }
        }
    }
}
