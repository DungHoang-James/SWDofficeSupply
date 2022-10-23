using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(User))]
    public class User
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string Firstname { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? IsMale { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public string AvatarUrl { get; set; }

        public int? DepartmentID { get; set; }

        public int RoleID { get; set; }

        [Required]
        public bool IsDelete { get; set; }
        public string TokenDevice { get; set; }

        [ForeignKey(nameof(DepartmentID))]
        public Department Department { get; set; }

        [ForeignKey(nameof(RoleID))]
        public Role Role { get; set; }

        //public List<Token> Tokens { get; set; }
    }
}
