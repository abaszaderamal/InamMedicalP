using FluentValidation;
using Med.Shared.Dtos.Raiting;

namespace Med.Shared.Validators.Raiting
{
    public class RaitingPostDtoValidator : AbstractValidator<RaitingPostDto>
    {
        public RaitingPostDtoValidator()
        {
            RuleFor(p => p.Title).NotEmpty().NotNull();

        }
    }
}
