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
    [Route(ApiVer1UrlDefinition.Menu.Menus)]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            this._menuService = menuService;
        }

        [HttpGet]
        //[Route(ApiVer1UrlDefinition.Menu.Get)]
        public async Task<ActionResult<GenericResult>> GetMenus([FromQuery] MenuResourceParam resourceParams)
        {
            var result = await _menuService.GetMenus(resourceParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        //[Route(ApiVer1UrlDefinition.Menu.GetDetail)]
        public async Task<ActionResult<GenericResult>> GetMenu(int id)
        {
            var menuDto = await _menuService.GetMenu(id);

            return menuDto.IsSuccess ? Ok(menuDto) : NotFound(menuDto);
        }

        [HttpPost]
        //[Route(ApiVer1UrlDefinition.Menu.Add)]
        public async Task<ActionResult<GenericResult>> AddMenu([FromBody] MenuPayload menuPayload)
        {
            var menu = await _menuService.AddMenu(menuPayload);
            return menu.IsSuccess ? Ok(menu) : BadRequest(menu);
        }

        [HttpPut]
        //[Route(ApiVer1UrlDefinition.Menu.Update)]
        public async Task<ActionResult<GenericResult>> UpdateMenu([FromBody] MenuDTO menuPayload)
        {
            var result = await _menuService.UpdateMenu(menuPayload.ID, menuPayload);

            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpDelete("{id}")]
        //[Route(ApiVer1UrlDefinition.Menu.Delete)]
        public async Task<ActionResult<GenericResult>> DeleteMenu(int id)
        {
            var result = await _menuService.DeleteMenu(id);

            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpPost("{id}/products")]
        public async Task<ActionResult<GenericResult>> AddProductToMenu(int id, [FromBody] ProductInMenuPayload productInMenuPayload)
        {
            var result = await _menuService.AddProductInMenu(id, productInMenuPayload);
            return result.IsSuccess ? Ok() : BadRequest(result);
        }

        [HttpPut("{id}/products")]
        public async Task<ActionResult<GenericResult>> UpdateProductInMenu(int id, [FromBody] ProductInMenuUpdatePayload productInMenuPayload)
        {
            var result = await _menuService.UpdateProductInMenu(id, productInMenuPayload);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<GenericResult>> GetProductInMenu(int id, [FromQuery] ProductInMenuResourceParam resourceParams)
        {
            var result = await _menuService.GetProductInMenu(id, resourceParams);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
