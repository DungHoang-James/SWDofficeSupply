using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API.Controllers
{
    [ApiController]
    [Route(ApiVer1UrlDefinition.OrderDetail.OrderDetails)]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            this._orderDetailService = orderDetailService;
        }

        [HttpGet("{orderID}")]
        public async Task<ActionResult<GenericResult>> GetOrderDetail(int orderID)
        {
            var result = await _orderDetailService.GetOrderDetailsByOrderID(orderID);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
