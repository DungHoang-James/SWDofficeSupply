using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Services;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Models;
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
    [Route(ApiVer1UrlDefinition.Order.Orders)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GenericResult>> GetOrders([FromQuery] OrderResourceParam resourceParams)
        {
            var result = await _orderService.GetOrders(resourceParams);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        //[Route(ApiVer1UrlDefinition.Order.GetDetail)]
        public async Task<GenericResult> GetOrder(int id)
        {
            var result = await _orderService.GetOrder(id);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<GenericResult>> CreateOrder([FromBody] OrderPayload orderPayload)
        {
            var result = await _orderService.CreateOrder(orderPayload);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        //[Authorize(Roles = "2,3")]
        public async Task<ActionResult<GenericResult>> UpdateOrder([FromBody] OrderUpdatePayload orderPayload)
        {
            var result = await _orderService.UpdateOrder(orderPayload);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpDelete("{id}")]
        //[Route(ApiVer1UrlDefinition.Order.Delete)]
        public async Task<GenericResult> DeleteOrder(int id)
        {

            var result = await _orderService.DeleteOrder(id);
            return result;
        }
    }
}
