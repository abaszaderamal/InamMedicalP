using FluentValidation;
using Med.Shared.Dtos.Medicine;

namespace Med.Shared.Validators.Medicine;

public class MedPutDtoValidator : AbstractValidator<MedUpdateDto>
{
    public MedPutDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty().NotNull();
        RuleFor(p => p.Name).NotEmpty().NotNull();
        RuleFor(p => p.MedCategoryId).NotEmpty().NotNull();
    }
}