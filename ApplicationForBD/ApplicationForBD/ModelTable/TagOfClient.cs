using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class TagOfClient
    {
        public int ClientID { get; set; }
        public int TagID { get; set; }
        public TagOfClient(int clientID, int tagID)
        {
            ClientID = clientID;
            TagID = tagID;
        }
    }
}
