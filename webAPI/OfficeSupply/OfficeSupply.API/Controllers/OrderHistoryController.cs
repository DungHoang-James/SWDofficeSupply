using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OfficeSupply.Business.IServices;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API.Controllers
{
    [Route("api/orderhistories")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {
        private readonly IOrderHistoryService _orderHistoryService;

        public OrderHistoryController(IOrderHistoryService orderHistoryService)
        {
            this._orderHistoryService = orderHistoryService;
        }

        [HttpGet]
        public ActionResult<OrderHistoryDTO> GetOrderHistories([FromQuery] ResourceParams resourceParams)
        {
            return Ok(_orderHistoryService.GetOrderHistories(resourceParams));
        }

        [HttpGet("{id}")]
        public ActionResult<OrderHistoryDTO> GetHistoryOrder(int id)
        {
            var orderHistoryDto = _orderHistoryService.GetOrderHistory(id);
            return orderHistoryDto != null ? Ok(orderHistoryDto) : NotFound(new { Message = "Not found order" });
        }

        [HttpPost]
        public ActionResult<OrderHistoryDTO> CreateOrderHistory([FromBody] OrderHistoryPayload orderHistoryPayload)
        {
            var orderHistoryDto = _orderHistoryService.CreateOrderHistory(orderHistoryPayload);
            return orderHistoryDto != null ? Ok(orderHistoryDto) : BadRequest(new { Message = "OrderHistory does not exist" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderHistoryPayload orderHistoryPayload)
        {
            var result = _orderHistoryService.UpdateOrderHistory(id, orderHistoryPayload);
            return result ? NoContent() : BadRequest(new { Message = "Order does not exist" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var result = _orderHistoryService.DeleteOrderHistory(id);
            return result ? NoContent() : BadRequest(new { Message = "Order does not exist" });
        }
    }
}
