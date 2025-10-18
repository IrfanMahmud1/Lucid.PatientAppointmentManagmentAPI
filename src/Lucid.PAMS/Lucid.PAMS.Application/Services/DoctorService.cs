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
    public class DoctorService : IDoctorService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IDoctorMapper _mapper;

        public DoctorService(IApplicationUnitOfWork applicationUnitOfWork, IDoctorMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork ?? throw new ArgumentNullException(nameof(applicationUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IDoctorMapper));
        }

        // Create doctor

        public async Task<ResponseDto<DoctorDto>> CreateDoctorAsync(CreateDoctorDto doctor)
        {
            if (doctor == null)
            {
                return ResponseDto<DoctorDto>.Fail("Doctor is required");
            }

            if(doctor.Id == Guid.Empty)
            {
                doctor.Id = Guid.NewGuid();
            }

            try
            {

                var doctorEntity = _mapper.MapFromCreateDto(doctor);

                await _applicationUnitOfWork.DoctorRepository.AddAsync(doctorEntity);
                await _applicationUnitOfWork.SaveAsync();

                var doctorDto = _mapper.MapToDto(doctorEntity);
                return  ResponseDto<DoctorDto>.Ok("Doctor created successfully", doctorDto );
            }
            catch (Exception ex)
            {
                return ResponseDto<DoctorDto>.Fail("Failed to create doctor");
            }
        }

        // Get doctor by id
        public async Task<ResponseDto<DoctorDto>> GetDoctorByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return ResponseDto<DoctorDto>.Fail("Invalid doctor id");
            }

            try
            {
                var doctor = await _applicationUnitOfWork.DoctorRepository.GetByIdAsync(id);
                if (doctor == null)
                {
                    return ResponseDto<DoctorDto>.Fail("doctor not found");
                }

                return ResponseDto<DoctorDto>.Ok("doctor retrieved successfully", _mapper.MapToDto(doctor));
            }
            catch (Exception ex)
            {
                return ResponseDto<DoctorDto>.Fail("Failed to retrieve doctor");
            }
        }


        // Get all doctors
        public async Task<ResponseDto<IEnumerable<DoctorDto>>> GetAllDoctorsAsync()
        {
            try
            {
                var doctors = await _applicationUnitOfWork.DoctorRepository.GetAllAsync();
                return ResponseDto<IEnumerable<DoctorDto>>.Ok("Doctors retrieved successfully",_mapper.MapToDtos(doctors));
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<DoctorDto>>.Fail("Failed to retrieve doctors");
            }
        }
    }
}
