using FluentValidation;
using Med.Shared.Dtos.User;

namespace Med.Shared.Validators.User
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(p => p.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.PhoneNumber)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Password
                )
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.EMail)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
