using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Area))]
    public class Area
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        public int MenuID { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        [ForeignKey(nameof(MenuID))]
        public Menu Menu { get; set; }

        //public List<SupplierInArea> SupplierInAreas { get; set; }
        public List<Company> Companies { get; set; }
    }
}
