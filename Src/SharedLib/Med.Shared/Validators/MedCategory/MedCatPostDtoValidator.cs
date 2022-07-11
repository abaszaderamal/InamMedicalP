using FluentValidation;
using Med.Shared.Dtos.MedCategory;

namespace Med.Shared.Validators.MedCategory
{
    public class MedCatPostDtoValidator : AbstractValidator<MedCatPostDto>
    {
        public MedCatPostDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull();
        }
    }
}
