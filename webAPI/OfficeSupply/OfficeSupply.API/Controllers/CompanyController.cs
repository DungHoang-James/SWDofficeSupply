using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
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
    [ApiController]
    [Route(ApiVer1UrlDefinition.Company.Companies)]
    public class CompanyController : Controller
    {
        public readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            this._companyService = companyService;
        }

        [HttpGet]
        //[Route(ApiVer1UrlDefinition.Company.Get)]
        public async Task<ActionResult<GenericResult>> GetCompanies([FromQuery] CompanyResourceParam resourceParams)
        {
            var result = await _companyService.GetCompanies(resourceParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        //[Route(ApiVer1UrlDefinition.Company.GetDetail)]
        public async Task<ActionResult<GenericResult>> GetCompany(int id)
        {
            var company = await _companyService.GetCompany(id);
            return company.IsSuccess ? Ok(company) : NotFound(company);

        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<GenericResult>> GetUserOfCompany(int id, [FromQuery] CompanyUserResourceParam resourceParams)
        {
            var result = await _companyService.GetUserOfCompany(id, resourceParams);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        //[Route(ApiVer1UrlDefinition.Company.Add)]
        public async Task<ActionResult<GenericResult>> CreateCompany([FromBody] CompanyPayload companyPayload)
        {
            var company = await _companyService.CreateCompany(companyPayload);
            return company.IsSuccess ? Ok(company) : BadRequest(company);
        }

        [HttpPut]
        //[Route(ApiVer1UrlDefinition.Company.Update)]
        public async Task<ActionResult<GenericResult>> UpdateCompany([FromBody] CompanyDTO companyPayload)
        {
            var result = await _companyService.UpdateCompany(companyPayload.ID, companyPayload);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }

        [HttpDelete("{id}")]
        //[Route(ApiVer1UrlDefinition.Company.Delete)]
        public async Task<ActionResult<GenericResult>> DeleteCompany(int id)
        {
            var result = await _companyService.DeleteCompany(id);
            return result.IsSuccess ? NoContent() : BadRequest(result);
        }
    }
}
