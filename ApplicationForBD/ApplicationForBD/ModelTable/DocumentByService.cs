using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class DocumentByService
    {
        public int ID { get; set; }
        public int ClientServiceID { get; set; }
        public string DocumentPath { get; set; }
        public DocumentByService(int iD, int clientServiceID, string documentPath)
        {
            ID = iD;
            ClientServiceID = clientServiceID;
            DocumentPath = documentPath;
        }
    }
}
