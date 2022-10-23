using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.ResourceParams
{
    public class ProductResourceParam : ResourceParams
    {
        public int UserID { get; set; }
        public string Name { get; set; } = "";
    }
}
