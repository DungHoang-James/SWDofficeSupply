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
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<SupplierPayload, Supplier>();
            CreateMap<SupplierDTO, Supplier>();
            CreateMap<Supplier, SupplierMinDTO>();
        }
    }
}
