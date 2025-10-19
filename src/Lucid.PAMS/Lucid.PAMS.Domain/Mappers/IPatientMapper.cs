using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Mappers
{
    public interface IPatientMapper
    {
        public PatientDto MapToDto(Patient patient);
        public Patient MapToEntity(PatientDto patientDto);
        public Patient MapFromCreateDto(CreatePatientDto createDto);
        public Patient MapFromUpdateDto(UpdatePatientDto updateDto, Patient patient);
        public IEnumerable<PatientDto> MapToDtos(IEnumerable<Patient> patients);
    }
}
