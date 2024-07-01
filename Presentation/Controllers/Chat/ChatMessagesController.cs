using Application.UseCases.ChatMessages.Commands.SendChatMessage;
using Application.UseCases.ChatMessages.Queries.LoadChatMessages;
using Domain.Entities.Users;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.DTO.Chats;
using System.Security.Claims;

namespace Presentation.Controllers.Chat
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpGet("load")]
        public async Task<IActionResult> Load([FromQuery] LoadChatMessagesRequest request)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                ?? throw new BadRequestException("User not found!");

            var query = new LoadChatMessagesQuery(request.ChatRoomId, user.Id, request.Page);
            var result = await _sender.Send(query);

            return Ok(result);
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
