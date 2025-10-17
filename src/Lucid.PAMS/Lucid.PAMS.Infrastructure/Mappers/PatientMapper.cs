using AutoMapper;
using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucid.PAMS.Infrastructure.Mappers
{
    public class PatientMapper : IPatientMapper
    {
        private readonly IMapper _mapper;

        public PatientMapper(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public PatientDto MapToDto(Patient patient)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));
            return _mapper.Map<PatientDto>(patient);
        }

        public Patient MapToEntity(PatientDto patientDto)
        {
            if (patientDto == null) throw new ArgumentNullException(nameof(patientDto));
            return _mapper.Map<Patient>(patientDto);
        }

        public Patient MapFromCreateDto(CreatePatientDto createDto)
        {
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));
            return _mapper.Map<Patient>(createDto);
        }

        public Patient MapFromUpdateDto(UpdatePatientDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));
            return _mapper.Map<Patient>(updateDto);
        }

        public IEnumerable<PatientDto> MapToDtos(IEnumerable<Patient> patients) =>
            (patients ?? Enumerable.Empty<Patient>()).Select(p => _mapper.Map<PatientDto>(p));
    }
}
