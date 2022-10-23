using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Helper;
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
    //[Authorize]
    [Route(ApiVer1UrlDefinition.Product.Products)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        //[Route(ApiVer1UrlDefinition.Product.Get)]
        public async Task<ActionResult<GenericResult>> GetProducts([FromQuery] ProductResourceParam resourceParams)
        {
            var result = await _productService.GetProducts(resourceParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        //[Route(ApiVer1UrlDefinition.Product.GetDetail)]
        public async Task<ActionResult<GenericResult>> GetProduct(int id)
        {
            var result = await _productService.GetProduct(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        //[Route(ApiVer1UrlDefinition.Product.Add)]
        public async Task<ActionResult<GenericResult>> CreateProduct([FromBody] ProductPayload productPayload)
        {
            var result = await _productService.CreateProduct(productPayload);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        //[Route(ApiVer1UrlDefinition.Product.Update)]
        public async Task<ActionResult<GenericResult>> UpdateProduct([FromBody] ProductUpdatePayload productPayload)
        {
            var result = await _productService.UpdateProduct(productPayload.ID, productPayload);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpDelete("{id}")]
        //[Route(ApiVer1UrlDefinition.Product.Delete)]
        public async Task<ActionResult<GenericResult>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }
    }
}
