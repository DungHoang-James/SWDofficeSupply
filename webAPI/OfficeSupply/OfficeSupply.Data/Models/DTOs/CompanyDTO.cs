using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class CompanyDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Wallet { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int AreaID { get; set; }
        public int ManagerID { get; set; }
        public bool IsDelete { get; set; }
    }
}
