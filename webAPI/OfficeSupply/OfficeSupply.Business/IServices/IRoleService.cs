using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface IRoleService
    {
        public Task<GenericResult> GetRole(int id);
        public Task<GenericResult> GetRoles(ResourceParams resourceParams);
        public Task<GenericResult> CreateRole(RolePayload rolePayload);
        public Task<GenericResult> UpdateRole(int id, RolePayload rolePayload);
    }
}
