using System.Windows;
using System.Windows.Controls;
using ApplicationForBD.Pages;
using ApplicationForBD.ApplicationDataBases;
using System.Linq;
using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace ApplicationForBD
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        
        public LoginPage()
        {
            InitializeComponent();
            LoginPasswordInputFile();
        }
        public LoginPage(string login, string password)
        {
            InitializeComponent();
            passwordPassBox.Password = password;
            passwordTextBox.Text = password;
            loginText.Text = login;
        }

        private void LoginPasswordInputFile() //Получение логина, пароля из файла
        {
            try
            {
                StreamReader streamReader = new StreamReader(Environment.CurrentDirectory + "packedData"); 

                string line;

                List<string> temp = new List<string>();
                while ((line = streamReader.ReadLine()) != null)
                    temp.Add(line.Split('|')[1]);

                loginText.Text = temp[0];
                passwordPassBox.Password = temp[1];
                passwordTextBox.Text = temp[2];
                streamReader.Close();
                knowMe.IsChecked = true;
            }
            catch
            {
                knowMe.IsChecked = false;
            }
        }

        private void VisiblePassword_Click(object sender, RoutedEventArgs e)
        {
            switch (VisiblePassword.Visibility)
            {
                case Visibility.Hidden:
                    VisibilityPassword();
                    passwordPassBox.Password = passwordTextBox.Text;
                    passwordPassBox.Focus();
                    break;
                case Visibility.Visible:
                    VisibilityPassword(Visibility.Collapsed, Visibility.Visible);
                    passwordTextBox.Text = passwordPassBox.Password;
                    passwordTextBox.CaretIndex = passwordTextBox.Text.Length;
                    passwordTextBox.Focus();
                    break;
            }
        }
        private void VisibilityPassword(Visibility activity = Visibility.Visible, Visibility disabled = Visibility.Collapsed)
        {
            VisiblePassword.Visibility = activity;
            passwordPassBox.Visibility = activity;
            passwordTextBox.Visibility = disabled;
            VisibleClosePassword.Visibility = disabled;
        }
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
            =>SaveElementFrame.frame.Navigate(new RegistrationPage());

        private void IDKPassword_Click(object sender, RoutedEventArgs e)
            => SaveElementFrame.frame.Navigate(new IDKPasswordPage());
        

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string query = $"SELECT * FROM [dbo].[user] WHERE login_user = '{loginText.Text.ToLower()}' AND (password_user = '{passwordPassBox.Password}' OR password_user = '{passwordTextBox.Text}')";

            SaveElementFrame.QueryString = query;
            SqlCommand sql = new SqlCommand(query, AppConnect.GetConnection);
            adapter.SelectCommand = sql;
            adapter.Fill(table);
                
            if (table.Rows.Count == 1)
            {
                if (knowMe.IsChecked == true)
                {
                    StreamWriter writer = new StreamWriter(Environment.CurrentDirectory + "PackedData");
                    writer.WriteLine("login |" + loginText.Text.ToLower());
                    writer.WriteLine("password |" + passwordPassBox.Password);
                    writer.WriteLine("password2 |" + passwordTextBox.Text);
                    writer.WriteLine("time |" + DateTime.Now);
                    writer.Close();
                }
                else
                {
                    FileInfo file = new FileInfo(Environment.CurrentDirectory + "PackedData");
                    if (file.Exists)
                        file.Delete();
                }
                HubWindow hub = new HubWindow();
                hub.Show();
                SaveElementFrame.Window.Close();
            }
            else
                MessageBox.Show("Проверьте логин или пароль!", "Такого аккаунта не существует!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
