using AutoMapper;
using OfficeSupply.Business.IServices;
using OfficeSupply.Business.Ultility.common;
using OfficeSupply.Business.Utility.common;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Helper;
using OfficeSupply.Data.IRepository;
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
    public class PeriodService : IPeriodService
    {
        private readonly IPeriodRepository _periodRepository;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICompanyRepository _companyRepository;

        public PeriodService(IPeriodRepository periodRepository,
                             IMapper mapper,
                             IDepartmentRepository departmentRepository,
                             ICompanyRepository companyRepository)
        {
            this._periodRepository = periodRepository;
            this._mapper = mapper;
            this._departmentRepository = departmentRepository;
            this._companyRepository = companyRepository;
        }

        public async Task<GenericResult> CreatePeriod(PeriodPayload periodPayload)
        {
            ExpiredPeriod(periodPayload.DepartmentID);

            //var now = DateTime.Now;
            //if (periodPayload.FromTime < now || periodPayload.ToTime < now)
            //{
            //    return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create Period fail, FromTime or ToTime invalid");
            //}

            if (periodPayload.FromTime > periodPayload.ToTime)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create Period fail, From Time must less than To Time");
            }

            var existPeriods = _periodRepository.GetPeriodAtTime(periodPayload.FromTime, periodPayload.ToTime, periodPayload.DepartmentID);

            if (existPeriods.Count != 0)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Time period coincides with other time periods");
            }

            var period = _mapper.Map<Period>(periodPayload);
            period.RemainingQuota = period.Quota;

            var result = _periodRepository.Create(period);

            if (result == false)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create Period fail");
            }

            var department = _departmentRepository.GetDepartmentByID(periodPayload.DepartmentID);
            var company = department.Company;
            company.Wallet -= period.Quota;

            _companyRepository.Update(company);

            return ApiResponse.Ok(resData: _mapper.Map<PeriodDTO>(period));
        }

        public async Task<GenericResult> UpdatePeriod(PeriodUpdatePayload periodUpdate)
        {
            if (periodUpdate.FromTime > periodUpdate.ToTime)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Create Period fail, From Time must less than To Time");
            }

            var existPeriods = _periodRepository.GetPeriodAtTime(periodUpdate.FromTime, periodUpdate.ToTime, periodUpdate.DepartmentID);

            if (existPeriods.Count != 0)
            {
                foreach (var e in existPeriods)
                {
                    if (e.ID != periodUpdate.ID)
                    {
                        return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound, messDetail: "Time period coincides with other time periods");
                    }
                }
            }

            if (periodUpdate.ResetQuota)
            {
                var department = _departmentRepository.GetDepartmentByIDMin(periodUpdate.DepartmentID);
                var companyId = department.CompanyID;
                var company = _companyRepository.GetCompanyByIDMin(companyId);

                var period = _periodRepository.GetById(periodUpdate.ID);

                _mapper.Map(periodUpdate, period);

                company.Wallet += period.RemainingQuota;
                company.Wallet -= period.Quota;

                period.RemainingQuota = period.Quota;

                var result = _periodRepository.Update(period);

                if (result == false)
                {
                    return ApiResponse.Error(messDetail: "Update period fail");
                }

                _companyRepository.Update(company);

                return ApiResponse.Ok(period);
            }
            else
            {
                var period = _periodRepository.GetById(periodUpdate.ID);

                _mapper.Map(periodUpdate, period);

                var result = _periodRepository.Update(period);

                if (result == false)
                {
                    return ApiResponse.Error(messDetail: "Update period fail");
                }

                return ApiResponse.Ok(period);
            }
        }

        public async Task<GenericResult> GetPeriodOfCompany(PeriodResourceParam resourceParam)
        {
            var periods = _periodRepository.GetPeriodsOfCompany(resourceParam);

            var result = new Pagination<PeriodDTO>(_mapper.Map<List<PeriodDTO>>(periods.Items),
                                                   periods.TotalCount,
                                                   periods.CurrentPage,
                                                   periods.PageSize);

            return ApiResponse.Ok(resData: result, totalCount: result.TotalCount);
        }

        public async Task<GenericResult> GetCurrentPeriod(int departmentId)
        {
            ExpiredPeriod(departmentId);

            var period = _periodRepository.GetCurrentPeriod(departmentId);

            if (period == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }

            return ApiResponse.Ok(_mapper.Map<PeriodDTO>(period));
        }

        public async Task<GenericResult> DeletePeriod(int id)
        {
            var period = _periodRepository.GetById(id);
            if (period == null)
            {
                return ApiResponse.Error(messCode: OfficeException.CommonException.RecordNotFound);
            }

            period.IsExpired = true;
            var result = _periodRepository.Update(period);

            if (result == false)
            {
                return ApiResponse.Error(messDetail: "Delete period fail");
            }

            return ApiResponse.Ok();
        }

        private void ExpiredPeriod(int departmentId)
        {
            var periods = _periodRepository.GetPeriodsNotExpired(departmentId);
            foreach (var p in periods)
            {
                p.IsExpired = true;
                _periodRepository.Update(p);
            }
        }
    }
}
