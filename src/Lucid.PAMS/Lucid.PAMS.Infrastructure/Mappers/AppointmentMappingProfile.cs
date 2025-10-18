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
    public class AppointmentMappingProfile : Profile
    {
        public AppointmentMappingProfile()
        {
            // Create your object-object mappings here
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<BookAppointmentDto,Appointment>().ReverseMap();
            CreateMap<UpdateAppointmentDto,Appointment>().ReverseMap();
        }
    }
}
