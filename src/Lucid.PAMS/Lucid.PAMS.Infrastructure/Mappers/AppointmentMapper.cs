using AutoMapper;
using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucid.PAMS.Infrastructure.Mappers
{
    public class AppointmentMapper : IAppointmentMapper
    {
        private readonly IMapper _mapper;

        public AppointmentMapper(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public AppointmentDto MapToDto(Appointment patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));
            return _mapper.Map<AppointmentDto>(patient);
        }

        public Appointment MapToEntity(AppointmentDto patientDto)
        {
            if (patientDto == null) throw new ArgumentNullException(nameof(patientDto));
            return _mapper.Map<Appointment>(patientDto);
        }

        public Appointment MapFromBookDto(BookAppointmentDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));
            return _mapper.Map<Appointment>(createDto);
        }

        public Appointment MapFromUpdateDto(UpdateAppointmentDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));
            return _mapper.Map<Appointment>(updateDto);
        }

        public IEnumerable<AppointmentDto> MapToDtos(IEnumerable<Appointment> patients) =>
            (patients ?? Enumerable.Empty<Appointment>()).Select(p => _mapper.Map<AppointmentDto>(p));
    }
}
