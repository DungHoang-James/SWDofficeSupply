using OfficeSupply.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        List<OrderDetail> GetOrderDetailsByOrderID(int orderId);
    }
}
