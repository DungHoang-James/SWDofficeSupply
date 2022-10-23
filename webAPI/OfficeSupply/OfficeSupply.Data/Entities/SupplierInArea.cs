using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(SupplierInArea))]
    public class SupplierInArea
    {
        public int SupplierID { get; set; }

        public int AreaID { get; set; }

        [ForeignKey(nameof(SupplierID))]
        public Supplier Supplier { get; set; }

        [ForeignKey(nameof(AreaID))]
        public Area Area { get; set; }
    }
}
