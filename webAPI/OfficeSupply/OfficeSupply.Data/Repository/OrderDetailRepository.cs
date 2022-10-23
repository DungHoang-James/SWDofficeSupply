using Microsoft.EntityFrameworkCore;
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
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;

        public OrderDetailRepository(OfficeSupplyDB officeSupplyDB) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
        }

        public List<OrderDetail> GetOrderDetailsByOrderID(int orderId)
        {
            return _officeSupplyDB.OrderDetails.Where(o => o.OrderID == orderId)
                                               .Include(o => o.ProductInMenu)
                                               .ThenInclude(p => p.Product)
                                               .ThenInclude(p => p.Category).ToList();
        }
    }
}
