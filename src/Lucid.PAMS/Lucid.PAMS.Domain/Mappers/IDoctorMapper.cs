using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Mappers
{
    public interface IDoctorMapper
    {
        public DoctorDto MapToDto(Doctor patient);
        public Doctor MapToEntity(DoctorDto patientDto);
        public Doctor MapFromCreateDto(CreateDoctorDto createDto);
        public IEnumerable<DoctorDto> MapToDtos(IEnumerable<Doctor> patients);
    }
}
