using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Helper;
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
    //[Authorize]
    [Route(ApiVer1UrlDefinition.Category.Categories)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        //[Route(ApiVer1UrlDefinition.Category.Get)]
        public async Task<ActionResult<GenericResult>> GetCategories([FromQuery] CategoryResourceParam resourceParams)
        {
            var result = await _categoryService.GetCategories(resourceParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        //[Route(ApiVer1UrlDefinition.Category.GetDetail)]
        public async Task<ActionResult<GenericResult>> GetCategory(int id)
        {
            var result = await _categoryService.GetCategory(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        //[Route(ApiVer1UrlDefinition.Category.Add)]
        public async Task<ActionResult<GenericResult>> AddCategory([FromBody] CategoryPayload categoryPayload)
        {
            var category = await _categoryService.AddCategory(categoryPayload);
            return Ok(category);
        }

        [HttpPut]
        //[Route(ApiVer1UrlDefinition.Category.Update)]
        public async Task<ActionResult<GenericResult>> UpdateCategory([FromBody] CategoryDTO categoryPayload)
        {
            var result = await _categoryService.UpdateCategory(categoryPayload.ID, categoryPayload);

            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpDelete("{id}")]
        //[Route(ApiVer1UrlDefinition.Category.Delete)]
        public async Task<ActionResult<GenericResult>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return NoContent();
        }
    }
}
