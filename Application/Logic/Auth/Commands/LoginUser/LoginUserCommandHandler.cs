using Application.Messaging;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Logic.Auth.Commands.LoginUser
{
    public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, SignInResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginUserCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _signInManager.PasswordSignInAsync(request.Username, request.Password, request.RememberMe, lockoutOnFailure: false);
        }
    }
}
