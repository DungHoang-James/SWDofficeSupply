using OfficeSupply.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ApproveTime { get; set; }
        public int? UserOrderID { get; set; }
        public int? UserApproveID { get; set; }
        public int OrderStatusID { get; set; }
        public UserDTO UserOrder { get; set; }
        public UserDTO UserApprove { get; set; }
    }
}
