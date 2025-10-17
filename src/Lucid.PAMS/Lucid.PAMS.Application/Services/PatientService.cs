using Lucid.PAMS.Application.Exceptions;
using Lucid.PAMS.Domain;
using Lucid.PAMS.Domain.Dtos;
using Lucid.PAMS.Domain.Entities;
using Lucid.PAMS.Domain.Mappers;
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
        private readonly IPatientMapper _mapper;

        public PatientService(IApplicationUnitOfWork applicationUnitOfWork, IPatientMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork ?? throw new ArgumentNullException(nameof(applicationUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IPatientMapper));
        }

        // Create patient with duplicate check

        public async Task<ResponseDto<PatientDto>> CreatePatientAsync(CreatePatientDto patient)
        {
            if (patient == null)
            {
                return ResponseDto<PatientDto>.Fail("Patient is required");
            }

            if(patient.Id != Guid.Empty)
            {
                return ResponseDto<PatientDto>.Fail("New patient id must be empty");
            }

            try
            {
                // this pre-check is only an early optimization.
                if (await _applicationUnitOfWork.PatientRepository.IsPatientDuplicateAsync(patient.Name, patient.Phone))
                {
                    throw new DuplicatePatientException("A patient with the same name and phone number already exists.");
                }

                await _applicationUnitOfWork.PatientRepository.AddAsync(_mapper.Map(patient));
                await _applicationUnitOfWork.SaveAsync();

                var patientDto = _mapper.Map(_mapper.Map(patient));
                return  ResponseDto<PatientDto>.Ok("Patient created successfully", patientDto );
            }
            catch (DuplicatePatientException ex)
            {
                return ResponseDto<PatientDto>.Fail(ex.Message );
            }
            catch (Exception ex)
            {
                return ResponseDto<PatientDto>.Fail("Failed to create patient");
            }
        }

        // Update patient with duplicate check
        public async Task<ResponseDto<PatientDto>> UpdatePatientAsync(UpdatePatientDto patient)
        {
            if (patient == null)
            {
                return ResponseDto<PatientDto>.Fail("Patient is required");
            }

            if (patient.Id == Guid.Empty)
            {
                return ResponseDto<PatientDto>.Fail("Invalid patient id");
            }

            try
            {
                if (await _applicationUnitOfWork.PatientRepository.IsPatientDuplicateAsync(patient.Name, patient.Phone, patient.Id))
                {
                    throw new DuplicatePatientException("A patient with the same name and phone number already exists.");
                }

                await _applicationUnitOfWork.PatientRepository.EditAsync(_mapper.Map(patient));
                await _applicationUnitOfWork.SaveAsync();

                var patientDto = _mapper.Map(_mapper.Map(patient));
                return ResponseDto<PatientDto>.Ok("Patient created successfully", patientDto);
            }
            catch (DuplicatePatientException ex)
            {
                return ResponseDto<PatientDto>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseDto<PatientDto>.Fail("Failed to update patient");
            }
        }

        // Delete patient by id

        public async Task<ResponseDto<PatientDto>> DeletePatientAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return ResponseDto<PatientDto>.Fail("Invalid patient id");
            }

            try
            {
                await _applicationUnitOfWork.PatientRepository.RemoveAsync(id);
                await _applicationUnitOfWork.SaveAsync();

                return ResponseDto<PatientDto>.Ok("Patient deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseDto<PatientDto>.Fail("Failed to delete patient");
            }
        }

        // Get patient by id
        public async Task<ResponseDto<PatientDto>> GetPatientByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return ResponseDto<PatientDto>.Fail("Invalid patient id");
            }

            try
            {
                var patient = await _applicationUnitOfWork.PatientRepository.GetByIdAsync(id);
                if (patient == null)
                {
                    return ResponseDto<PatientDto>.Fail("Patient not found");
                }

                return ResponseDto<PatientDto>.Ok("Patient retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseDto<PatientDto>.Fail("Failed to retrieve patient");
            }
        }

        // Get all patients
        public async Task<ResponseDto<PatientDto>> GetAllPatientsAsync()
        {
            try
            {
                var patients = await _applicationUnitOfWork.PatientRepository.GetAllAsync();
                return ResponseDto<PatientDto>.Ok("Patients retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseDto<PatientDto>.Fail("Failed to retrieve patients");
            }
        }
    }
}
