using AutoMapper;
using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Infrastructure.Mappers
{
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            // Create your object-object mappings here
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<CreateDoctorDto,Doctor>().ReverseMap();
        }
    }
}
