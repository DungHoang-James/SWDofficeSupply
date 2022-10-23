using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class UserUpdatePayload
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsMale { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
