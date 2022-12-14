using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class OrderHistoryDTO
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ToStatus { get; set; }
        public int FromStatus { get; set; }
        public int UserChangeID { get; set; }
        public DateTime TimeChange { get; set; }
        public string Description { get; set; }
    }
}
