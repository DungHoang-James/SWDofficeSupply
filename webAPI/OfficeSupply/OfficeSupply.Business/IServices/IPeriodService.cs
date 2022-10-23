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
    public interface IPeriodService
    {
        Task<GenericResult> CreatePeriod(PeriodPayload periodPayload);
        Task<GenericResult> GetCurrentPeriod(int departmentId);
        Task<GenericResult> DeletePeriod(int id);
        Task<GenericResult> GetPeriodOfCompany(PeriodResourceParam resourceParam);
        Task<GenericResult> UpdatePeriod(PeriodUpdatePayload periodUpdate);
    }
}
