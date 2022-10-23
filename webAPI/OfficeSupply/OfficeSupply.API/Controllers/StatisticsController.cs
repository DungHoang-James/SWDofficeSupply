using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
using OfficeSupply.Data.Models.DTOs.statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API.Controllers
{
    [ApiController]
    [Route("api/v1/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IDepartmentService _departmentService;
        private readonly IAreaService _areaService;

        public StatisticsController(ICategoryService categoryService,
                                    IDepartmentService departmentService,
                                    IAreaService areaService)
        {
            this._categoryService = categoryService;
            this._departmentService = departmentService;
            this._areaService = areaService;
        }

        [HttpGet("categories")]
        public ActionResult<List<CategoryStatisticsDTO>> GetProductInCategory()
        {
            var categoryStatistics = _categoryService.GetCategoryStatistics();
            return Ok(categoryStatistics);
        }

        [HttpGet("departments")]
        public ActionResult<List<DepartmentStatisticsDTO>> GetEmpInDepartment()
        {
            var departmentStatistics = _departmentService.GetDepartmentStatistics();
            return Ok(departmentStatistics);
        }

        [HttpGet("areas")]
        public ActionResult<List<AreaStatisticsDTO>> GetCompanyInArea()
        {
            var areaStatistics = _areaService.GetAreaStatistics();
            return Ok(areaStatistics);
        }

        [HttpGet("menus")]
        public ActionResult<List<MenuStatisticsDTO>> GetProductInMenu()
        {
            return Ok();
        }
    }
}
