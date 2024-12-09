using FluentValidation;
using TBC.PersonReference.Core.Entities;

namespace TBC.PersonReference.Application.Validators
{
    public class PhoneNumberValidator : AbstractValidator<PhoneNumber>
    {
        public PhoneNumberValidator()
        {
            RuleFor(p => p.Number)
                .MinimumLength(4).WithMessage("Number must be at least 4 characters.")
                .MaximumLength(50).WithMessage("Number cannot exceed 50 characters.");
        }
    }
}
