using FluentValidation;
using Lucid.PAMS.Domain.Dtos;

namespace Lucid.PAMS.Api.Validators
{
    public class UpdateAppointmentValidator : AbstractValidator<UpdateAppointmentDto>
    {
        public UpdateAppointmentValidator()
        {
            // Id must be present
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => status == "Pending" || status == "Confirmed" || status == "Cancelled")
                .WithMessage("Status must be one of the following: Pending, Confirmed, Cancelled.");

        }
    }
}
