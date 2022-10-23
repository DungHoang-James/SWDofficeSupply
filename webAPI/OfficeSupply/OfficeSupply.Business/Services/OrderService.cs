using AutoMapper;

using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderHistoryRepository _historyRepository;
        private readonly IPeriodRepository _periodRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFirebaseService _firebaseService;
        private readonly IProductInMenuRepository _productInMenuRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository,
                            IOrderDetailRepository orderDetailRepository,
                            IUserRepository userRepository,
                            IOrderHistoryRepository historyRepository,
                            IPeriodRepository periodRepository,
                            IDepartmentRepository departmentRepository,
                            IFirebaseService firebaseService,
                            IProductInMenuRepository productInMenuRepository,
                            IMapper mapper)
        {
            _orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._userRepository = userRepository;
            this._historyRepository = historyRepository;
            this._periodRepository = periodRepository;
            this._departmentRepository = departmentRepository;
            this._firebaseService = firebaseService;
            this._productInMenuRepository = productInMenuRepository;
            _mapper = mapper;
        }

        public async Task<GenericResult> CreateOrder(OrderPayload orderPayload)
        {
            var userOrder = _userRepository.GetById(orderPayload.UserOrderID);
            var userHigher = _userRepository.GetLeaderInDepartmentId((int)userOrder.DepartmentID);

            var title = $"Have 1 order request";
            var body = $"{userOrder.Firstname} {userOrder.Lastname} request 1 order";

            var order = new Order()
            {
                CreateTime = DateTime.Now,
                UserOrderID = orderPayload.UserOrderID,
                OrderStatusID = OrderStatusBase.WAITING,
            };

            if (userOrder.RoleID == RoleBase.LEADER)
            {
                order.UserApproveID = orderPayload.UserOrderID;
                order.ApproveTime = DateTime.Now;
                order.OrderStatusID = OrderStatusBase.LEADER_APPROVE;

                userHigher = _userRepository.GetManagerInCompany(userHigher.Department.CompanyID);
            }

            var result = _orderRepository.Create(order);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create Order fail");
            }

            foreach (var orderDetail in orderPayload.OrderDetail)
            {
                var newOrderDetail = _mapper.Map<OrderDetail>(orderDetail);
                newOrderDetail.OrderID = order.ID;

                _orderDetailRepository.Create(newOrderDetail);
            }

            if (userHigher.TokenDevice != null)
            {
                _firebaseService.SendMessage(userHigher.TokenDevice, title, body);
            }

            return ApiResponse.Ok(_mapper.Map<OrderDTO>(order));
        }

        public async Task<GenericResult> DeleteOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            //set status to cancel...
            order.OrderStatusID = 3;
            var result = _orderRepository.Update(order);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Delete Order fail");
            }
            return ApiResponse.Ok();
        }

        public async Task<GenericResult> GetOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            var result = _mapper.Map<OrderDTO>(order);
            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> GetOrders(OrderResourceParam resourceParams)
        {
            Pagination<Order> pageListOrders;
            var user = _userRepository.GetUserByID(resourceParams.UserID);

            if (user == null)
            {
                return ApiResponse.Error(messDetail: "Not found user match userID");
            }

            if (user.RoleID == RoleBase.EMPLOYEE)
            {
                pageListOrders = _orderRepository.GetOrdersByUserID(resourceParams);
            }
            else if (user.RoleID == RoleBase.LEADER)
            {
                pageListOrders = _orderRepository.GetOrderOfDepartment(resourceParams, (int)user.DepartmentID);
            }
            else
            {
                var department = _departmentRepository.GetDepartmentByID((int)user.DepartmentID);
                pageListOrders = _orderRepository.GetOrderOfCompany(resourceParams, department.CompanyID);
            }

            var orders = new Pagination<OrderDTO>(_mapper.Map<List<OrderDTO>>(pageListOrders.Items),
                                                        pageListOrders.TotalCount,
                                                        pageListOrders.CurrentPage,
                                                        pageListOrders.PageSize);
            return ApiResponse.Ok(orders);
        }

        public async Task<GenericResult> UpdateOrder(OrderUpdatePayload orderPayload)
        {
            var order = _orderRepository.GetById(orderPayload.OrderID);
            if (order == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);

            string title;
            string body;

            order.ApproveTime = DateTime.Now;
            order.UserApproveID = orderPayload.UserApproveID;

            var userOrder = _userRepository.GetUserByID((int)order.UserOrderID);
            var currentPeriod = _periodRepository.GetCurrentPeriod((int)userOrder.DepartmentID);

            // old order status
            var oldOrderstatus = order.OrderStatusID;

            if (orderPayload.IsApprove)
            {
                var user = _userRepository.GetUserByID(orderPayload.UserApproveID);

                if (user.RoleID == RoleBase.LEADER)
                {
                    order.OrderStatusID = OrderStatusBase.LEADER_APPROVE;
                    title = "Leader approved your order request";
                    body = $"Your order request has been approved. Order ID: {order.ID}";
                }
                else if (user.RoleID == RoleBase.MANAGER)
                {
                    order.OrderStatusID = OrderStatusBase.MANAGER_APPROVE;
                    title = "Manager approved your order request";
                    body = $"Your order request has been approved. Order ID: {order.ID}";

                    var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderID(orderPayload.OrderID);
                    double total = 0;
                    foreach (var o in orderDetails)
                    {
                        total += (o.Quantity * o.Price);

                        var productInMenu = _productInMenuRepository.GetProductInMenu(o.MenuID, o.ProductID);
                        productInMenu.Quantity -= o.Quantity;
                        _productInMenuRepository.Update(productInMenu);
                    }
                    currentPeriod.RemainingQuota -= total;
                }
                else
                {
                    return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Update Order fail");
                }
            }
            else
            {
                order.OrderStatusID = OrderStatusBase.CANCEL;
                title = "Your order has been rejected";
                body = $"Your order request has been rejected. Order ID: {order.ID}";
            }

            var result = _orderRepository.Update(order);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Update Order fail");
            }

            _periodRepository.Update(currentPeriod);

            var orderHistory = new OrderHistory()
            {
                OrderID = order.ID,
                FromStatus = oldOrderstatus,
                ToStatus = order.OrderStatusID,
                UserChangeID = orderPayload.UserApproveID,
                TimeChange = DateTime.Now,
                Description = orderPayload.Description
            };

            var frs = _historyRepository.Create(orderHistory);
            if (frs == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Update Order fail");
            }

            if (userOrder.TokenDevice != null)
            {
                _firebaseService.SendMessage(userOrder.TokenDevice, title, body);
            }

            return ApiResponse.Ok(frs);
        }
    }
}
