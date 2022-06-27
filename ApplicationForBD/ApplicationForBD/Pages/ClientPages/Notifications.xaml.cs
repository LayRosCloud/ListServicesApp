using ApplicationForBD.ApplicationDataBases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ApplicationForBD.Pages
{
    /// <summary>
    /// Логика взаимодействия для Notifications.xaml
    /// </summary>
    public partial class Notifications : Page
    {
        public Notifications()
        {
            InitializeComponent();
            if (SaveElementFrame.client != null)
            {
                List<NotificationsModel> notifications = new List<NotificationsModel>();
                SqlCommand sql = new SqlCommand($@"SELECT s.Title, cs.StartTime, s.MainImagePath, s.DurationInSeconds
                FROM [dbo].[ClientService] cs, [dbo].[Service] s
                WHERE  ClientID = {SaveElementFrame.client.ID} AND cs.ServiceID = s.ID", AppConnect.GetConnection);
                AppConnect.OpenConnection();
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if(reader.GetDateTime(1) > DateTime.Now)
                            notifications.Add(new NotificationsModel(reader.GetString(0), reader.GetDateTime(1), "\\"+reader.GetString(2).Trim(), reader.GetInt32(3)));
                    }
                }
                listNotifications.ItemsSource = (from s in notifications orderby s.NotificationDate, s.DateStart select s).ToList();
            }

        }
    }
}
