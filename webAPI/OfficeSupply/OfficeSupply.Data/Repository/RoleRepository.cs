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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public RoleRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            _officeSupplyDB = officeSupplyDB;
            _propertyMappingService = propertyMappingService;
        }

        public List<Role> GetRoles(ResourceParams resourceParams)
        {
            var roleMap = _propertyMappingService.GetPropertyMapping<RoleDTO, Role>();
            var roleQuery = _officeSupplyDB.Roles.ApplySort(resourceParams.OrderBy, roleMap);
            roleQuery = roleQuery.Skip((resourceParams.PageNumber - 1) * resourceParams.PageSize).Take(resourceParams.PageSize);
            return roleQuery.ToList();
        }
    }
}
