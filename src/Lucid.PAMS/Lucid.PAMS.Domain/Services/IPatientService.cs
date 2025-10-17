using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Domain.Services
{
    public interface IPatientService
    {
        Task<ResponseDto<PatientDto>> CreatePatientAsync(CreatePatientDto patient);
        Task<ResponseDto<PatientDto>> UpdatePatientAsync(UpdatePatientDto patient);
        Task<ResponseDto<PatientDto>> DeletePatientAsync(Guid id);
        Task<ResponseDto<PatientDto>> GetPatientByIdAsync(Guid id);
        Task<ResponseDto<PatientDto>> GetAllPatientsAsync();
    }
}
