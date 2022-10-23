using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.ResourceParams;
using OfficeSupply.Data.PropertyMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public UserRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this._propertyMappingService = propertyMappingService;
        }

        public void AddUser(User user)
        {
            _officeSupplyDB.Users.Add(user);
            _officeSupplyDB.SaveChanges();
        }

        public User GetUser(string email)
        {
            return _officeSupplyDB.Users.FirstOrDefault(u => u.Email.Equals(email));
        }

        public User GetUserByID(int id)
        {
            return _officeSupplyDB.Users.Include(u => u.Department).FirstOrDefault(u => u.ID == id);
        }

        public Pagination<User> GetUsers(UserResourceParam resourceParams)
        {
            var userPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<UserDTO, User>();

            var userQuery = _officeSupplyDB.Users.Where(u => (u.Firstname.Contains(resourceParams.Name) || u.Lastname.Contains(resourceParams.Name)) &&
                                                        (u.RoleID == RoleBase.VISITOR ||
                                                         u.RoleID == RoleBase.EMPLOYEE ||
                                                         u.RoleID == RoleBase.LEADER ||
                                                         u.RoleID == RoleBase.MANAGER));
            userQuery = userQuery.ApplySort(resourceParams.OrderBy, userPropertyMappingDictionary);

            var pageList = Pagination<User>.Create(userQuery, resourceParams.PageNumber, resourceParams.PageSize);

            return pageList;
        }

        public Pagination<User> GetUsersByDepartmentId(int departmentId, DepartmentUserResourceParam resourceParams)
        {
            var userPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<UserDTO, User>();

            var userQuery = _officeSupplyDB.Users.Where(u => u.DepartmentID == departmentId &&
                                                        (u.Firstname.Contains(resourceParams.Name) ||
                                                        u.Lastname.Contains(resourceParams.Name)));
            userQuery = userQuery.ApplySort(resourceParams.OrderBy, userPropertyMappingDictionary);

            var pageList = Pagination<User>.Create(userQuery, resourceParams.PageNumber, resourceParams.PageSize);

            return pageList;
        }

        public Pagination<User> GetUsersByCompanyId(int companyId, CompanyUserResourceParam resourceParams)
        {
            var userPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<UserDTO, User>();

            var userQuery = _officeSupplyDB.Users.Include(u => u.Department)
                                                 .Where(u => u.Department.CompanyID == companyId &&
                                                             (u.Firstname.Contains(resourceParams.Name) ||
                                                             u.Lastname.Contains(resourceParams.Name)));
            userQuery = userQuery.ApplySort(resourceParams.OrderBy, userPropertyMappingDictionary);

            var pageList = Pagination<User>.Create(userQuery, resourceParams.PageNumber, resourceParams.PageSize);

            return pageList;
        }

        public User GetLeaderInDepartmentId(int departmentId)
        {
            return _officeSupplyDB.Users.Include(u => u.Department).ThenInclude(d => d.Company)
                                        .FirstOrDefault(u => u.DepartmentID == departmentId && u.IsDelete == false && u.RoleID == RoleBase.LEADER);
        }

        public User GetManagerInCompany(int companyId)
        {
            return _officeSupplyDB.Users.Include(u => u.Department)
                                        .ThenInclude(d => d.Company)
                                        .FirstOrDefault(u => u.Department.CompanyID == companyId && u.IsDelete == false && u.RoleID == RoleBase.MANAGER);
        }

        public List<User> GetUsersMin()
        {
            return _officeSupplyDB.Users.Where(u => u.IsDelete == false && u.RoleID == RoleBase.VISITOR).ToList();
        }

        //public List<User> GetUsersIsVisitor()
        //{
        //    return _officeSupplyDB.Users.Where(u => u.RoleID == RoleBase.VISITOR && u.IsDelete == false).ToList();
        //}
    }
}
