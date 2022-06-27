using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class AttachedProduct
    {
        public int MainProductId { get; set; }
        public int AttachedProductId { get; set; }
        public AttachedProduct(int mainProductId, int attachedProductId)
        {
            MainProductId = mainProductId;
            AttachedProductId = attachedProductId;
        }
    }
}
