using AutoMapper;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Models.DTOs;
using OfficeSupply.Data.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API.Profiles
{
    public class PeriodProfile : Profile
    {
        public PeriodProfile()
        {
            CreateMap<Period, PeriodDTO>();
            CreateMap<PeriodPayload, Period>();
            CreateMap<PeriodUpdatePayload, Period>();
        }
    }
}
