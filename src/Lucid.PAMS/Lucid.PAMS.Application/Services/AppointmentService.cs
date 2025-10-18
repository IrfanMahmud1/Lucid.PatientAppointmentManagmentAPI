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

            if(appointment.Id == Guid.Empty)
            {
                appointment.Id = Guid.NewGuid();
            }

            try
            {
                // this pre-check is only an early optimization.
                if (await _applicationUnitOfWork.AppointmentRepository.IsAppointmentDuplicateAsync(appointment.PatientId, appointment.DoctorId,appointment.AppointmentDate))
                {
                    throw new DuplicateAppointmentException("An appointment with the same patient and doctor on this date already exists.");
                }

                var appointmentEntity = _mapper.MapFromBookDto(appointment);

                await _applicationUnitOfWork.AppointmentRepository.AddAsync(appointmentEntity);
                await _applicationUnitOfWork.SaveAsync();

                var appointmentDto = _mapper.MapToDto(appointmentEntity);
                return  ResponseDto<AppointmentDto>.Ok("Appointment booked successfully", appointmentDto );
            }
            catch (DuplicateAppointmentException ex)
            {
                return ResponseDto<AppointmentDto>.Fail(ex.Message );
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

            if (appointment.Id == Guid.Empty)
            {
                return ResponseDto<AppointmentDto>.Fail("Invalid appointment id");
            }

            try
            {
                if (await _applicationUnitOfWork.AppointmentRepository.IsAppointmentDuplicateAsync(appointment.PatientId, appointment.DoctorId, appointment.AppointmentDate))
                {
                    throw new DuplicateAppointmentException("An appointment with the same patient and doctor on this date already exists.");
                }

                var appointmentEntity = _mapper.MapFromUpdateDto(appointment);

                await _applicationUnitOfWork.AppointmentRepository.EditAsync(appointmentEntity);
                await _applicationUnitOfWork.SaveAsync();

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
            if (id == Guid.Empty)
            {
                return ResponseDto<AppointmentDto>.Fail("Invalid appointment id");
            }

            try
            {
                var appointment = await _applicationUnitOfWork.AppointmentRepository.GetByIdAsync(id);
                if (appointment == null)
                {
                    return ResponseDto<AppointmentDto>.Fail("Appointment not found");
                }

                return ResponseDto<AppointmentDto>.Ok("Appointment retrieved successfully",_mapper.MapToDto(appointment));
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
                var appointments = await _applicationUnitOfWork.AppointmentRepository.GetAllAsync();
                return ResponseDto<IEnumerable<AppointmentDto>>.Ok("Appointments retrieved successfully",_mapper.MapToDtos(appointments));
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<AppointmentDto>>.Fail("Failed to retrieve appointments");
            }
        }

    }
}
