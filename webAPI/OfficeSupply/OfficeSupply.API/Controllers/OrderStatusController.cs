using Microsoft.AspNetCore.Mvc;

using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Services;
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
    [Route("api/orderstatuses")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;

        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            this._orderStatusService = orderStatusService;
}
        [HttpGet]
        public ActionResult<OrderStatusDTO> GetOrderStatuses([FromQuery] ResourceParams resourceParams)
        {
            return Ok(_orderStatusService.GetOrderStatuses(resourceParams));
        }

        [HttpGet("{id}")]
        public ActionResult<OrderStatusDTO> GetOrderStatus(int id)
        {
            var orderStatusDto = _orderStatusService.GetOrderStatus(id);
            return orderStatusDto != null ? Ok(orderStatusDto) : NotFound(new { Message = "Not found OrderStatus" });
        }

        [HttpPost]
        public ActionResult<OrderStatusDTO> CreateOrderStatus([FromBody] OrderStatusPayload orderStatusPayload)
{
            var orderStatusDto = _orderStatusService.CreateOrderStatus(orderStatusPayload);
            return orderStatusDto != null ? Ok(orderStatusDto) : BadRequest(new { Message = "OrderStatus is unvalid" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] OrderStatusPayload orderStatusPayload)
        {
            var result = _orderStatusService.UpdateOrderStatus(id, orderStatusPayload);
            return result ? NoContent() : BadRequest(new { Message = "OrderStatus does not exist" });
        }

    }
}
