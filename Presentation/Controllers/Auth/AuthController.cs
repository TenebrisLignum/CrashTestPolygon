using Application.Logic.Auth.Commands.RegisterUser;
using Application.Logic.Auth.Queries.GetUser;
using Application.Logic.Tokens.Commands.GenerateJWTToken;
using Domain.Entities.Users;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _sender.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] GetUserQuery command)
        {
            var user = await _sender.Send(command);

            if (user is null)
                throw new BadRequestException("User does't exist!");

            if (!await _userManager.CheckPasswordAsync(user, command.Password))
                throw new BadRequestException("Wrong password!");

            var token = await _sender.Send(new GenerateJWTTokenCommand(user));

            return Ok(new { token });
        }
    }
}
