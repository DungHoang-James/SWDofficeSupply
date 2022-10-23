using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.Requests
{
    public class OrderPayload
    {
        public int UserOrderID { get; set; }
        public List<OrderDetailPayload> OrderDetail { get; set; }
    }
}
