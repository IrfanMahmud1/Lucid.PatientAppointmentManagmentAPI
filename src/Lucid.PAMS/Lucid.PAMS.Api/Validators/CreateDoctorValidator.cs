using FluentValidation;
using Lucid.PAMS.Domain.Dtos;

namespace Lucid.PAMS.Api.Validators
{
    public class CreateDoctorValidator : AbstractValidator<CreateDoctorDto>
    {
        public CreateDoctorValidator()
        {

            // Name: required, reasonable length
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            // Phone: required , basic format
            RuleFor(x => x.Phone)
                .NotNull().WithMessage("Phone is required.")
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\d{11}$").WithMessage("Phone number must be exactly 11 digits long and contain only numbers.")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));


            // Department: required, reasonable length
            RuleFor(x => x.Department)
                .NotNull().WithMessage("Department is required.")
                .NotEmpty().WithMessage("Department is required.")
                .Length(2, 100).WithMessage("Department must be between 2 and 100 characters.");

            // Fee: required, positive value
            RuleFor(x => x.Fee)
                .GreaterThan(0).WithMessage("Fee must be a positive value.");

        }
    }
}
