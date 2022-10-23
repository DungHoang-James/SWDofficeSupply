using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Supplier))]
    public class Supplier
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        [Required, StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        public List<Product> Products { get; set; }
        //public List<SupplierInArea> SupplierInAreas { get; set; }
    }
}
