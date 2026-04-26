using DTO.Request;
using FluentValidation;

namespace DTO.Validators.PersonData
{
    public class CreatePersonDataRequestValidator : AbstractValidator<CreatePersonDataRequest>
    {
        public CreatePersonDataRequestValidator()
        {
            RuleFor(x => x.PersonName)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 to 100 characters.");

            RuleFor(x => x.PersonDoB)
                .NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.PersonHeight)
                .NotEmpty().WithMessage("Height is required.")
                .InclusiveBetween(1, 8).WithMessage("Height must be from 1 feet to 8 feet");

            RuleFor(x => x.PersonWeight)
                .NotEmpty().WithMessage("Weight is required.")
                .InclusiveBetween(10, 300).WithMessage("Weight must be from 10 kg to 300 kg");

            RuleFor(x => x.PersonGender)
                .IsInEnum().WithMessage("Invalid gender value.");

            RuleFor(x => x.PersonMaritalStatus)
                .IsInEnum().WithMessage("Invalid marital status value.");

            RuleFor(x => x.PersonIsGraduated)
                .NotNull().WithMessage("Graduation Status is required.");
        }
    }
}
