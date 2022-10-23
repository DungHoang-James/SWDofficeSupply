using AutoMapper;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
                           IDepartmentRepository departmentRepository,
                           IMapper mapper)
        {
            this._userRepository = userRepository;
            this._departmentRepository = departmentRepository;
            this._mapper = mapper;
        }

        public async Task<GenericResult> GetUsers(UserResourceParam resourceParams)
        {
            if (resourceParams.All)
            {
                var usersMin = _userRepository.GetUsersMin();

                return ApiResponse.Ok(resData: _mapper.Map<List<UserMinDTO>>(usersMin));
            }

            var users = _userRepository.GetUsers(resourceParams);

            Pagination<UserDTO> result = new(_mapper.Map<List<UserDTO>>(users.Items),
                           users.TotalCount,
                           users.CurrentPage,
                           users.PageSize);


            return ApiResponse.Ok(resData: result, totalCount: users.TotalCount);
        }

        public async Task<GenericResult> GetUser(int id)
        {
            var user = _userRepository.GetUserByID(id);
            if (user == null) return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);

            var userDto = _mapper.Map<UserDTO>(user);

            if (user.DepartmentID != null)
            {
                var department = _departmentRepository.GetDepartmentByID((int)user.DepartmentID);
                userDto.MenuID = department.Company.Area.MenuID;
                userDto.CompanyID = department.CompanyID;
            }

            return ApiResponse.Ok(userDto);
        }

        public async Task<GenericResult> GetUsersByDepartmentId(int departmentId, DepartmentUserResourceParam resourceParams)
        {
            var users = _userRepository.GetUsersByDepartmentId(departmentId, resourceParams);

            Pagination<UserDTO> result = new(_mapper.Map<List<UserDTO>>(users.Items),
                                             users.TotalCount,
                                             users.CurrentPage,
                                             users.PageSize);

            return ApiResponse.Ok(resData: result, totalCount: result.TotalCount);
        }

        public async Task<GenericResult> UpdateUser(UserPayload userDTO)
        {
            var user = _userRepository.GetUser(userDTO.Email);
            if (user == null)
            {
                return ApiResponse.Error(messDetail: "Email not exist in system");
            }

            if (userDTO.RoleID == RoleBase.LEADER)
            {
                var leader = _userRepository.GetLeaderInDepartmentId((int)userDTO.DepartmentID);
                if (leader != null)
                {
                    return ApiResponse.Error(messDetail: "This office already has a leader");
                }
            }

            _mapper.Map(userDTO, user);
            var result = _userRepository.Update(user);
            return result ? ApiResponse.Ok() : ApiResponse.Error(messDetail: "Update user fail");
        }

        public async Task<GenericResult> UpdateUserInfo(int id, UserUpdatePayload updatePayload)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return ApiResponse.Error(messDetail: "User not exist in system");
            }

            _mapper.Map(updatePayload, user);
            var result = _userRepository.Update(user);
            return result ? ApiResponse.Ok() : ApiResponse.Error(messDetail: "Update user fail");
        }
    }
}
