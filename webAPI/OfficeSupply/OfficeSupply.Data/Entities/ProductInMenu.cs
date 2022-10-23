using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(ProductInMenu))]
    public class ProductInMenu
    {
        public int MenuID { get; set; }

        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey(nameof(ProductID))]
        public Product Product { get; set; }

        [ForeignKey(nameof(MenuID))]
        public Menu Menu { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
