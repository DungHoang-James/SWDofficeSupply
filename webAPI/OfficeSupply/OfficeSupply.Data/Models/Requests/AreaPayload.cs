using OfficeSupply.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.Requests
{
    public class AreaPayload
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int MenuID { get; set; }
        //public List<SupplierInAreaPayload> SupplierInAreas { get; set; }
    }
}
