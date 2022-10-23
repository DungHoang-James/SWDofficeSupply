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
    public class OrderStatusRepository : BaseRepository<OrderStatus>, IOrderStatusRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public OrderStatusRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            _officeSupplyDB = officeSupplyDB;
            _propertyMappingService = propertyMappingService;
        }

        public List<OrderStatus> GetOrderStatuses(ResourceParams resourceParams)
        {
            var orderStatusMap = _propertyMappingService.GetPropertyMapping<OrderStatusDTO, OrderStatus>();
            var orderStatusQuery = _officeSupplyDB.OrderStatus.ApplySort(resourceParams.OrderBy, orderStatusMap);
            orderStatusQuery = orderStatusQuery.Skip((resourceParams.PageNumber - 1) * resourceParams.PageSize).Take(resourceParams.PageSize);
            return orderStatusQuery.ToList();
        }
    }
}
