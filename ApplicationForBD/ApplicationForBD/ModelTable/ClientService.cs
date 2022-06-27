using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class ClientService
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int ServiceID { get; set; }
        public DateTime StartTime { get; set; }
        public string Comment { get; set; }
        public ClientService(int id, int clientID, int serviceID, DateTime start, string comment)
        {
            ID = id;
            ClientID = clientID;
            ServiceID = serviceID;
            StartTime = start;
            Comment = comment;
        }
    }
}
