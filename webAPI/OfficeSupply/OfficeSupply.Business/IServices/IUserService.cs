using OfficeSupply.Business.Ultility.common;
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
    public interface IUserService
    {
        Task<GenericResult> GetUsers(UserResourceParam resourceParams);
        Task<GenericResult> GetUser(int id);
        Task<GenericResult> GetUsersByDepartmentId(int departmentId, DepartmentUserResourceParam resourceParams);
        Task<GenericResult> UpdateUser(UserPayload userDTO);
        Task<GenericResult> UpdateUserInfo(int id, UserUpdatePayload updatePayload);
    }
}
