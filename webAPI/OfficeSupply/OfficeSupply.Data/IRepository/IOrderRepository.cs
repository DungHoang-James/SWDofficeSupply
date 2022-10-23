using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Pagination<Order> GetOrders(OrderResourceParam resourceParams);
        Pagination<Order> GetOrdersByUserID(OrderResourceParam resourceParams);
        Pagination<Order> GetOrderOfDepartment(OrderResourceParam resourceParam, int departmentID);
        Pagination<Order> GetOrderOfCompany(OrderResourceParam resourceParam, int companyID);
        Order GetOrderHasUserInfo(int orderId);
    }
}
