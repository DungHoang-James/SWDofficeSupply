using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.ResourceParams
{
    public class OrderResourceParam : ResourceParams
    {
        public int UserID { get; set; }
        public DateTime FromTime { get; set; } = DateTime.MinValue;
        public DateTime ToTime { get; set; } = DateTime.Now.AddDays(1);
    }
}
