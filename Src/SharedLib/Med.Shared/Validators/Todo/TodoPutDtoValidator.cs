using FluentValidation;
using Med.Shared.Dtos.Todo;

namespace Med.Shared.Validators.Todo;

public class TodoPutDtoValidator : AbstractValidator<TodoUpdateDto>
{
    public TodoPutDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().NotNull();
        RuleFor(p => p.Status).NotEmpty().NotNull();
        RuleFor(p => p.Note).NotEmpty().NotNull();
        RuleFor(p => p.Id).NotEmpty().NotNull();



    }
}