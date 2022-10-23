using AutoMapper;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPeriodRepository _periodRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository,
                                  IOrderRepository orderRepository,
                                  IPeriodRepository periodRepository,
                                  IMapper mapper)
        {
            this._orderDetailRepository = orderDetailRepository;
            this._orderRepository = orderRepository;
            this._periodRepository = periodRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetOrderDetailsByOrderID(int orderId)
        {
            var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderID(orderId);

            var result = _mapper.Map<List<OrderDetailDTO>>(orderDetails);

            return ApiResponse.Ok(resData: result);

            //var order = _orderRepository.GetOrderHasUserInfo(orderId);
            //var departmentId = (int)order.UserOrder.DepartmentID;
            //var period = _periodRepository.GetCurrentPeriod(departmentId);

        }
    }
}
