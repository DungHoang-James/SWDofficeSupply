using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Models.DTOs
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public CategoryDTO Category { get; set; }
        public bool IsDelete { get; set; }
    }
}
