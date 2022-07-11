using FluentValidation;
using Med.Shared.Dtos.Tag;

namespace Med.Shared.Validators.Tag
{
    public class TagPostDtoValidator : AbstractValidator<TagPostDto>
    {
        public TagPostDtoValidator()
        {
            RuleFor(p=>p.RaitingName).NotEmpty().NotNull();
        }
    }
}
