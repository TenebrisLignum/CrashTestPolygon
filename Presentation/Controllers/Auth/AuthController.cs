using Application.Logic.Auth.Commands.LoginUser;
using Application.Logic.Auth.Commands.LogoutUser;
using Application.Logic.Auth.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
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
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _sender.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("/logout")]
        [Authorize]
        public async Task<IActionResult> Logout(LogoutUserCommand command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
