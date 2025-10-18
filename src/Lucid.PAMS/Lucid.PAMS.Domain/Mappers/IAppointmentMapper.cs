using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Mappers
{
    public interface IAppointmentMapper
    {
        public AppointmentDto MapToDto(Appointment patient);
        public Appointment MapToEntity(AppointmentDto patientDto);
        public Appointment MapFromBookDto(BookAppointmentDto createDto);
        public Appointment MapFromUpdateDto(UpdateAppointmentDto updateDto);
        public IEnumerable<AppointmentDto> MapToDtos(IEnumerable<Appointment> patients);
    }
}
