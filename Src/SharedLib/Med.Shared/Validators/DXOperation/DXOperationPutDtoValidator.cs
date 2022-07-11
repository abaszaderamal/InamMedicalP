using FluentValidation;
using Med.Shared.Dtos.DXOperation;

namespace Med.Shared.Validators.DXOperation;

public class DXOperationPutDtoValidator : AbstractValidator<DXOperationPutDto>
{
    public DXOperationPutDtoValidator()
    {
        RuleFor(p => p.Note).NotEmpty().NotNull();
        RuleFor(p => p.Status).NotEmpty().NotNull();
        RuleFor(p => p.Id).NotEmpty().NotNull();
    }
}