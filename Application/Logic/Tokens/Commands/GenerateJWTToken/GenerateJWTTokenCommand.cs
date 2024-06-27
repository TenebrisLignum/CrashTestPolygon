using Application.Messaging;
using Domain.Entities.Users;

namespace Application.Logic.Tokens.Commands.GenerateJWTToken
{
    public sealed record GenerateJWTTokenCommand
    (
        ApplicationUser User
    )
    : ICommand<string>;
}
