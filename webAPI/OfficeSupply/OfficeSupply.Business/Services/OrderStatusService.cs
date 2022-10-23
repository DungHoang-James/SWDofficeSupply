using AutoMapper;

using OfficeSupply.Business.IServices;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.IRepository;
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
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderStatusService(IOrderStatusRepository orderStatusRepository, IMapper mapper)
        {
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }

        public OrderStatusDTO CreateOrderStatus(OrderStatusPayload orderStatusPayload)
        {
            var orderSt = _mapper.Map<OrderStatus>(orderStatusPayload);
            var result = _orderStatusRepository.Create(orderSt);
            return result ? _mapper.Map<OrderStatusDTO>(orderSt) : null;
        }

        public OrderStatusDTO GetOrderStatus(int id)
        {
            var orderSt = _orderStatusRepository.GetById(id);
            return orderSt == null ? null : _mapper.Map<OrderStatusDTO>(orderSt);
        }

        public List<OrderStatusDTO> GetOrderStatuses(ResourceParams resourceParams)
        {
            return _mapper.Map<List<OrderStatusDTO>>(_orderStatusRepository.GetOrderStatuses(resourceParams));
        }

        public bool UpdateOrderStatus(int id, OrderStatusPayload orderStatusPayload)
        {
            var orderSt = _orderStatusRepository.GetById(id);
            if (orderSt == null) return false;
            _mapper.Map(orderStatusPayload, orderSt);
            return _orderStatusRepository.Update(orderSt);
        }
    }
}
