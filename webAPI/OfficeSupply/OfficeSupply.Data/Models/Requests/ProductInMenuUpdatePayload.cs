using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.Requests
{
    public class ProductInMenuUpdatePayload
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool IsDelete { get; set; }
    }
}
