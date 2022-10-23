using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class MenuDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AdminID { get; set; }
        public bool IsDelete { get; set; }
    }
}
