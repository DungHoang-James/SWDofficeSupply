using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsMale { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }
        public int? DepartmentID { get; set; }
        public int CompanyID { get; set; }
        public int MenuID { get; set; }
        public int RoleID { get; set; }
        public bool IsDelete { get; set; }
    }
}
