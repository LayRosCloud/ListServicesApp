using ApplicationForBD.ApplicationDataBases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ApplicationForBD.Pages
{
    public partial class ClientsNowPage : Page
    {
        public ClientsNowPage() //Инициализация компонентов.
        {
            InitializeComponent();
            StartTimer(); //Запуск обновления списка каждые 30 минут.
            Refresh(); //Обновления списка

        }
        private void StartTimer() //Запуск обновления списка каждые 30 минут.
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 30, 0);
            dispatcherTimer.Start();
        }
        private void Refresh() //Обновления списка
        {
            List<ClientNowCLass> clientNowCLasses = new List<ClientNowCLass>();

            //получение списка в виде 'Фамилия, имя, отчества, названия услуги и даты начала'.
            SqlCommand sql = new SqlCommand(@"SELECT c.FirstName, c.LastName, c.Patronymic, s.Title, cs.StartTime
                                            FROM[dbo].[ClientService] cs, [dbo].[Service] s, [dbo].[Client] c
                                            WHERE cs.ClientID = c.ID AND cs.ServiceID = s.ID", AppConnect.GetConnection); 
            AppConnect.OpenConnection();
            SqlDataReader reader = sql.ExecuteReader();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                    clientNowCLasses.Add(new ClientNowCLass(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4)));
            }

            listClients.ItemsSource = (from s in clientNowCLasses //инициализация списка.
                                       where s.StartTime >= DateTime.Now && s.StartTime <= DateTime.Now.AddDays(1)
                                       orderby s.StartTime 
                                       select s).ToList();

            AppConnect.CloseConnection();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e) 
            => Refresh();
        
    }
}
