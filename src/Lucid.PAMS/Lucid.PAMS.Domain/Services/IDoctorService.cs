using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Services
{
    public interface IDoctorService
    {
        Task<ResponseDto<DoctorDto>> CreateDoctorAsync(CreateDoctorDto patient);
        Task<ResponseDto<IEnumerable<DoctorDto>>> GetAllDoctorsAsync();
        Task<ResponseDto<DoctorDto>> GetDoctorByIdAsync(Guid id);
    }
}
