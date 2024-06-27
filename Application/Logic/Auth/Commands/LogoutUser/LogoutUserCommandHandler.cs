using Application.Messaging;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Logic.Auth.Commands.LogoutUser
{
    public sealed class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutUserCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
        }
    }
}
