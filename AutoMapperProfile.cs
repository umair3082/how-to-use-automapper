using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmpDTO, EmpEntity>().ReverseMap();
        }
    }
}
