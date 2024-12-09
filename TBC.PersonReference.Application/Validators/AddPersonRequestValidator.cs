using FluentValidation;
using TBC.PersonReference.Application.Models.Request;

namespace TBC.PersonReference.Application.Validators
{
    public class AddPersonRequestValidator : AbstractValidator<AddPersonRequest>
    {
        public AddPersonRequestValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .MinimumLength(2).WithMessage("FirstName must be at least 2 characters.")
                .MaximumLength(50).WithMessage("FirstName cannot exceed 50 characters.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("FirstName must contain only Latin letters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .MinimumLength(2).WithMessage("LastName must be at least 2 characters.")
                .MaximumLength(50).WithMessage("LastName cannot exceed 50 characters.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("FirstName must contain only Latin letters.");

            RuleFor(p => p.PrivateNumber)
                .NotEmpty().WithMessage("PrivateNumber is required.")
                .Length(11).WithMessage("PrivateNumber must be exactly 11 characters.");

            RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage("BirthDate is required.")
                .Must(SharedFunctions.BeAtLeast18YearsOld).WithMessage("Person must be at least 18 years old.");

            RuleForEach(p => p.PhoneNumbers)
                .SetValidator(new PhoneNumberValidator());
        }

    }
}
