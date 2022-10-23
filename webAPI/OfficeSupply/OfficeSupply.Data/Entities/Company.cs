using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Company))]
    public class Company
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public double Wallet { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        [Required, StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        public int AreaID { get; set; }

        public int? ManagerID { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        [ForeignKey(nameof(AreaID))]
        public Area Area { get; set; }

        public List<Department> Departments { get; set; }

        [ForeignKey(nameof(ManagerID))]
        public User Manager { get; set; }
    }
}
