using FluentValidation;
using Med.Shared.Dtos.Tag;

namespace Med.Shared.Validators.Tag;

public class TagPutDtoValidator : AbstractValidator<TagUpdateDto>
{
    public TagPutDtoValidator()
    {
        RuleFor(p => p.RaitingName).NotEmpty().NotNull();
        RuleFor(p => p.Id).NotEmpty().NotNull();

    }
}