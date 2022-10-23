using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class SupplierInAreaRepository : BaseRepository<SupplierInArea>, ISupplierInAreaRepository
    {
        public SupplierInAreaRepository(OfficeSupplyDB officeSupplyDB) : base(officeSupplyDB)
        {

        }
    }
}
