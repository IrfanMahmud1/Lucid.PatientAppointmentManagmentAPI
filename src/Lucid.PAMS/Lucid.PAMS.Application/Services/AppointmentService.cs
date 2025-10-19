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
    public class AppointmentService : IAppointmentService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IAppointmentMapper _mapper;

        public AppointmentService(IApplicationUnitOfWork applicationUnitOfWork, IAppointmentMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork ?? throw new ArgumentNullException(nameof(applicationUnitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IAppointmentMapper));
        }

        // Create appointment with duplicate check

        public async Task<ResponseDto<AppointmentDto>> BookAppointmentAsync(BookAppointmentDto appointment)
        {
            if (appointment == null)
            {
                return ResponseDto<AppointmentDto>.Fail("Appointment is required");
            }

            try
            {
                // this pre-check is only an early optimization.
                if (await _applicationUnitOfWork.AppointmentRepository.IsAppointmentDuplicateAsync(appointment.PatientId, appointment.DoctorId, appointment.AppointmentDate))
                {
                    throw new DuplicateAppointmentException("An appointment with the same patient and doctor on this date already exists.");
                }
                // Map Appointment from Book DTO
                var appointmentEntity = _mapper.MapFromBookDto(appointment);

                // Generate token number
                appointmentEntity.TokenNumber = await GenerateTokenAsync(appointment.DoctorId, appointment.AppointmentDate);
                // Set initial status
                appointmentEntity.Status = "Pending";

                // Save appointment
                await _applicationUnitOfWork.AppointmentRepository.AddAsync(appointmentEntity);
                await _applicationUnitOfWork.SaveAsync();

                // Map to DTO
                var appointmentDto = _mapper.MapToDto(appointmentEntity);
                return ResponseDto<AppointmentDto>.Ok("Appointment booked successfully", appointmentDto);
            }
            catch (DuplicateAppointmentException ex)
            {
                return ResponseDto<AppointmentDto>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseDto<AppointmentDto>.Fail("Failed to create appointment");
            }
        }

        // Update appointment with duplicate check
        public async Task<ResponseDto<AppointmentDto>> UpdateAppointmentAsync(UpdateAppointmentDto appointment)
        {
            if (appointment == null)
            {
                return ResponseDto<AppointmentDto>.Fail("Appointment is required");
            }

            // Validate appointment id
            if (appointment.Id == Guid.Empty)
            {
                return ResponseDto<AppointmentDto>.Fail("Invalid appointment id");
            }

            try
            {
                // get existing appointment
                var appointmentEntity = await _applicationUnitOfWork.AppointmentRepository.GetByIdAsync(appointment.Id);

                // check if appointment exists
                if (appointmentEntity == null)
                {
                    return ResponseDto<AppointmentDto>.Fail("Appointment not found");
                }
                // Prevent updates to completed appointments
                if (appointmentEntity.Status == "Completed")
                {
                    return ResponseDto<AppointmentDto>.Fail("Completed appointment cannot be updated");
                }
                // Prevent updates to cancelled appointments
                if (appointmentEntity.Status == "Cancelled")
                {
                    return ResponseDto<AppointmentDto>.Fail("Cancelled appointment cannot be updated");
                }
                // Map Appointment from Update DTO
                appointmentEntity = _mapper.MapFromUpdateDto(appointment,appointmentEntity);

                // Update appointment
                await _applicationUnitOfWork.AppointmentRepository.EditAsync(appointmentEntity);
                await _applicationUnitOfWork.SaveAsync();

                // Map to DTO
                var appointmentDto = _mapper.MapToDto(appointmentEntity);
                return ResponseDto<AppointmentDto>.Ok("Appointment updated successfully", appointmentDto);
            }
            catch (DuplicateAppointmentException ex)
            {
                return ResponseDto<AppointmentDto>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseDto<AppointmentDto>.Fail("Failed to update appointment");
            }
        }


        // Get appointment by id
        public async Task<ResponseDto<AppointmentDto>> GetAppointmentByIdAsync(Guid id)
        {
            // Validate appointment id
            if (id == Guid.Empty)
            {
                return ResponseDto<AppointmentDto>.Fail("Invalid appointment id");
            }

            try
            {
                // Get appointment
                var appointment = await _applicationUnitOfWork.AppointmentRepository.GetByIdAsync(id);
                if (appointment == null)
                {
                    return ResponseDto<AppointmentDto>.Fail("Appointment not found");
                }

                return ResponseDto<AppointmentDto>.Ok("Appointment retrieved successfully", _mapper.MapToDto(appointment));
            }
            catch (Exception ex)
            {
                return ResponseDto<AppointmentDto>.Fail("Failed to retrieve appointment");
            }
        }

        // Get all appointments
        public async Task<ResponseDto<IEnumerable<AppointmentDto>>> GetAllAppointmentsAsync()
        {
            try
            {
                // Get all appointments
                var appointments = await _applicationUnitOfWork.AppointmentRepository.GetAllAsync();
                return ResponseDto<IEnumerable<AppointmentDto>>.Ok("Appointments retrieved successfully", _mapper.MapToDtos(appointments));
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<AppointmentDto>>.Fail("Failed to retrieve appointments");
            }
        }

        // Generate appointment token

        private async Task<int> GenerateTokenAsync(Guid doctorId, DateTime appointmentDate)
        {
            // Only consider same doctor and same day
            var existingTokens = await _applicationUnitOfWork.AppointmentRepository
                .GetAllAsync(a => a.DoctorId == doctorId && a.AppointmentDate.Date == appointmentDate.Date);

            int nextToken = 1;
            if (existingTokens.Any())
            {
                nextToken = existingTokens.Max(a => a.TokenNumber) + 1;
            }

            return nextToken;
        }

    }
}
