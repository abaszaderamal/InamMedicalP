using FluentValidation;
using Med.Shared.Dtos.Speciality;

namespace Med.Shared.Validators.Speciality;

public class SpecialityPutDtoValidator : AbstractValidator<SpecialityUpdateDto>
{
    public SpecialityPutDtoValidator()
    {
        RuleFor(p => p.ShortName).NotEmpty().NotNull();
        RuleFor(p => p.Name).NotEmpty().NotNull();
        RuleFor(p => p.Id).NotEmpty().NotNull();

    }
}