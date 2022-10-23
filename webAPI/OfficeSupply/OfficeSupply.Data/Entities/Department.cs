using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Department))]
    public class Department
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int CompanyID { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        [ForeignKey(nameof(CompanyID))]
        public Company Company { get; set; }

        public List<User> Users { get; set; }
    }
}
