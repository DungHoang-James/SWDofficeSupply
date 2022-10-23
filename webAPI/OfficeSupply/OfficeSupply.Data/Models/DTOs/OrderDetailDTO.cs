using OfficeSupply.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class OrderDetailDTO
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int MenuID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ProductInMenuDTO ProductInMenu { get; set; }
    }
}
