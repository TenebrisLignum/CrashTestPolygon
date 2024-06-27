using Application.Messaging;
using Domain.Entities.Users;

namespace Application.Logic.Auth.Queries.GetUser
{
    public sealed record GetUserQuery
        (
            string Username,
            string Password,
            bool RememberMe = true
        )
        : IQuery<ApplicationUser?>;
}
