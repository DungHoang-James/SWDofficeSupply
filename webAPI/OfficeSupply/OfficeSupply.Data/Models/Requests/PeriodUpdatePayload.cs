using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.Requests
{
    public class PeriodUpdatePayload
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public double Quota { get; set; }
        public double RemainingQuota { get; set; }
        public bool ResetQuota { get; set; }
    }
}
