using Application.UseCases.ChatMessages.Commands.SendChatMessage;
using Application.UseCases.ChatMessages.Queries.GetMessageQuery;
using Application.UseCases.ChatMessages.Queries.LoadChatMessages;
using Domain.Entities.Users;
using Domain.Exceptions;
using Hubs.Chats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Presentation.Models.DTO.ChatMessages;
using System.Security.Claims;

namespace Presentation.Controllers.Chats
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatMessagesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<ChatRoomHub, IChatRoomHub> _chatHub;

        public ChatMessagesController
        (
            ISender sender,
            UserManager<ApplicationUser> userManager,
            IHubContext<ChatRoomHub, IChatRoomHub> chatHub
        )
        {
            _sender = sender;
            _userManager = userManager;
            _chatHub = chatHub;
        }

        [HttpGet("load")]
        public async Task<IActionResult> Load([FromQuery] LoadChatMessagesRequest request)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                ?? throw new BadRequestException("User not found!");

            var query = new LoadChatMessagesQuery(request.ChatRoomId, user.Id, request.Page);
            var result = await _sender.Send(query);

            return Ok(result);
        }

        [HttpPost("send")]
        public async Task Send(SendChatMessageRequest request)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                ?? throw new BadRequestException("User not found!");

            var command = new SendChatMessageCommand(request.Text, request.ChatRoomId, user.Id);
            var result = await _sender.Send(command);

            var query = new GetChatMessageQuery(result);
            var newMessage = await _sender.Send(query);
             
            await _chatHub.Clients.All.ReceiveMessage(newMessage);
        }
    }
}
