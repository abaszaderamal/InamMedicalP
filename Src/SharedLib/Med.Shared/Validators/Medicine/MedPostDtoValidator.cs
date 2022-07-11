using FluentValidation;
using Med.Shared.Dtos.MedCategory;
using Med.Shared.Dtos.Medicine;

namespace Med.Shared.Validators.Medicine
{
    public class MedPostDtoValidator : AbstractValidator<MedPostDto>
    {
        public MedPostDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull();
            RuleFor(p => p.MedCategoryId).NotEmpty().NotNull();
        }
    }
}
