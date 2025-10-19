using FluentValidation;
using Lucid.PAMS.Domain.Dtos;

namespace Lucid.PAMS.Api.Validators
{
    public class BookAppointmentValidator : AbstractValidator<BookAppointmentDto>
    {
        public BookAppointmentValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("PatientId is required.")
                .Must(id => id != Guid.Empty).WithMessage("PatientId cannot be an empty GUID.");
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("DoctorId is required.")
                .Must(id => id != Guid.Empty).WithMessage("DoctorId cannot be an empty GUID.");
            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("AppointmentDate is required.")
                .Must(date => date > DateTime.Now).WithMessage("AppointmentDate must be in the future.");
        }
    }
}
