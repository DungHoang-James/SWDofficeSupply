using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository,
                              IUserRepository userRepository,
                              IDepartmentRepository departmentRepository,
                              IMapper mapper)
        {
            this._companyRepository = companyRepository;
            this._userRepository = userRepository;
            this._departmentRepository = departmentRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetCompanies(CompanyResourceParam resourceParams)
        {
            var companies = _companyRepository.GetCompanies(resourceParams);
            Pagination<CompanyDTO> result = new(_mapper.Map<List<CompanyDTO>>(companies.Items),
                                            companies.TotalCount,
                                            companies.CurrentPage,
                                            companies.PageSize);
            return ApiResponse.Ok(resData: result, totalCount: companies.TotalCount);
        }

        public async Task<GenericResult> GetCompany(int id)
        {
            var company = _companyRepository.GetById(id);

            if (company == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }
            var result = _mapper.Map<CompanyDTO>(company);

            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> CreateCompany(CompanyPayload companyPayload)
        {
            var company = _mapper.Map<Company>(companyPayload);
            var result = _companyRepository.Create(company);
            if (!result)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Add Company fail");
            }

            Department department = new Department() { Name = "Manager", CompanyID = company.ID, IsDelete = false };
            _departmentRepository.Create(department);

            return ApiResponse.Ok(_mapper.Map<CompanyDTO>(company));
        }

        public async Task<GenericResult> UpdateCompany(int id, CompanyDTO companyPayload)
        {
            var company = _companyRepository.GetById(id);
            if (company == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);

            User oldManager = null;
            if (company.ManagerID != null)
            {
                oldManager = _userRepository.GetById((int)company.ManagerID);
            }

            _mapper.Map(companyPayload, company);
            var result = _companyRepository.Update(company);
            if (!result)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Update fail");
            }

            var dep = _departmentRepository.GetFirstDepartmentByCompanyId(company.ID);

            var manager = _userRepository.GetById((int)company.ManagerID);
            manager.RoleID = RoleBase.MANAGER;
            manager.DepartmentID = dep.ID;

            _userRepository.Update(manager);

            if (oldManager != null)
            {
                oldManager.RoleID = RoleBase.EMPLOYEE;
                _userRepository.Update(oldManager);
            }

            return ApiResponse.Ok();
        }

        public async Task<GenericResult> DeleteCompany(int id)
        {
            var company = _companyRepository.GetById(id);
            if (company == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            company.IsDelete = true;
            var result = _companyRepository.Update(company);
            if (!result)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "delete fail");
            }
            return ApiResponse.Ok();
        }

        public async Task<GenericResult> GetUserOfCompany(int companyId, CompanyUserResourceParam resourceParams)
        {
            var user = _userRepository.GetUsersByCompanyId(companyId, resourceParams);
            Pagination<UserDTO> result = new Pagination<UserDTO>(_mapper.Map<List<UserDTO>>(user.Items),
                                                                 user.TotalCount,
                                                                 user.CurrentPage,
                                                                 user.PageSize);

            return ApiResponse.Ok(resData: result);
        }
    }
}
