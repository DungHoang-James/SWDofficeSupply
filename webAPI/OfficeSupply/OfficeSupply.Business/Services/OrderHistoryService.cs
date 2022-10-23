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
    class OrderHistoryService : IOrderHistoryService
    {
        private readonly IOrderHistoryRepository _orderHistoryRepository;
        private readonly IMapper _mapper;

        public OrderHistoryService(IOrderHistoryRepository orderHistoryRepository, IMapper mapper)
        {
            _orderHistoryRepository = orderHistoryRepository;
            _mapper = mapper;
        }

        public OrderHistoryDTO CreateOrderHistory(OrderHistoryPayload orderHistoryPayload)
        {
            var orderHis = _mapper.Map<OrderHistory>(orderHistoryPayload);
            var result = _orderHistoryRepository.Create(orderHis);
            return result ? _mapper.Map<OrderHistoryDTO>(orderHis) : null;
        }

        public bool DeleteOrderHistory(int id)
        {
            var orderHis = _orderHistoryRepository.GetById(id);
            if (orderHis == null) return false;
            // set status to cancel ...
            orderHis.ToStatus = 3;
            return _orderHistoryRepository.Update(orderHis);
        }

        public List<OrderHistoryDTO> GetOrderHistories(ResourceParams resourceParams)
        {
            return _mapper.Map<List<OrderHistoryDTO>>(_orderHistoryRepository.GetOrderHistories(resourceParams));
        }

        public OrderHistoryDTO GetOrderHistory(int id)
        {
            var orderHis = _orderHistoryRepository.GetById(id);
            return orderHis == null ? null : _mapper.Map<OrderHistoryDTO>(orderHis);
        }


        public bool UpdateOrderHistory(int id, OrderHistoryPayload orderHistoryPayload)
        {
            var orderHis = _orderHistoryRepository.GetById(id);
            if (orderHis == null) return false;
            _mapper.Map(orderHistoryPayload, orderHis);
            return _orderHistoryRepository.Update(orderHis);
        }
    }
}
