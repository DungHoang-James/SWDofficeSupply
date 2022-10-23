using OfficeSupply.Business.Ultility.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface IOrderDetailService
    {
        Task<GenericResult> GetOrderDetailsByOrderID(int orderId);
    }
}
