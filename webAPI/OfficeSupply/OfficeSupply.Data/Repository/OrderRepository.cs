using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using OfficeSupply.Data.PropertyMapping;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public OrderRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            _officeSupplyDB = officeSupplyDB;
            _propertyMappingService = propertyMappingService;
        }

        public Pagination<Order> GetOrders(OrderResourceParam resourceParams)
        {
            var orderMap = _propertyMappingService.GetPropertyMapping<OrderDTO, Order>();
            var orderQuery = _officeSupplyDB.Orders.ApplySort(resourceParams.OrderBy, orderMap);

            var pageList = Pagination<Order>.Create(orderQuery, resourceParams.PageNumber, resourceParams.PageSize);
            return pageList;
        }

        public Pagination<Order> GetOrdersByUserID(OrderResourceParam resourceParams)
        {
            var orderMap = _propertyMappingService.GetPropertyMapping<OrderDTO, Order>();


            var orderQuery = _officeSupplyDB.Orders.Where(o => o.UserOrderID == resourceParams.UserID);
            orderQuery = orderQuery.ApplySort(resourceParams.OrderBy, orderMap);

            var pageList = Pagination<Order>.Create(orderQuery, resourceParams.PageNumber, resourceParams.PageSize);
            return pageList;
        }

        public Pagination<Order> GetOrderOfDepartment(OrderResourceParam resourceParam, int departmentID)
        {
            var orderMap = _propertyMappingService.GetPropertyMapping<OrderDTO, Order>();

            var orderQuery = _officeSupplyDB.Orders.Include(o => o.UserOrder)
                                                   .Where(u => u.UserOrder.DepartmentID == departmentID &&
                                                               u.CreateTime >= resourceParam.FromTime &&
                                                               u.CreateTime <= resourceParam.ToTime);
            orderQuery = orderQuery.ApplySort(resourceParam.OrderBy, orderMap);

            var pageList = Pagination<Order>.Create(orderQuery, resourceParam.PageNumber, resourceParam.PageSize);
            return pageList;
        }

        public Pagination<Order> GetOrderOfCompany(OrderResourceParam resourceParam, int companyID)
        {
            var orderMap = _propertyMappingService.GetPropertyMapping<OrderDTO, Order>();

            var orderQuery = _officeSupplyDB.Orders.Include(o => o.UserOrder)
                                                   .ThenInclude(u => u.Department)
                                                   .Include(o => o.UserApprove)
                                                   .Where(o => o.UserOrder.Department.CompanyID == companyID &&
                                                               o.OrderStatusID >= 2 &&
                                                               o.CreateTime >= resourceParam.FromTime &&
                                                               o.CreateTime <= resourceParam.ToTime);
            orderQuery = orderQuery.ApplySort(resourceParam.OrderBy, orderMap);

            var pageList = Pagination<Order>.Create(orderQuery, resourceParam.PageNumber, resourceParam.PageSize);
            return pageList;
        }

        public Order GetOrderHasUserInfo(int orderId)
        {
            return _officeSupplyDB.Orders.Include(o => o.UserOrder).FirstOrDefault(o => o.ID == orderId);
        }
    }
}
