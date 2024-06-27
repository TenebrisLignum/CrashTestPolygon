using FluentValidation;

namespace Application.Logic.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() {
            RuleFor(u => u.Password)
                .Equal(u => u.ConfirmPassword)
                .WithMessage("Password doesn't match with confirm password.");

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("Invalid email.");
        }
    }
}
