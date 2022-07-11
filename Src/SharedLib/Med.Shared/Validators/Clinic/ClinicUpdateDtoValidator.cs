using FluentValidation;
using Med.Shared.Dtos.Clinic;

namespace Med.Shared.Validators.Clinic
{
    public class ClinicUpdateDtoValidator :AbstractValidator<ClinicUpdateDto>
    {
        public ClinicUpdateDtoValidator()
        {
            RuleFor(p=>p.Id).NotEmpty().NotNull();

            RuleFor(p => p.Email)
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
