using FluentValidation;
using Med.Shared.Dtos.Clinic;

namespace Med.Shared.Validators.Clinic
{
    public class ClinicPostDtoValidator :AbstractValidator<ClinicPostDto>
    {
        public ClinicPostDtoValidator()
        {
            RuleFor(p=>p.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.Number)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.Address)
                .NotEmpty()
                .NotNull();
        }
    }
}
