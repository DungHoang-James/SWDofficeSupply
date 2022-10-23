using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.ResourceParams
{
    public class CompanyResourceParam : ResourceParams
    {
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public double WalletFrom { get; set; } = 0;
        public double WalletTo { get; set; } = double.MaxValue;
    }
}
