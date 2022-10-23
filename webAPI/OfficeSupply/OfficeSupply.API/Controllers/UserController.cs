using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    //[EnableCors]
    [Route(ApiVer1UrlDefinition.User.Users)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GenericResult>> GetUsers([FromQuery] UserResourceParam resourceParam)
        {
            var result = await _userService.GetUsers(resourceParam);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResult>> GetUser(int id)
        {
            var result = await _userService.GetUser(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResult>> UpdateUser(int id, [FromBody] UserUpdatePayload updatePayload)
        {
            var result = await _userService.UpdateUserInfo(id, updatePayload);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpPut]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<GenericResult>> UpdateUser([FromBody] UserPayload userDTO)
        {
            var result = await _userService.UpdateUser(userDTO);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }
    }
}
