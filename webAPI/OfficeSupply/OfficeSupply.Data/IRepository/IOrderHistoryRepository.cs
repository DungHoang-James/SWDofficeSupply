using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface IOrderHistoryRepository : IBaseRepository<OrderHistory>
    {
        List<OrderHistory> GetOrderHistories(ResourceParams resourceParams);
    }
}
