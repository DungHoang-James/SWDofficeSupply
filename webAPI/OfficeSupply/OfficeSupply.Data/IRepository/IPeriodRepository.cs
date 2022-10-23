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
    public interface IPeriodRepository : IBaseRepository<Period>
    {
        List<Period> GetPeriodAtTime(DateTime fromTime, DateTime toTime, int departmentId);
        Period GetCurrentPeriod(int departmentId);
        List<Period> GetPeriodsNotExpired(int departmentId);
        Pagination<Period> GetPeriodsOfCompany(PeriodResourceParam resourceParam);
    }
}
