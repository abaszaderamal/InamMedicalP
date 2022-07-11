using FluentValidation;
using Med.Shared.Dtos.Evaluation;

namespace Med.Shared.Validators.Evaluation;

public class EvaluationPostDtoValidator : AbstractValidator<EvaluationPostDto>
{
    public EvaluationPostDtoValidator()
    {
        RuleFor(p => p.OwnerUserId).NotEmpty().NotNull();
        RuleFor(p => p.RatingId).NotEmpty().NotNull();
        RuleFor(p => p.Value).NotEmpty().NotNull();
    }
}