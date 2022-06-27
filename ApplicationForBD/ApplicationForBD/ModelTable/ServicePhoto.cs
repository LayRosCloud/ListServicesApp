using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class ServicePhoto
    {
        public int ID { get; set; }
        public int ServiceID { get; set; }
        public string PhotoPath { get; set; }
        public ServicePhoto(int iD, int serviceID, string photoPath)
        {
            ID = iD;
            ServiceID = serviceID;
            PhotoPath = photoPath;
        }
    }
}
