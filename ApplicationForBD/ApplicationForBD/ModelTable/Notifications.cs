using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class NotificationsModel
    {
        public string TitleService { get; set; }
        public DateTime DateStart { get; set; }
        public string NotificationDate { get; }
        public string ImagePathService { get; set; }
        public double Duration { get; set; }


        public NotificationsModel(string title, DateTime date, string path, double duration)
        {
            TitleService = title;
            DateStart = date;
            ImagePathService = path;
            Duration = duration / 60.0;
            NotificationDate = Math.Round(DateStart.Subtract(DateTime.Now).TotalDays).ToString();
        }
    }
}
