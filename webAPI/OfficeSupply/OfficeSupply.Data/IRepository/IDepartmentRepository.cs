using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.IRepository
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        List<Department> GetAllDepartmentsOfCompany(int companyId);
        Department GetDepartmentByID(int id);
        Pagination<Department> GetDepartmentsByCompanyId(int companyId, DepartmentResourceParam resourceParams);
        Department GetDepartmentByIDMin(int id);
        List<DepartmentStatisticsDTO> GetDepartmentStatistics();
        Department GetFirstDepartmentByCompanyId(int companyId);
    }
}
