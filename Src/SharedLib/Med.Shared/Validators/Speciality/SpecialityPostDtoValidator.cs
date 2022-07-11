using FluentValidation;
using Med.Shared.Dtos.Speciality;

namespace Med.Shared.Validators.Speciality
{
    public class SpecialityPostDtoValidator : AbstractValidator<SpecialityPostDto>
    {
        public SpecialityPostDtoValidator()
        {
            RuleFor(p => p.ShortName).NotEmpty().NotNull();
            RuleFor(p => p.Name).NotEmpty().NotNull();
        }
    }
}
