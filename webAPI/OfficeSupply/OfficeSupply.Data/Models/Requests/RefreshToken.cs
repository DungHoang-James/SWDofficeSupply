using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.Requests
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
    }
}
