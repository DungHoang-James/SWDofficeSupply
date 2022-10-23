using Microsoft.EntityFrameworkCore;
using OfficeSupply.Data.DatabaseContext;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.ResourceParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.Repository
{
    public class PeriodRepository : BaseRepository<Period>, IPeriodRepository
    {
        private readonly OfficeSupplyDB _officeSupplyDB;

        public PeriodRepository(OfficeSupplyDB officeSupplyDB) : base(officeSupplyDB)
        {
            this._officeSupplyDB = officeSupplyDB;
        }

        public List<Period> GetPeriodAtTime(DateTime fromTime, DateTime toTime, int departmentId)
        {
            return _officeSupplyDB.Periods.Where(p => (p.DepartmentID == departmentId) &&
                                                      ((p.FromTime <= fromTime && p.ToTime >= toTime) ||
                                                      (p.FromTime <= fromTime && p.ToTime > fromTime) ||
                                                      (p.ToTime >= toTime && p.FromTime < toTime)) &&
                                                      (p.IsExpired == false)).ToList();
        }

        public Period GetCurrentPeriod(int departmentId)
        {
            var now = DateTime.Now;
            return _officeSupplyDB.Periods.FirstOrDefault(p => p.DepartmentID == departmentId &&
                                                               (p.FromTime <= now && now <= p.ToTime) &&
                                                               p.IsExpired == false);
        }

        public List<Period> GetPeriodsNotExpired(int departmentId)
        {
            var now = DateTime.Now;
            return _officeSupplyDB.Periods.Where(p => p.DepartmentID == departmentId && p.IsExpired == false && p.ToTime < now).ToList();
        }

        public Pagination<Period> GetPeriodsOfCompany(PeriodResourceParam resourceParam)
        {
            var periodQuery = _officeSupplyDB.Periods.Include(p => p.Department).Where(p => p.Department.CompanyID == resourceParam.CompanyID);
            if (resourceParam.DepartmentID != null)
            {
                periodQuery = periodQuery.Where(p => p.DepartmentID == resourceParam.DepartmentID);
            }

            var pageList = Pagination<Period>.Create(periodQuery, resourceParam.PageNumber, resourceParam.PageSize);
            return pageList;
        }
    }
}
