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
    [Route(ApiVer1UrlDefinition.Supplier.Suppliers)]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this._supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<GenericResult>> GetSuppliers([FromQuery] SupplierResourceParam resourceParams)
        {
            var result = await _supplierService.GetSuppliers(resourceParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResult>> GetSupplier(int id)
        {
            var result = await _supplierService.GetSupplier(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResult>> CreateSupplier([FromBody] SupplierPayload supplierPayload)
        {
            var result = await _supplierService.CreateSupplier(supplierPayload);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<GenericResult>> UpdateSupplier([FromBody] SupplierDTO supplierPayload)
        {
            var result = await _supplierService.UpdateSupplier(supplierPayload.ID, supplierPayload);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResult>> DeleteSupplier(int id)
        {
            var result = await _supplierService.DeleteSupplier(id);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }
    }
}
