
using System.Windows;
using System.Windows.Controls;
using ApplicationForBD.Pages;


namespace ApplicationForBD
{
    /// <summary>
    /// Логика взаимодействия для IDKPasswordPage.xaml
    /// </summary>
    public partial class IDKPasswordPage : Page
    {
        public IDKPasswordPage()
        {
            InitializeComponent();
        }

        private void AutorizationBTN_Click(object sender, RoutedEventArgs e)
        {
            SaveElementFrame.frame.Navigate(new LoginPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            MailAddress fromAdress = new MailAddress("schoolearn@bk.ru", "школа Леарн");
            MailAddress toAdress = new MailAddress(emailTextBox.Text);
            MailMessage mail = new MailMessage(fromAdress, toAdress);
            mail.Body = $"Ваш пароль: {passwordPassBox.Password}";
            mail.Subject = "Забыли пароль";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.mail.ru";
            smtp.Port = 465;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromAdress.Address, "1xT5dKeLQvkUZAFPkJcK");

            smtp.Send(mail);*/

        }
    }
}
