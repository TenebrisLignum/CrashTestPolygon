using Application.UseCases.ChatMessages.Commands.SendChatMessage;
using Domain.Entities.Users;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.DTO.Chats;
using System.Security.Claims;

namespace Presentation.Controllers.Chat
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ChatMessagesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatMessagesController
        (
            ISender sender,
            UserManager<ApplicationUser> userManager
        )
        {
            _sender = sender;
            _userManager = userManager;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send(SendChatMessageRequest request)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                ?? throw new BadRequestException("User not found!");

            var command = new SendChatMessageCommand(request.Text, request.ChatRoomId, user.Id);
            var result = await _sender.Send(command);

            return Ok(result);
        }
    }
}
