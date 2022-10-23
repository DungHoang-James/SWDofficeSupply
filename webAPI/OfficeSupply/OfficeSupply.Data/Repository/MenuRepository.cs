
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
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public MenuRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this._propertyMappingService = propertyMappingService;
        }

        public Pagination<Menu> GetMenus(MenuResourceParam resourceParams)
        {
            var menuPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<MenuDTO, Menu>();
            var menuQuery = _officeSupplyDB.Menus.Where(m => m.Name.Contains(resourceParams.Name) && m.AdminID == resourceParams.AdminID);
            menuQuery = menuQuery.ApplySort(resourceParams.OrderBy, menuPropertyMappingDictionary);
            var pageList = Pagination<Menu>.Create(menuQuery, resourceParams.PageNumber, resourceParams.PageSize);
            return pageList;
        }

        public List<Menu> GetMenusMin()
        {
            return _officeSupplyDB.Menus.Where(m => m.IsDelete == false).ToList();
        }
    }
}
