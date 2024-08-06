using Application.UseCases.ChatRooms.Commands.CreateChatRoom;
using Application.UseCases.ChatRooms.Commands.JoinChatRoom;
using Application.UseCases.ChatRooms.Queries.GetChatRoomDetails;
using Application.UseCases.ChatRooms.Queries.GetChatRoomsByUserId;
using Domain.Entities.Users;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.DTO.ChatRooms;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new BadRequestException("User not found!");

            var query = new GetChatRoomDetailsQuery(id, userId);
            var result = await _sender.Send(query);
            return Ok(result);
        }

        [HttpGet("my-chats")]
        public async Task<IActionResult> List()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new BadRequestException("User not found!");

            var query = new GetChatRoomsByUserIdQuery(userId);
            var result = await _sender.Send(query);

            return Ok(result);
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join(JoinChatRoomRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new BadRequestException("User not found!");

            var command = new JoinChatRoomCommand(request.ChatRoomName, userId, request.Password);

            var result = await _sender.Send(command);
            return Ok(new { chatRoomId = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateChatRoomRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new BadRequestException("User not found!");

            var command = new CreateChatRoomCommand(request.Name, request.IsPrivate, request.Password, userId);

            var result = await _sender.Send(command);
            return Ok(new { chatRoomId = result });
        }
    }
}
