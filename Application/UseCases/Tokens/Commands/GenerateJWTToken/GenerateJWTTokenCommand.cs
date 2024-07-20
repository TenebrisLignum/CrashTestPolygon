using Application.Messaging;
using Domain.Entities.Users;

namespace Application.UseCases.Tokens.Commands.GenerateJWTToken
{
    public sealed record GenerateJWTTokenCommand
    (
        ApplicationUser User
    )
    : ICommand<string>;
}
