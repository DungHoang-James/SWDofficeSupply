using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Product))]
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryID { get; set; }

        public int SupplierID { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public Category Category { get; set; }

        [ForeignKey(nameof(SupplierID))]
        public Supplier Supplier { get; set; }

        public List<ProductInMenu> ProductInMenus { get; set; }
    }
}
