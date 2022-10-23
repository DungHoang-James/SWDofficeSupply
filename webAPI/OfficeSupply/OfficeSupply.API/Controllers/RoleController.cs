using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Services;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route(ApiVer1UrlDefinition.Role.Roles)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpGet]
        //[Route(ApiVer1UrlDefinition.Role.Get)]
        public async Task<ActionResult<GenericResult>> GetRoles([FromQuery] ResourceParams resourceParams)
        {
            var result = await _roleService.GetRoles(resourceParams);
            return result;
        }

        [HttpGet("{id}")]
        //[Route(ApiVer1UrlDefinition.Role.GetDetail)]
        public async Task<GenericResult> GetRole(int id)
        {
            var result = await _roleService.GetRole(id);
            return result;
        }

        [HttpPost]
        //[Route(ApiVer1UrlDefinition.Role.Add)]
        public async Task<GenericResult> CreateRole([FromBody] RolePayload rolePayload)
        {
            var result = await _roleService.CreateRole(rolePayload);
            return result;
        }

        [HttpPut]
        //[Route(ApiVer1UrlDefinition.Role.Update)]
        public async Task<GenericResult> UpdateRole(int id, [FromBody] RolePayload rolePayload)
        {
            var result = await _roleService.UpdateRole(id, rolePayload);
            return result;
        }

    }
}
