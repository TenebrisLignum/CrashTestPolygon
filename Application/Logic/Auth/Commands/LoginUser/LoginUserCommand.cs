using Application.Messaging;
using Microsoft.AspNetCore.Identity;

namespace Application.Logic.Auth.Commands.LoginUser
{
    public sealed record LoginUserCommand
        (
            string Username,
            string Password,
            bool RememberMe = true
        )
        : ICommand<SignInResult>;
}
