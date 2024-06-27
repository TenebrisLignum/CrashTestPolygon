﻿using Application.Messaging;
using Domain.Entities.Articles;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Logic.Auth.Queries.GetUser
{
    public sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, ApplicationUser?>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Username);
            return user;
        }
    }
}
