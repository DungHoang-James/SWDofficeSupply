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
    [Route(ApiVer1UrlDefinition.Period.Periods)]
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodService _periodService;

        public PeriodController(IPeriodService periodService)
        {
            this._periodService = periodService;
        }

        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<GenericResult>> GetPeriodOfDepartment(int departmentId)
        {
            var period = await _periodService.GetCurrentPeriod(departmentId);
            return period.IsSuccess ? Ok(period) : NotFound(period);
        }

        [HttpGet]
        public async Task<ActionResult<GenericResult>> GetPeriod([FromQuery] PeriodResourceParam resourceParam)
        {
            var result = await _periodService.GetPeriodOfCompany(resourceParam);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResult>> CreatePeriod([FromBody] PeriodPayload periodPayload)
        {
            var result = await _periodService.CreatePeriod(periodPayload);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResult>> DeletePeriod(int id)
        {
            var result = await _periodService.DeletePeriod(id);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult<GenericResult>> UpdatePeriod([FromBody] PeriodUpdatePayload periodUpdate)
        {
            var result = await _periodService.UpdatePeriod(periodUpdate);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
