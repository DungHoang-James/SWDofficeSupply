using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(Period))]
    public class Period
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public int DepartmentID { get; set; }

        [Required]
        public DateTime FromTime { get; set; }

        [Required]
        public DateTime ToTime { get; set; }

        [Required]
        public double Quota { get; set; }

        public double RemainingQuota { get; set; }

        [Required]
        public bool IsExpired { get; set; }

        [ForeignKey(nameof(DepartmentID))]
        public Department Department { get; set; }
    }
}
