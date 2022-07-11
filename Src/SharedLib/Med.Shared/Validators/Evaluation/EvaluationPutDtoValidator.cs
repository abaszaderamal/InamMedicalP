using FluentValidation;
using Med.Shared.Dtos.Evaluation;

namespace Med.Shared.Validators.Evaluation;

public class EvaluationPutDtoValidator : AbstractValidator<EvaluationPutDto>
{
    public EvaluationPutDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty().NotNull();
        RuleFor(p => p.OwnerUserId).NotEmpty().NotNull();
        RuleFor(p => p.RatingId).NotEmpty().NotNull();
        RuleFor(p => p.Value).NotEmpty().NotNull();
    }
}