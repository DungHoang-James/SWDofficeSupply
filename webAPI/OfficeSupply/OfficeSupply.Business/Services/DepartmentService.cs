using AutoMapper;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository,
                                 IUserRepository userRepository,
                                 IMapper mapper)
        {
            this._departmentRepository = departmentRepository;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetDepartments(DepartmentResourceParam resourceParams)
        {
            if (resourceParams.All)
            {
                var userInfo = _userRepository.GetUserByID(resourceParams.UserID);
                var departments = _departmentRepository.GetAllDepartmentsOfCompany(userInfo.Department.CompanyID);
                return ApiResponse.Ok(_mapper.Map<List<DepartmentDTO>>(departments));
            }

            var user = _userRepository.GetUserByID(resourceParams.UserID);
            var pageList = _departmentRepository.GetDepartmentsByCompanyId(user.Department.CompanyID, resourceParams);

            Pagination<DepartmentDTO> result = new(_mapper.Map<List<DepartmentDTO>>(pageList.Items),
                                                   pageList.TotalCount,
                                                   pageList.CurrentPage,
                                                   pageList.PageSize);

            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> GetDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }

            return ApiResponse.Ok(_mapper.Map<DepartmentDTO>(department));
        }

        public async Task<GenericResult> CreateDepartment(DepartmentPayload departmentPayload)
        {
            var department = _mapper.Map<Department>(departmentPayload);
            var result = _departmentRepository.Create(department);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create Department fail");
            }
            return ApiResponse.Ok(_mapper.Map<DepartmentDTO>(department));
        }

        public async Task<GenericResult> UpdateDepartment(int id, DepartmentDTO departmentPayload)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            _mapper.Map(departmentPayload, department);
            var result = _departmentRepository.Update(department);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Update Department fail");
            }
            return ApiResponse.Ok();
        }

        public async Task<GenericResult> DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            department.IsDelete = true;
            var result = _departmentRepository.Update(department); if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Delete Department fail");
            }
            return ApiResponse.Ok();
        }

        public List<DepartmentStatisticsDTO> GetDepartmentStatistics()
        {
            return _departmentRepository.GetDepartmentStatistics();
        }
    }
}
