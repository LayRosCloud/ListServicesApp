using ApplicationForBD.ApplicationDataBases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ApplicationForBD.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCardPage.xaml
    /// </summary>
    public partial class ShoppingCardPage : Page
    {
        public decimal CostSum { get; set; } = 0;
        public decimal DiscountPriceSum { get; set; } = 0;
        public int CountList { get; set; } = SaveElementFrame.listService.Count;
        public ShoppingCardPage()
        {
            for (int i = 0; i < SaveElementFrame.listService.Count; i++)
            {
                CostSum += SaveElementFrame.listService[i].Cost;
                DiscountPriceSum += SaveElementFrame.listService[i].DiscountPrice;
            }
            InitializeComponent();
            nothingService.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            for (int i = 0; i < SaveElementFrame.listService.Count; i++)
            {
                listServicesBuy.Items.Add(SaveElementFrame.listService[i]);
            }
            gridDownMenu.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            backButton.Text = "<";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveElementFrame.frameHub.Navigate(new ServicesPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TextBlock id = (((Button)sender).Parent as Grid).Children[0] as TextBlock;
            Service serv = (from s in SaveElementFrame.listService where s.Id == Convert.ToInt32(id.Text) select s).FirstOrDefault();
            SaveElementFrame.listService.Remove(serv);
            listServicesBuy.Items.Clear();

            CostSum = 0;
            DiscountPriceSum = 0;
            for (int i = 0; i < SaveElementFrame.listService.Count; i++)
            {
                CostSum += SaveElementFrame.listService[i].Cost;
                DiscountPriceSum += SaveElementFrame.listService[i].DiscountPrice;
                listServicesBuy.Items.Add(SaveElementFrame.listService[i]);

            }
            
            costText.Text = $"{CostSum:0.00}";
            discountText.Text = $"{DiscountPriceSum:0.00} р.";
            countText.Text = $"Количество товара: {SaveElementFrame.listService.Count:0}";
            gridDownMenu.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            nothingService.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

        }
        private DateTime GetMaxDate(Service serv)
        {
            SqlCommand sql = new SqlCommand($"SELECT ServiceID, StartTime FROM [dbo].[ClientService] WHERE ServiceID = {serv.Id}", AppConnect.GetConnection);
            AppConnect.OpenConnection();
            SqlDataReader reader = sql.ExecuteReader();
            DateTime date = DateTime.MinValue;
            while (reader.Read())
            {
                if (date <= reader.GetDateTime(1))
                    date = reader.GetDateTime(1);
            }
            AppConnect.CloseConnection();

            if (date < DateTime.Now)
                date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 8, 0, 0);
            if (date.TimeOfDay + new TimeSpan(0, Convert.ToInt32(serv.DurationInSeconds), 0) > new TimeSpan(19, 0, 0))
                date = new DateTime(date.Year, date.Month, date.Day + 1, 8, 0, 0);
            SqlCommand sqlClient = new SqlCommand($"SELECT StartTime, s.DurationInSeconds FROM [dbo].[ClientService] cs, [dbo].[Service] s WHERE ClientID = {SaveElementFrame.client.ID} AND cs.ServiceID = s.ID", AppConnect.GetConnection);
            AppConnect.OpenConnection();
            reader = sqlClient.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (date <= reader.GetDateTime(0).AddMinutes(reader.GetInt32(1)/60.0))
                    {
                        date = date.AddMinutes(reader.GetInt32(1) / 60.0);
                    }
                }
            }
            AppConnect.CloseConnection();
            return date;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //покупка
        {
            if (SaveElementFrame.client != null)
            {
                SqlCommand sql;
                for (int i = 0; i < SaveElementFrame.listService.Count; i++)
                {
                    sql = new SqlCommand($@"INSERT INTO [dbo].[ClientService](ClientID, ServiceID, StartTime) VALUES({SaveElementFrame.client.ID}, 
                    {SaveElementFrame.listService[i].Id}, 
                    '{GetMaxDate(SaveElementFrame.listService[i]).ToString().Replace('.', '-')}')", AppConnect.GetConnection);
                    AppConnect.OpenConnection();
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        AppConnect.CloseConnection();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка!");
                        AppConnect.CloseConnection();

                    }

                }
                SaveElementFrame.listService.Clear();
                listServicesBuy.Items.Clear();
            }
            else
            {
                MessageBox.Show("внесите личные данные в профиле!", "Ошибка!");
                SaveElementFrame.frameHub.Navigate(new Profile());
            }
        }
    }
}
