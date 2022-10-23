using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.Requests
{
    public class UserPayload
    {
        public string Email { get; set; }
        public int? DepartmentID { get; set; }
        public int RoleID { get; set; }
    }
}
