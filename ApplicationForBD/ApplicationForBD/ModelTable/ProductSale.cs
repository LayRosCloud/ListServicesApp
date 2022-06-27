using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class ProductSale
    {
        public int ID { get; set; }
        public DateTime SaleDate { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int ClientServiceID { get; set; }
        public ProductSale(int iD, DateTime saleDate, int productID, int quantity, int clientServiceID)
        {
            ID = iD;
            SaleDate = saleDate;
            ProductID = productID;
            Quantity = quantity;
            ClientServiceID = clientServiceID;
        }
    }
}
