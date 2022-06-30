using ApplicationForBD.ApplicationDataBases;
using ApplicationForBD.Pages;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ApplicationForBD
{
    public partial class HubWindow : Window
    {
        bool isWiden;
        public HubWindow()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            SqlDataReader reader = AppConnect.GetOpenReader(SaveElementFrame.QueryString);
            reader.Read();
            InitializeSaveElement(reader);

            AppConnect.CloseConnection();
            reader = AppConnect.GetOpenReader($"Select * From [dbo].[Client] WHERE Email = '{SaveElementFrame.EmailUser}'");
            if (reader.HasRows)
            {
                reader.Read();
                SaveElementFrame.client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                Convert.ToString(reader.GetValue(3)), reader.GetDateTime(4), reader.GetDateTime(5), reader.GetString(6), reader.GetString(7),
                reader.GetString(8), Convert.ToString(reader.GetValue(9)));

                reductionText.Text = SaveElementFrame.client.FirstName.ToUpper()[0].ToString() + SaveElementFrame.client.LastName.ToUpper()[0].ToString();
                nameUserText.Text = SaveElementFrame.client.FirstName + " " + SaveElementFrame.client.LastName;
                emailUserText.Text = SaveElementFrame.EmailUser.ToString();
            }
            else
            {
                reductionText.Text = SaveElementFrame.NameUser.ToUpper()[0].ToString();
                nameUserText.Text = SaveElementFrame.NameUser.ToString();
                emailUserText.Text = SaveElementFrame.EmailUser.ToString();
            }
            AppConnect.CloseConnection();

            if (SaveElementFrame.client != null)
            {
                reader = AppConnect.GetOpenReader($"Select * From [dbo].[ClientService] WHERE ClientID = {SaveElementFrame.client.ID}");
                if (reader.HasRows)
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        if (reader.GetDateTime(3) >= DateTime.Now)
                            count++;
                    }
                    CountNotificies.Text = count.ToString();
                }
                AppConnect.CloseConnection();
            }
            notificationCountBorder.Visibility = Convert.ToInt32(CountNotificies.Text) == 0 ? Visibility.Collapsed: Visibility.Visible;
            
            HubFrame.Navigate(new ServicesPage());
            SaveElementFrame.NameTextBlock = nameUserText;
            SaveElementFrame.ReductionTextBlock = reductionText;
            SaveElementFrame.EmailTextBlock = emailUserText;
            switch (SaveElementFrame.RoleUser)
            {
                case 1:
                    lvlUserText.Text = "Администратор";
                    AdminButton.Visibility = Visibility.Visible;
                    break;
                case 2:
                    lvlUserText.Text = "Пользователь";
                    AdminButton.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void InitializeSaveElement(IDataRecord reader)
        {
            SaveElementFrame.NameUser = reader.GetString(1);
            SaveElementFrame.EmailUser = reader.GetString(3);
            SaveElementFrame.RoleUser = reader.GetInt32(4);

            SaveElementFrame.borderCount = notificationCountBorder;
            SaveElementFrame.countNot = CountNotificies;

            SaveElementFrame.frameHub = HubFrame;
        }
        private void HideButton_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState.Minimized;


        private void UpScaleButton_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState == WindowState.Normal? WindowState.Maximized: WindowState.Normal;


        private void ExitButton_Click(object sender, RoutedEventArgs e)
            => Close();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveElementFrame.client = null;
            SaveElementFrame.listService = new System.Collections.Generic.List<Service>();
            new MainWindow().Show();
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
            => DragMove();


        private void ProfileButton_Click(object sender, RoutedEventArgs e)
            => HubFrame.Navigate(new Profile());


        private void listButtonService_Click(object sender, RoutedEventArgs e)
            => HubFrame.Navigate(new ServicesPage());



        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            => isWiden = true;

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isWiden = false;
            ((Rectangle)sender).ReleaseMouseCapture();
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (isWiden && e.LeftButton == MouseButtonState.Pressed)
            {
                ((Rectangle)sender).CaptureMouse();
                double newWidth = e.GetPosition(this).X;
                if (newWidth > 0)
                    this.Width = newWidth;
            }
        }

        private void Rectangle_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (isWiden && e.LeftButton == MouseButtonState.Pressed)
            {
                ((Rectangle)sender).CaptureMouse();
                double newHeight = e.GetPosition(this).Y;
                if (newHeight > 0)
                    this.Height = newHeight;
            }
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
            => HubFrame.Navigate(new AdminPanel());

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HubFrame.Navigate(new Notifications());
        }
    }
}
