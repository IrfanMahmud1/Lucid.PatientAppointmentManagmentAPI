using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Services
{
    public interface IAppointmentService
    {
        Task<ResponseDto<AppointmentDto>> BookAppointmentAsync(BookAppointmentDto patient);
        Task<ResponseDto<AppointmentDto>> UpdateAppointmentAsync(UpdateAppointmentDto patient);
        Task<ResponseDto<AppointmentDto>> GetAppointmentByIdAsync(Guid id);
        Task<ResponseDto<IEnumerable<AppointmentDto>>> GetAllAppointmentsAsync();
        Task<ResponseDto<IEnumerable<AppointmentDto>>> FilterAppointmentsAsync(FilterAppointmentDto dto);
    }
}
