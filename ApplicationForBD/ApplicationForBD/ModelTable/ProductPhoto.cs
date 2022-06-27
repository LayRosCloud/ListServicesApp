using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class ProductPhoto
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string PhotoPath { get; set; }

        public ProductPhoto(int iD, int productID, string photoPath)
        {
            ID = iD;
            ProductID = productID;
            PhotoPath = photoPath;
        }
    }
}
