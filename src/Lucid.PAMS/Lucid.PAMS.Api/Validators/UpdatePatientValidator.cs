using FluentValidation;
using Lucid.PAMS.Domain.Dtos;

namespace Lucid.PAMS.Api.Validators
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientDto>
    {
        public UpdatePatientValidator()
        {
            // Id must be present
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            // Name: required, reasonable length
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.")
                .NotEqual("string").WithMessage("Name cannot be the default value 'string'.");


            // Phone: required , basic format
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\d{11}$").WithMessage("Phone number must be exactly 11 digits long and contain only numbers.");

            // Age: reasonable range
            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0).WithMessage("Age cannot be negative.")
                .LessThanOrEqualTo(130).WithMessage("Age seems too high.");

            // Gender: required and limited to known values
            RuleFor(x => x.Gender)
                .Must(g => g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Gender must be 'Male', 'Female', 'Other'.");

            // Address: required and length limit
            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.")
                .NotEqual("string").WithMessage("Address cannot be the default value 'string'.");

            // CreatedDate: ensure it's not in the future (adjust as needed)
            RuleFor(x => x.CreatedDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedDate cannot be in the future.")
                .When(x => x.CreatedDate != default(DateTime));
        }
    }
}
