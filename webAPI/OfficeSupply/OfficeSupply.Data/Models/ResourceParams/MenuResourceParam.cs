using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.ResourceParams
{
    public class MenuResourceParam : ResourceParams
    {
        public int AdminID { get; set; }
        public string Name { get; set; } = "";
        public bool All { get; set; } = false;
    }
}
