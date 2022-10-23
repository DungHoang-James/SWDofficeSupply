using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface IOrderStatusService
    {
        OrderStatusDTO GetOrderStatus(int id);
        List<OrderStatusDTO> GetOrderStatuses(ResourceParams resourceParams);
        OrderStatusDTO CreateOrderStatus(OrderStatusPayload orderStatusPayload);
        bool UpdateOrderStatus(int id, OrderStatusPayload orderStatusPayload);
    }
}
