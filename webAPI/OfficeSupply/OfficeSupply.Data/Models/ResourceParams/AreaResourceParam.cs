using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.ResourceParams
{
    public class AreaResourceParam : ResourceParams
    {
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public bool All { get; set; } = false;
    }
}
