using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.DTOs.statistics;
using OfficeSupply.Data.Models.Requests;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.IServices
{
    public interface IDepartmentService
    {
        public Task<GenericResult> GetDepartment(int id);
        public Task<GenericResult> CreateDepartment(DepartmentPayload departmentPayload);
        public Task<GenericResult> UpdateDepartment(int id, DepartmentDTO departmentPayload);
        public Task<GenericResult> DeleteDepartment(int id);
        public Task<GenericResult> GetDepartments(DepartmentResourceParam resourceParams);
        List<DepartmentStatisticsDTO> GetDepartmentStatistics();
    }
}
