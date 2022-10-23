using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(OrderDetail))]
    public class OrderDetail
    {
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public int MenuID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [ForeignKey(nameof(OrderID))]
        public Order Order { get; set; }

        public ProductInMenu ProductInMenu { get; set; }
    }
}
