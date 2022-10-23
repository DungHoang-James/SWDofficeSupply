using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Menu))]
    public class Menu
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public int AdminID { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        [ForeignKey(nameof(AdminID))]
        public User Admin { get; set; }

        public List<ProductInMenu> ProductInMenus { get; set; }
        public List<Area> Areas { get; set; }
    }
}
