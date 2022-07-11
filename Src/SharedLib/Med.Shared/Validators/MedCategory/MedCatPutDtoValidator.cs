using FluentValidation;
using Med.Shared.Dtos.MedCategory;

namespace Med.Shared.Validators.MedCategory;

public class MedCatPutDtoValidator : AbstractValidator<MedCatUpdateDto>
{
    public MedCatPutDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty().NotNull();
        RuleFor(p => p.Name).NotEmpty().NotNull();
    }
}