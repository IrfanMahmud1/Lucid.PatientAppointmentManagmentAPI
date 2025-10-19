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
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            // Phone: required , basic format
            RuleFor(x => x.Phone)
                .NotNull().WithMessage("Phone is required.")
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid.")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            // Age: reasonable range
            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0).WithMessage("Age cannot be negative.")
                .LessThanOrEqualTo(130).WithMessage("Age seems too high.");

            // Gender: required and limited to known values
            RuleFor(x => x.Gender)
                .Must(g => string.IsNullOrWhiteSpace(g) ||
                           g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Gender must be 'Male', 'Female', 'Other', or empty.")
                .When(x => !string.IsNullOrWhiteSpace(x.Gender));

            // Address: required and length limit
            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            // CreatedDate: ensure it's not in the future (adjust as needed)
            RuleFor(x => x.CreatedDate)
                .Equal(DateTime.UtcNow).WithMessage("CreatedDate cannot be in the past or future.")
                .When(x => x.CreatedDate != default(DateTime));
        }
    }
}
