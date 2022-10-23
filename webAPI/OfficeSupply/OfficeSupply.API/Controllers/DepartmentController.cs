using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
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
    [Route(ApiVer1UrlDefinition.Department.Departments)]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;

        public DepartmentController(IDepartmentService departmentService,
                                    IUserService userService)
        {
            this._departmentService = departmentService;
            this._userService = userService;
        }

        [HttpGet]
        //[Route(ApiVer1UrlDefinition.Department.Get)]
        public async Task<ActionResult<GenericResult>> GetDepartments([FromQuery] DepartmentResourceParam resourceParams)
        {
            var result = await _departmentService.GetDepartments(resourceParams);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id}")]
        //[Route(ApiVer1UrlDefinition.Department.GetDetail)]
        public async Task<ActionResult<GenericResult>> GetDepartment(int id)
        {
            var result = await _departmentService.GetDepartment(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        //[Route(ApiVer1UrlDefinition.Department.Add)]
        public async Task<ActionResult<GenericResult>> CreateDepartment([FromBody] DepartmentPayload departmentPayload)
        {
            var result = await _departmentService.CreateDepartment(departmentPayload);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        //[Route(ApiVer1UrlDefinition.Department.Update)]
        public async Task<ActionResult<GenericResult>> UpdateDepartment([FromBody] DepartmentDTO departmentPayload)
        {
            var result = await _departmentService.UpdateDepartment(departmentPayload.ID, departmentPayload);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpDelete("id")]
        //[Route(ApiVer1UrlDefinition.Department.Delete)]
        public async Task<ActionResult<GenericResult>> DeleteDepartment(int id)
        {
            var result = await _departmentService.DeleteDepartment(id);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<GenericResult>> GetUserInDepartment(int id, [FromQuery] DepartmentUserResourceParam resourceParams)
        {
            var result = await _userService.GetUsersByDepartmentId(id, resourceParams);
            return Ok(result);
        }
    }
}
