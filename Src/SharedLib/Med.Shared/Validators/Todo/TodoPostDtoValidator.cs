using FluentValidation;
using Med.Shared.Dtos.Todo;

namespace Med.Shared.Validators.Todo
{
    public class TodoPostDtoValidator : AbstractValidator<TodoPostDto>
    {
        public TodoPostDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull();
            RuleFor(p => p.Status).NotEmpty().NotNull();
            RuleFor(p => p.Note).NotEmpty().NotNull();
        }
    }
}
