using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.ResourceParams
{
    public class PeriodResourceParam : ResourceParams
    {
        public int CompanyID { get; set; }
        public int? DepartmentID { get; set; }
    }
}
