using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using OfficeSupply.Data.PropertyMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;
        private readonly IPropertyMappingService _propertyMappingService;

        public DepartmentRepository(OfficeSupplyDB officeSupplyDB, IPropertyMappingService propertyMappingService) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
            this._propertyMappingService = propertyMappingService;
        }

        public List<Department> GetAllDepartmentsOfCompany(int companyId)
        {
            return _officeSupplyDB.Departments.Where(d => d.CompanyID == companyId).ToList();
        }

        public Pagination<Department> GetDepartmentsByCompanyId(int companyId, DepartmentResourceParam resourceParams)
        {
            var departmentPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<DepartmentDTO, Department>();
            var departmentQuery = _officeSupplyDB.Departments.Where(d => d.CompanyID == companyId && d.Name.Contains(resourceParams.Name));

            departmentQuery = departmentQuery.ApplySort(resourceParams.OrderBy, departmentPropertyMappingDictionary);

            var pageList = Pagination<Department>.Create(departmentQuery, resourceParams.PageNumber, resourceParams.PageSize);
            return pageList;
        }

        public Department GetDepartmentByID(int id)
        {
            var department = _officeSupplyDB.Departments.Where(d => d.ID == id).Include(d => d.Company)
                                                          .ThenInclude(c => c.Area)
                                                          .FirstOrDefault();

            return department;
        }

        public Department GetDepartmentByIDMin(int id)
        {
            return _officeSupplyDB.Departments.AsNoTracking().FirstOrDefault(d => d.ID == id);
        }

        public List<DepartmentStatisticsDTO> GetDepartmentStatistics()
        {
            return _officeSupplyDB.Departments.Include(d => d.Users)
                                              .Select(d => new DepartmentStatisticsDTO { ID = d.ID, NumOfEmp = d.Users.Count }).ToList();
        }

        public Department GetFirstDepartmentByCompanyId(int companyId)
        {
            return _officeSupplyDB.Departments.AsNoTracking().FirstOrDefault(d => d.CompanyID == companyId);
        }
    }
}
