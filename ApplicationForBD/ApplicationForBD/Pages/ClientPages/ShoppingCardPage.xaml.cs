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
            CountingSum();
            InitializeComponent();
            nothingService.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

            InitializeList();
            gridDownMenu.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            backButton.Text = "<";
        }
        private void CountingSum()
        {
            CostSum = 0;
            DiscountPriceSum = 0;
            for (int i = 0; i < SaveElementFrame.listService.Count; i++)
            {
                CostSum += SaveElementFrame.listService[i].Cost;
                DiscountPriceSum += SaveElementFrame.listService[i].DiscountPrice;
            }
        }
        private void InitializeList()
        {
            for (int i = 0; i < SaveElementFrame.listService.Count; i++)
            {
                listServicesBuy.Items.Add(SaveElementFrame.listService[i]);
            }
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
            SqlDataReader reader = AppConnect.GetOpenReader($"SELECT ServiceID, StartTime FROM [dbo].[ClientService]");
            DateTime date = DateTime.MinValue;
            while (reader.Read())
            {
                if (date <= reader.GetDateTime(1))
                    date = reader.GetDateTime(1);
            }
            AppConnect.CloseConnection();

            if (date < DateTime.Now)
            {
                date = DateTime.Now.AddDays(1);
                date = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
            }

            reader = AppConnect.GetOpenReader($"SELECT StartTime, s.DurationInSeconds FROM [dbo].[ClientService] cs, [dbo].[Service] s WHERE cs.ServiceID = s.ID");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (date <= reader.GetDateTime(0).AddMinutes(reader.GetInt32(1) / 60.0))
                    {
                        if (date.AddMinutes(reader.GetInt32(1) / 60.0) >= new DateTime(date.Year, date.Month, date.Day, 19, 0, 0))
                        {
                            date = date.AddDays(1);
                            date = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
                        }
                        else
                            date = date.AddMinutes(reader.GetInt32(1)/60.0);
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

                if (SaveElementFrame.client != null)
                {
                    SqlDataReader reader = AppConnect.GetOpenReader($"Select * From [dbo].[ClientService] WHERE ClientID = {SaveElementFrame.client.ID}");
                    if (reader.HasRows)
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            if (reader.GetDateTime(3) >= DateTime.Now)
                                count++;
                        }
                        SaveElementFrame.countNot.Text = count.ToString();
                        SaveElementFrame.borderCount.Visibility = count == 0 ? Visibility.Collapsed : Visibility.Visible;

                    }
                    AppConnect.CloseConnection();
                }

                SaveElementFrame.listService.Clear();
                listServicesBuy.Items.Clear();
                gridDownMenu.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
                nothingService.Visibility = SaveElementFrame.listService.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

            }
            else
            {
                MessageBox.Show("внесите личные данные в профиле!", "Ошибка!");
                SaveElementFrame.frameHub.Navigate(new Profile());
            }
        }
    }
}
