using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class Manufacturer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public Manufacturer(int iD, string name, DateTime startDate)
        {
            ID = iD;
            Name = name;
            StartDate = startDate;
        }
    }
}
