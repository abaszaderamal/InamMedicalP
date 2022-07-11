using FluentValidation;
using Med.Shared.Dtos.Raiting;

namespace Med.Shared.Validators.Raiting;

public class RaitingPutDtoValidator : AbstractValidator<RaitingUpdateDto>
{
    public RaitingPutDtoValidator()
    {
        RuleFor(p => p.Title).NotEmpty().NotNull();
        RuleFor(p => p.Id).NotEmpty().NotNull();

    }
}