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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UserPayload, User>();
            CreateMap<UserUpdatePayload, User>();
            CreateMap<User, UserMinDTO>();
        }
    }
}
