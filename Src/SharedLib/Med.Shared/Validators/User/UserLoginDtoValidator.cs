using FluentValidation;
using Med.Shared.Dtos.User;

namespace Med.Shared.Validators.User
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
            RuleFor(p => p.Password)
                .NotNull()
                .NotEmpty(); 
             
        }
    }
}
