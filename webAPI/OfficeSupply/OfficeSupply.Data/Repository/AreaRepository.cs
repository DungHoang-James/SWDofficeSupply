using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
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
    public class AreaRepository : BaseRepository<Area>, IAreaRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public AreaRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this._propertyMappingService = propertyMappingService;
        }

        public Pagination<Area> GetAreas(AreaResourceParam resourceParams)
        {
            var areasPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<AreaDTO, Area>();
            var areasQuery = _officeSupplyDB.Areas.Where(a => a.Name.Contains(resourceParams.Name)
                                                         && a.Location.Contains(resourceParams.Location));
            areasQuery = areasQuery.ApplySort(resourceParams.OrderBy, areasPropertyMappingDictionary);
            var pageList = Pagination<Area>.Create(areasQuery, resourceParams.PageNumber, resourceParams.PageSize);
            return pageList;
        }

        public List<Area> GetAreasMin()
        {
            return _officeSupplyDB.Areas.Where(a => a.IsDelete == false).ToList();
        }

        public List<AreaStatisticsDTO> GetAreaStatistics()
        {
            return _officeSupplyDB.Areas.Include(a => a.Companies)
                                        .Select(a => new AreaStatisticsDTO() { ID = a.ID, NumOfCompany = a.Companies.Count }).ToList();
        }
    }
}
