using Application.UseCases.ChatRooms.Commands.CreateChatRoom;
using Application.UseCases.ChatRooms.Commands.JoinChatRoom;
using Application.UseCases.ChatRooms.Queries.GetChatRoomsByUserId;
using Domain.Entities.Users;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.DTO.Chats;
using System.Security.Claims;

namespace Presentation.Controllers.Chats
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatRoomsController(
            ISender sender,
            UserManager<ApplicationUser> userManager
        )
        {
            _sender = sender;
            _userManager = userManager;
        }

        [HttpGet("my-chats")]
        public async Task<IActionResult> List()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                ?? throw new BadRequestException("User not found!");

            var query = new GetChatRoomsByUserIdQuery(user.Id);
            var result = await _sender.Send(query);

            return Ok(result);
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join(JoinChatRoomRequest request)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                ?? throw new BadRequestException("User not found!");

            var command = new JoinChatRoomCommand(request.ChatRoomName, user.Id, request.Password);

            var result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChatRoomRequest request)
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                ?? throw new BadRequestException("User not found!");

            var command = new CreateChatRoomCommand(request.Name, request.IsPrivate, request.Password, user.Id);

            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}
