using AutoMapper;
using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Mapper
{
    public class MappingProfile : Profile
    {
       
   
        public MappingProfile()
        {
            CreateMap(typeof(Hall) , typeof(HallViewModel));
        }
    }
}
