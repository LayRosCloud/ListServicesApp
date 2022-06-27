using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using ApplicationForBD.ApplicationDataBases;

using System.Windows.Media.Animation;


namespace ApplicationForBD.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        string genderCode = null;
        public string NameUserProfile { get; set; }
        public Profile()
        {
            InitializeComponent();

            passwordTextBox.Visibility = Visibility.Collapsed;

            InitializeDataTextBox();

            AnimationInitializeWidthHeightProperty(0, 450, TimeSpan.FromSeconds(3), 0.4, false, new RepeatBehavior(1), CircleBorderMini);
            AnimationInitializeWidthHeightProperty(500, 530, TimeSpan.FromSeconds(1.5), 0.5, true, RepeatBehavior.Forever, CircleBorderBig);
            AnimationInitializeWidthHeightProperty(500, 550, TimeSpan.FromSeconds(1), 0.5, true, RepeatBehavior.Forever, RectangleBorderBig);
            redactor.Visibility = Visibility.Collapsed;
        }
        private void InitializeDataTextBox()
        {
            if (SaveElementFrame.client != null)
            {
                rectorUser.Text = SaveElementFrame.client.FirstName.ToUpper()[0].ToString() + SaveElementFrame.client.LastName.ToUpper()[0].ToString();
                nameUser.Text = SaveElementFrame.client.FirstName + " " + SaveElementFrame.client.LastName;
                firstNameText.Text = SaveElementFrame.client.FirstName;
                lastNameText.Text = SaveElementFrame.client.LastName;
                patronymicText.Text = SaveElementFrame.client.Patronymic;
                birthdayDate.Text = SaveElementFrame.client.Birthday.ToString();
                dateRegistration.Text = SaveElementFrame.client.RegistrationDate.ToShortDateString().ToString();
                phone.Text = SaveElementFrame.client.Phone;
                switch (SaveElementFrame.client.GenderCode)
                {
                    case "м":
                        maleRadio.IsChecked = true;
                        break;
                    case "ж":
                        womanRadio.IsChecked = true;
                        break;
                }
                photoPath.Text = SaveElementFrame.client.PhotoPath;
            }
            else
            {
                rectorUser.Text = SaveElementFrame.NameUser.ToUpper()[0].ToString();
                nameUser.Text = SaveElementFrame.NameUser;

            }
            loginTextBox.Text = SaveElementFrame.NameUser;

            emailTextBox.Text = SaveElementFrame.EmailUser;
            NameUserProfile = SaveElementFrame.NameUser;
            switch (SaveElementFrame.RoleUser)
            {
                case 1:
                    roleTextBlock.Text = "Администратор";
                    break;
                case 2:
                    roleTextBlock.Text = "Пользователь";
                    break;
            }
            SqlCommand sql = new SqlCommand(SaveElementFrame.QueryString, AppConnect.GetConnection);
            AppConnect.OpenConnection();
            SqlDataReader reader = sql.ExecuteReader();

            while (reader.Read())
            {
                passwordPassBox.Password = reader.GetString(2);
                passwordTextBox.Text = reader.GetString(2);
                SaveElementFrame.PasswordUser = passwordPassBox.Password;
            }
            AppConnect.CloseConnection();
        }
        private void AnimationInitializeWidthHeightProperty(double from, double to, TimeSpan duration, double accelerationRatio, bool reverse, RepeatBehavior repeat, 
            Border @object)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation(from, to, duration);
            doubleAnimation.AccelerationRatio = accelerationRatio;
            doubleAnimation.AutoReverse = reverse;
            doubleAnimation.RepeatBehavior = repeat;
            @object.BeginAnimation(WidthProperty, doubleAnimation);
            @object.BeginAnimation(HeightProperty, doubleAnimation);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "openButton")
            {
                openButton.Visibility = Visibility.Collapsed;
                closeButton.Visibility = Visibility.Visible;

                passwordPassBox.Visibility = Visibility.Collapsed;
                passwordTextBox.Visibility = Visibility.Visible;

                passwordTextBox.Text = passwordPassBox.Password;
            }
            else if (btn.Name == "closeButton")
            {
                openButton.Visibility = Visibility.Visible;
                closeButton.Visibility = Visibility.Collapsed;

                passwordPassBox.Visibility = Visibility.Visible;
                passwordTextBox.Visibility = Visibility.Collapsed;

                passwordPassBox.Password = passwordTextBox.Text;
            }
        }

        private bool IsDataBaseQuery(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            SqlCommand sql = new SqlCommand(query, AppConnect.GetConnection);
            adapter.SelectCommand = sql;
            adapter.Fill(table);

            return !(table.Rows.Count > 0);
        }
        private void UpdateDataBaseUser(string query)
        {
            SqlCommand sql = new SqlCommand(query, AppConnect.GetConnection);

            AppConnect.OpenConnection();

            if (sql.ExecuteNonQuery() == 1)
            {
                SaveElementFrame.NameUser = loginTextBox.Text;
                SaveElementFrame.PasswordUser = passwordPassBox.Password;
                SaveElementFrame.EmailUser = emailTextBox.Text;
                SaveElementFrame.NameTextBlock.Text = loginTextBox.Text;
                SaveElementFrame.EmailTextBlock.Text = emailTextBox.Text;
                SaveElementFrame.ReductionTextBlock.Text = loginTextBox.Text.ToUpper()[0].ToString();
            }
            else
            {
                MessageBox.Show("Ошибка изменения данных =<");
            }
            AppConnect.CloseConnection();
            
        }
        private void loginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            redactor.Visibility = IsDataBaseQuery($"SELECT * FROM [dbo].[user] WHERE login_user = '{loginTextBox.Text.ToLower()}'") ?
               Visibility.Visible : Visibility.Collapsed;
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            redactor.Visibility = IsDataBaseQuery($"SELECT * FROM [dbo].[user] WHERE email_user = '{emailTextBox.Text.ToLower()}'") ?
               Visibility.Visible : Visibility.Collapsed; 
        }

        private void passwordPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Text = passwordPassBox.Password;
            redactor.Visibility = passwordPassBox.Password.Length >= 4
                && SaveElementFrame.PasswordUser != passwordPassBox.Password
                && SaveElementFrame.PasswordUser != passwordTextBox.Text?
                Visibility.Visible : Visibility.Collapsed;
        }
        private void passwordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordPassBox.Password = passwordTextBox.Text;
            redactor.Visibility = passwordTextBox.Text.Length >= 4
                && SaveElementFrame.PasswordUser != passwordPassBox.Password
                && SaveElementFrame.PasswordUser != passwordTextBox.Text?
                Visibility.Visible : Visibility.Collapsed;
        }
        private void redactor_Click(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.IsVisible)
                passwordPassBox.Password = passwordTextBox.Text;
            else
                passwordTextBox.Text = passwordPassBox.Password;

            UpdateDataBaseUser($"UPDATE [dbo].[user] SET login_user = '{loginTextBox.Text}', email_user = '{emailTextBox.Text}', password_user = '{passwordPassBox.Password}' WHERE login_user = '{SaveElementFrame.NameUser}'");



            redactor.Visibility = Visibility.Collapsed;
        }

        private void redactorEdit_Click(object sender, RoutedEventArgs e)
        {
            if (SaveElementFrame.client == null)
            {
                SqlCommand sql = new SqlCommand($@"INSERT INTO [dbo].[Client] VALUES('{firstNameText.Text}', '{lastNameText.Text}', '{patronymicText.Text}', {birthdayDate.Text}, 
                                                {DateTime.Now.ToShortDateString()}, '{emailTextBox.Text}', '{phone.Text}', '{genderCode.ToLower()}', '{photoPath.Text}')", AppConnect.GetConnection);
                AppConnect.OpenConnection();
                if (sql.ExecuteNonQuery() == 1)
                {
                    AppConnect.CloseConnection();
                    MessageBox.Show("успех");
                    SqlCommand initializeClient = new SqlCommand($"SELECT * FROM [dbo].[Client] WHERE Email = {emailTextBox.Text}");
                    AppConnect.OpenConnection();
                    SqlDataReader reader = initializeClient.ExecuteReader();
                    
                    reader.Read();
                    SaveElementFrame.client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                Convert.ToString(reader.GetValue(3)), reader.GetDateTime(4), reader.GetDateTime(5), reader.GetString(6), reader.GetString(7),
                reader.GetString(8), Convert.ToString(reader.GetValue(9)));
                    AppConnect.CloseConnection();

                }
                else
                {
                    MessageBox.Show("провал");
                    AppConnect.CloseConnection();

                }
            }
            else
            {
                SqlCommand sql = new SqlCommand($@"
                                                UPDATE [dbo].[Client] 
                                                SET FirstName = '{firstNameText.Text}', 
                                                LastName = '{lastNameText.Text}', 
                                                Patronymic = '{patronymicText.Text}',
                                                Birthday = '{birthdayDate.Text.Replace('.', '-')}',
                                                Email = '{emailTextBox.Text}',
                                                Phone = '{phone.Text}',
                                                GenderCode = '{genderCode}',
                                                PhotoPath = '{photoPath.Text}'
                                                WHERE ID = {SaveElementFrame.client.ID}", AppConnect.GetConnection);
                AppConnect.OpenConnection();
                if (sql.ExecuteNonQuery() == 1)
                {
                    AppConnect.CloseConnection();
                    MessageBox.Show("успех");
                    SqlCommand initializeClient = new SqlCommand($"SELECT * FROM [dbo].[Client] WHERE Email = '{emailTextBox.Text}'", AppConnect.GetConnection);
                    AppConnect.OpenConnection();
                    SqlDataReader reader = initializeClient.ExecuteReader();

                    reader.Read();
                    SaveElementFrame.client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                Convert.ToString(reader.GetValue(3)), reader.GetDateTime(4), reader.GetDateTime(5), reader.GetString(6), reader.GetString(7),
                reader.GetString(8), Convert.ToString(reader.GetValue(9)));
                    AppConnect.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Провал");
                    AppConnect.CloseConnection();
                }
            }
        }

        private void firstNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            redactorEdit.Visibility = firstNameText.Text != SaveElementFrame.client.FirstName? Visibility.Visible: Visibility.Collapsed;
        }

        private void lastNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            redactorEdit.Visibility = lastNameText.Text != SaveElementFrame.client.LastName ? Visibility.Visible : Visibility.Collapsed;
        }

        private void patronymicText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (patronymicText.Text != SaveElementFrame.client.Patronymic)
                redactorEdit.Visibility = Visibility.Visible;
        }

        private void birthdayDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
                redactorEdit.Visibility = Convert.ToDateTime(birthdayDate.Text) != SaveElementFrame.client.Birthday ? Visibility.Visible : Visibility.Collapsed;
        }

        private void phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (phone.Text != SaveElementFrame.client.Phone)
                redactorEdit.Visibility = Visibility.Visible;
        }

        private void maleRadio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            switch (radio.Content.ToString().ToLower())
            {
                case "мужской":
                    genderCode = "м";
                    break;
                case "женский":
                    genderCode = "ж";
                    break;
            }
          
            redactorEdit.Visibility = genderCode != SaveElementFrame.client.GenderCode ? Visibility.Visible : Visibility.Collapsed;
        }

        private void photoPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (photoPath.Text != SaveElementFrame.client.PhotoPath)
                redactorEdit.Visibility = Visibility.Visible;
        }
    }
}
