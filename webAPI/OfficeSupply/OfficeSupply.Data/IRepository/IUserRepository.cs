using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUser(string email);
        void AddUser(User user);
        User GetUserByID(int id);
        Pagination<User> GetUsers(UserResourceParam resourceParams);
        Pagination<User> GetUsersByDepartmentId(int departmentId, DepartmentUserResourceParam resourceParams);
        User GetLeaderInDepartmentId(int departmentId);
        Pagination<User> GetUsersByCompanyId(int companyId, CompanyUserResourceParam resourceParams);
        User GetManagerInCompany(int companyId);
        List<User> GetUsersMin();
        //List<User> GetUsersIsVisitor();
    }
}
