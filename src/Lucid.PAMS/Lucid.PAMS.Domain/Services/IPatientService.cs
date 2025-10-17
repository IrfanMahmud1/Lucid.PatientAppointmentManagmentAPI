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
        Task CreatePatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(Guid id);
        Task<Patient> GetPatientByIdAsync(Guid id);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
    }
}
