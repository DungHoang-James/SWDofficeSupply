using OfficeSupply.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class ProductInMenuDTO
    {
        public int MenuID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool IsDelete { get; set; }
        public ProductDTO Product { get; set; }
    }
}
