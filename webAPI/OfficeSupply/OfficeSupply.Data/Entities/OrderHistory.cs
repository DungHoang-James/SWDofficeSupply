using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(OrderHistory))]
    public class OrderHistory
    {
        [Key]
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int? FromStatus { get; set; }

        public int? ToStatus { get; set; }

        public int UserChangeID { get; set; }

        [Required]
        public DateTime TimeChange { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(FromStatus))]
        public OrderStatus FromOrderStatus { get; set; }

        [ForeignKey(nameof(ToStatus))]
        public OrderStatus ToOrderStatus { get; set; }

        [ForeignKey(nameof(UserChangeID))]
        public User UserChange { get; set; }

        [ForeignKey(nameof(OrderID))]
        public Order Order { get; set; }
    }
}
