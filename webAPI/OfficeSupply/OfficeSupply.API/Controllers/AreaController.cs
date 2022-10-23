using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Entities;
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
    [Route(ApiVer1UrlDefinition.Area.Areas)]
    //[Authorize]
    public class AreaController : ControllerBase
    {
        public readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            this._areaService = areaService;
        }

        [HttpGet]
        //[Route(ApiVer1UrlDefinition.Area.Get)]
        public async Task<ActionResult<GenericResult>> GetAreas([FromQuery] AreaResourceParam resourceParams)
        {
            var result = await _areaService.GetAreas(resourceParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        //[Route(ApiVer1UrlDefinition.Area.GetDetail)]
        public async Task<ActionResult<GenericResult>> GetArea(int id)
        {
            var area = await _areaService.GetArea(id);
            return area.IsSuccess ? Ok(area) : NotFound(area);
        }

        [HttpPost]
        //[Route(ApiVer1UrlDefinition.Area.Add)]
        public async Task<ActionResult<GenericResult>> CreateArea([FromBody] AreaPayload areaPayload)
        {
            var area = await _areaService.CreateArea(areaPayload);
            return area.IsSuccess ? Ok(area) : BadRequest(area);
        }

        [HttpPut]
        //[Route(ApiVer1UrlDefinition.Area.Update)]
        public async Task<ActionResult<GenericResult>> UpdateArea([FromBody] AreaDTO areaPayload)
        {
            var result = await _areaService.UpdateArea(areaPayload.ID, areaPayload);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpDelete("{id}")]
        //[Route(ApiVer1UrlDefinition.Area.Delete)]
        public async Task<ActionResult<GenericResult>> DeleteArea(int id)
        {
            var result = await _areaService.DeleteArea(id);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }
    }
}
