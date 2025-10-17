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
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            // Create your object-object mappings here
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<CreatePatientDto,Patient>().ReverseMap();
            CreateMap<UpdatePatientDto,Patient>().ReverseMap();
        }
    }
}
