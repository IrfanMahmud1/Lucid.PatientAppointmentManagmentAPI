using Lucid.PAMS.Domain;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.PAMS.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public PatientService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }

        public async Task CreatePatientAsync(Patient patient)
        {
            try
            {
                if (!await _applicationUnitOfWork.PatientRepository.IsPatientDuplicateAsync(patient.Name, patient.Phone))
                {
                    await _applicationUnitOfWork.PatientRepository.AddAsync(patient);
                    await _applicationUnitOfWork.SaveAsync();
                }
                throw new ArgumentException("Duplicate patient found");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdatePatientAsync(Patient patient)
        {
            try
            {
                if (!await _applicationUnitOfWork.PatientRepository.IsPatientDuplicateAsync(patient.Name, patient.Phone, patient.Id))
                {
                    await _applicationUnitOfWork.PatientRepository.EditAsync(patient);
                    await _applicationUnitOfWork.SaveAsync();
                }
                throw new ArgumentException("Duplicate patient found");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeletePatientAsync(Guid id)
        {
            try
            {
                await _applicationUnitOfWork.PatientRepository.RemoveAsync(id);
                await _applicationUnitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(Guid id)
        {
            try
            {
                return await _applicationUnitOfWork.PatientRepository.GetByIdAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            try
            {
                return await _applicationUnitOfWork.PatientRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
