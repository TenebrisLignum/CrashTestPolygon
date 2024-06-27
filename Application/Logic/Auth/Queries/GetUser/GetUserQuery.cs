using Application.Messaging;
using Domain.Entities.Users;

namespace Application.Logic.Auth.Queries.GetUser
{
    public sealed record GetUserQuery
        (
            string Email,
            string Password,
            bool RememberMe = true
        )
        : IQuery<ApplicationUser?>;
}
