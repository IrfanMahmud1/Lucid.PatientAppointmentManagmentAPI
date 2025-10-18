using AutoMapper;
using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucid.PAMS.Infrastructure.Mappers
{
    public class DoctorMapper : IDoctorMapper
    {
        private readonly IMapper _mapper;

        public DoctorMapper(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public DoctorDto MapToDto(Doctor patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));
            return _mapper.Map<DoctorDto>(patient);
        }

        public Doctor MapToEntity(DoctorDto patientDto)
        {
            if (patientDto == null) throw new ArgumentNullException(nameof(patientDto));
            return _mapper.Map<Doctor>(patientDto);
        }

        public Doctor MapFromCreateDto(CreateDoctorDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));
            return _mapper.Map<Doctor>(createDto);
        }

        public IEnumerable<DoctorDto> MapToDtos(IEnumerable<Doctor> patients) =>
            (patients ?? Enumerable.Empty<Doctor>()).Select(p => _mapper.Map<DoctorDto>(p));
    }
}
