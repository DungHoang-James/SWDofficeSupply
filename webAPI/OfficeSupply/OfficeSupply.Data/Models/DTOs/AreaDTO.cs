using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class AreaDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MenuID { get; set; }
        public bool IsDelete { get; set; }
    }
}
