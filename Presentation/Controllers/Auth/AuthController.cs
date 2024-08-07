﻿using Application.UseCases.Auth.Commands.RegisterUser;
using Application.UseCases.Auth.Queries.GetUser;
using Application.UseCases.Tokens.Commands.GenerateJWTToken;
using Domain.Entities.Users;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(
            ISender sender,
            UserManager<ApplicationUser> userManager
        )
        {
            _sender = sender;
            _userManager = userManager;
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _sender.Send(command);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.First().Description);

            return Ok(result);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] GetUserQuery query)
        {
            var user = await _sender.Send(query);

            if (user is null)
                throw new BadRequestException("User does't exist!");

            if (!await _userManager.CheckPasswordAsync(user, query.Password))
                throw new BadRequestException("Wrong password!");

            var token = await _sender.Send(new GenerateJWTTokenCommand(user));

            return Ok(new { token });
        }
    }
}
