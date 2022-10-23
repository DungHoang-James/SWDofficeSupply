using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Order))]
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public DateTime? ApproveTime { get; set; }

        public int? UserOrderID { get; set; }

        public int? UserApproveID { get; set; }

        public int OrderStatusID { get; set; }

        [ForeignKey(nameof(UserOrderID))]
        public User UserOrder { get; set; }

        [ForeignKey(nameof(UserApproveID))]
        public User UserApprove { get; set; }

        [ForeignKey(nameof(OrderStatusID))]
        public OrderStatus OrderStatus { get; set; }

        public List<OrderHistory> OrderHistories { get; set; }
    }
}
