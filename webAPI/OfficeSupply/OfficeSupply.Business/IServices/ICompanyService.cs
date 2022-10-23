using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface ICompanyService
    {
        public Task<GenericResult> GetCompany(int id);
        public Task<GenericResult> CreateCompany(CompanyPayload companyPayload);
        public Task<GenericResult> UpdateCompany(int id, CompanyDTO companyPayload);
        public Task<GenericResult> DeleteCompany(int id);
        public Task<GenericResult> GetCompanies(CompanyResourceParam resourceParams);
        Task<GenericResult> GetUserOfCompany(int companyId, CompanyUserResourceParam resourceParams);
    }
}
