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
    public class AreaProfile : Profile
    {
        public AreaProfile()
        {
            CreateMap<Area, AreaDTO>();
            CreateMap<AreaPayload, Area>();
            CreateMap<AreaDTO, Area>();
            CreateMap<AreaPayload, AreaDTO>();
            CreateMap<Area, AreaMinDTO>();
        }
    }
}
