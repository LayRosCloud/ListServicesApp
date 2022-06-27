using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using ApplicationForBD.Pages;
using ApplicationForBD.ApplicationDataBases;
using System.Net;
using System.Net.Mail;

namespace ApplicationForBD
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
            => InitializeComponent();
        

        private void AutorizationButton_Click(object sender, RoutedEventArgs e)
        {
            SaveElementFrame.frame.Navigate(new LoginPage());
        }

        private void VisiblePassword_Click(object sender, RoutedEventArgs e)
        {
            if(passwordPassBox.IsVisible)
            {
                passwordTextBox.Text = passwordPassBox.Password;
                passwordTextBox2.Text = passwordPassBox2.Password;
                ChoiceVisibleElement(Visibility.Hidden, Visibility.Visible);
            }
            else
            {
                passwordPassBox.Password = passwordTextBox.Text;
                passwordPassBox2.Password = passwordTextBox2.Text;
                ChoiceVisibleElement(Visibility.Visible, Visibility.Hidden);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(errorLogin.IsVisible || errorEmail.IsVisible || errorLoginPovtor.IsVisible || errorPass.IsVisible || errorPassPov.IsVisible)
            {

            }
            else
            {
                try
                {
                    string query = $"INSERT INTO [dbo].[user](login_user, password_user, email_user, role_id) VALUES('{loginTextBox.Text.ToLower()}','{passwordPassBox.Password}','{emailTextBox.Text.ToLower()}',2)";
                    SqlCommand cmd = new SqlCommand(query, AppConnect.GetConnection);

                    AppConnect.OpenConnection();

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        /*
                        MailAddress fromAdress = new MailAddress("schoolearn@bk.ru", "школа Леарн");
                        MailAddress toAdress = new MailAddress(emailTextBox.Text);
                        MailMessage mail = new MailMessage(fromAdress, toAdress);
                        mail.Body = $"Здравствуй, {loginTextBox.Text}! Ты успешно зарегестрировался в нашем сервисе Школа Леарн, твои данные: \nЛогин: {loginTextBox.Text}\nПочта: {emailTextBox.Text}\nПароль: {passwordPassBox.Password}";
                        mail.Subject = "Успешная регистрация!";

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.mail.ru";
                        smtp.Port = 465;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(fromAdress.Address, "1xT5dKeLQvkUZAFPkJcK");

                        smtp.Send(mail);*/
                        SaveElementFrame.frame.Navigate(new LoginPage(loginTextBox.Text, passwordPassBox.Password));
                    }
                    else
                        MessageBox.Show("Не удалось зарегестрировать аккаунт =<");
                    
                    AppConnect.CloseConnection();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }
        private async Task SendEmailAsync() //нерабочая отправка
        {
            MailAddress fromAdress = new MailAddress("schoolearn@bk.ru", "школа Леарн");
            MailAddress toAdress = new MailAddress(emailTextBox.Text);
            MailMessage mail = new MailMessage(fromAdress, toAdress);
            mail.Body = $"Здравствуй, {loginTextBox.Text}! Ты успешно зарегистрировался в нашем сервисе Школа Леарн, твои данные: \nЛогин: {loginTextBox.Text}\nПочта: {emailTextBox.Text}\nПароль: {passwordPassBox.Password}";
            mail.Subject = "Успешная регистрация!";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 465;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromAdress.Address, "sefikboom");

            await smtp.SendMailAsync(mail);
        }
        
        //Проверка данных
        private void loginTextBox_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            DataTable table = new DataTable();

            GetTableFromQuery($"SELECT login_user FROM [dbo].[user] WHERE login_user = '{loginTextBox.Text}'", ref table);
            if (table.Rows.Count >= 1)
            {
                ThinknessTextBlock(loginTextBlock, new Thickness(0, 0, 0, 5));
                ChoiceElementText(errorLogin, "*Логин уже занят");
            }
            else if (loginTextBox.Text.Length < 4)
            {
                ThinknessTextBlock(loginTextBlock, new Thickness(0, 0, 0, 5));
                ChoiceElementText(errorLogin, "*Логин слишком короткий");
            }
            else if (errorLogin.Text.Length >= 30)
            {
                ThinknessTextBlock(loginTextBlock, new Thickness(0, 0, 0, 5));
                ChoiceElementText(errorLogin, "*Логин слишком длинный");
            }
            else
            {
                ThinknessTextBlock(loginTextBlock, new Thickness(0, 0, 0, 0));
                errorLogin.Visibility = Visibility.Collapsed;
            }
        }
        
        private void emailTextBox_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            DataTable table = new DataTable();

            GetTableFromQuery($"SELECT email_user FROM [dbo].[user] WHERE email_user = '{emailTextBox.Text}'", ref table);

            if (!emailTextBox.Text.Contains('@'))
            {
                ThinknessTextBlock(emailTextBlock, new Thickness(0, 30, 0, 0));
                ChoiceElementText(errorEmail, "*Отсутствует знак @");
            }
            else if (table.Rows.Count >= 1)
            {
                ThinknessTextBlock(emailTextBlock, new Thickness(0, 30, 0, 0));
                ChoiceElementText(errorEmail, "*Данная почта уже занята");
            }
            else
            {
                ThinknessTextBlock(emailTextBlock, new Thickness(0, 20, 0, 0));
                errorEmail.Visibility = Visibility.Collapsed;
            }

        }

        private void emailPovTextBox_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (emailPovTextBox.Text != emailTextBox.Text)
            {
                ThinknessTextBlock(emailPovTextBlock, new Thickness(0, 20, 0, 0));
                ChoiceElementText(errorLoginPovtor, "*Почты не совпадают");
            }
            else
            {
                ThinknessTextBlock(emailTextBlock, new Thickness(0, 10, 0, 0));
                errorLoginPovtor.Visibility = Visibility.Collapsed;
            }
        }

        private void passwordPassBox_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (passwordPassBox.IsVisible)
                passwordTextBox.Text = passwordPassBox.Password;
            else
                passwordPassBox.Password = passwordTextBox.Text;
            


            if (passwordPassBox.Password.Length <= 4)
            {
                ThinknessTextBlock(passwordTextBlock, new Thickness(0, 20, 0, 0));
                ChoiceElementText(errorPass, "*Пароль слишком короткий!");
            }
            else
            {
                ThinknessTextBlock(passwordTextBlock, new Thickness(0, 10, 0, 0));
                errorPass.Visibility = Visibility.Collapsed;
            }

        }

        private void passwordPassBox2_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            if (passwordPassBox2.IsVisible)
                passwordTextBox2.Text = passwordPassBox2.Password;
            else
                passwordPassBox2.Password = passwordTextBox2.Text;

            if (passwordPassBox2.Password != passwordPassBox.Password)
            {
                ThinknessTextBlock(passwordPovTextBlocks, new Thickness(0, 20, 0, 0));
                ChoiceElementText(errorPassPov, "*Пароли не совпадают!");
            }

            else
            {
                ThinknessTextBlock(passwordPovTextBlocks, new Thickness(0, 10, 0, 0));
                errorPassPov.Visibility = Visibility.Collapsed;

            }

        }
        private void GetTableFromQuery(string query, ref DataTable table)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            table = new DataTable();

            SqlCommand cmd = new SqlCommand(query, AppConnect.GetConnection);
            adapter.SelectCommand = cmd;
            adapter.Fill(table);
        }

        private void ThinknessTextBlock(TextBlock text, Thickness thickness)
            => text.Margin = thickness;
        private static void ChoiceElementText(TextBlock textBlock, string text)
        {
            textBlock.Text = text;
            textBlock.Visibility = Visibility.Visible;
        }
        private void ChoiceVisibleElement(Visibility pass, Visibility text)
        {
            passwordPassBox.Visibility = pass;
            passwordPassBox2.Visibility = pass;
            VisiblePassword.Visibility = pass;

            passwordTextBox.Visibility = text;
            passwordTextBox2.Visibility = text;
            VisibleClosePassword.Visibility = text;
    }
    }
}
