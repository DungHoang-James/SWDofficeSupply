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
    public class OrderHistoryRepository : BaseRepository<OrderHistory>, IOrderHistoryRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public OrderHistoryRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            _officeSupplyDB = officeSupplyDB;
            _propertyMappingService = propertyMappingService;
        }

        public List<OrderHistory> GetOrderHistories(ResourceParams resourceParams)
        {
            var orderHistoryMap = _propertyMappingService.GetPropertyMapping<OrderHistoryDTO, OrderHistory>();
            var orderHistoryQuery = _officeSupplyDB.OrderHistories.ApplySort(resourceParams.OrderBy, orderHistoryMap);
            orderHistoryQuery = orderHistoryQuery.Skip((resourceParams.PageNumber - 1) * resourceParams.PageSize).Take(resourceParams.PageSize);
            return orderHistoryQuery.ToList();
        }
    }
}
