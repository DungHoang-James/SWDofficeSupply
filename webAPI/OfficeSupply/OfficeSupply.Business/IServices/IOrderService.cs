using OfficeSupply.Business.Ultility.common;
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
    public interface IOrderService
    {
        public Task<GenericResult> GetOrder(int id);
        public Task<GenericResult> GetOrders(OrderResourceParam resourceParams);
        public Task<GenericResult> CreateOrder(OrderPayload orderPayload);
        public Task<GenericResult> UpdateOrder(OrderUpdatePayload orderPayload);
        public Task<GenericResult> DeleteOrder(int id);
    }
}
