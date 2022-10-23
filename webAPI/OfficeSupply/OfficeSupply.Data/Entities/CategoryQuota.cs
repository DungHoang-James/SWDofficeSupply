using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Entities
{
    [Table(nameof(CategoryQuota))]
    public class CategoryQuota
    {
        public int PeriodID { get; set; }

        public int CategoryID { get; set; }

        public int LimitedAmount { get; set; }

        [ForeignKey(nameof(PeriodID))]
        public Period Period { get; set; }

        [ForeignKey(nameof(CategoryID))]
        public Category Category { get; set; }
    }
}
