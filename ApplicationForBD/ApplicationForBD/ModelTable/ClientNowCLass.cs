using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class ClientNowCLass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public ClientNowCLass(string first, string last, string patronymic, string title, DateTime date)
        {
            FirstName = first;
            LastName = last;
            Patronymic = patronymic;
            Title = title;
            StartTime = date;
        }
    }
}
