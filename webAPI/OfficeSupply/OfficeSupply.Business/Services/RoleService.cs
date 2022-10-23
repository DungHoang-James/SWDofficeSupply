using AutoMapper;

using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.IRepository;
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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<GenericResult> CreateRole(RolePayload rolePayload)
        {
            var role = _mapper.Map<Role>(rolePayload);
            var result = _roleRepository.Create(role);
            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create product fail");
            }
            return ApiResponse.Ok(_mapper.Map<RoleDTO>(role));
        }

        public async Task<GenericResult> GetRole(int id)
        {
            var role = _roleRepository.GetById(id);
            if(role == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }
            var result = _mapper.Map<RoleDTO>(role);
            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> GetRoles(ResourceParams resourceParams)
        {
            var result = _mapper.Map<List<RoleDTO>>(_roleRepository.GetRoles(resourceParams));
            return ApiResponse.Ok(result);
        }

        public async Task<GenericResult> UpdateRole(int id, RolePayload rolePayload)
        {
            var role = _roleRepository.GetById(id);
            if (role == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            _mapper.Map(rolePayload, role);
            var result = _roleRepository.Update(role);
            if(result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound,messDetail: "Update role fail");
            }
            return ApiResponse.Ok();
        }


    }
}
