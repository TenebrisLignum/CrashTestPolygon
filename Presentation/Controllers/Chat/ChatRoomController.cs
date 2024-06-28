using Application.UseCases.ChatRooms.Commands.CreateChatRoom;
using Domain.Entities.Users;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers.Chat
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatRoomController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatRoomController(
            ISender sender,
            UserManager<ApplicationUser> userManager
        )
        {
            _sender = sender;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(string name)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChatRoomCommand command)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value) ?? throw new BadRequestException("User not found!");
            var newCommand = command with { OwnerId = user.Id };

            var result = await _sender.Send(newCommand);
            return Ok(result);
        }
    }
}
