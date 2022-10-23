using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.Requests
{
    public class OrderUpdatePayload
    {
        public int UserApproveID { get; set; }
        public int OrderID { get; set; }
        public bool IsApprove { get; set; }
        public string Description { get; set; }
    }
}
