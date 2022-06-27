using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string MainImagePath { get; set; }
        public bool IsActive { get; set; }
        public int ManufacturerID { get; set; }

        public Product(int iD, string title, decimal cost, string description, string mainImagePath, bool isActive, int manufacturerID)
        {
            ID = iD;
            Title = title;
            Cost = cost;
            Description = description;
            MainImagePath = mainImagePath;
            IsActive = isActive;
            ManufacturerID = manufacturerID;
        }
    }
}
