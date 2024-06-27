using Application.Messaging;
using Microsoft.AspNetCore.Identity;

namespace Application.Logic.Auth.Commands.RegisterUser
{
    public sealed record RegisterUserCommand
    (
        string Email,
        string Username,
        string Password,
        string ConfirmPassword
    )
    : ICommand<IdentityResult>;
}
