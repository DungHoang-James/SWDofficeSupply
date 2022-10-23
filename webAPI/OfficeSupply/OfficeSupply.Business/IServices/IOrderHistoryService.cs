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
    public interface IOrderHistoryService
    {
        OrderHistoryDTO GetOrderHistory(int id);
        List<OrderHistoryDTO> GetOrderHistories(ResourceParams resourceParams);
        OrderHistoryDTO CreateOrderHistory(OrderHistoryPayload orderHistoryPayload);
        bool UpdateOrderHistory(int id, OrderHistoryPayload orderHistoryPayload);
        bool DeleteOrderHistory(int id);
    }
}
