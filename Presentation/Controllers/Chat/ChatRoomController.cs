using Application.UseCases.ChatRooms.Commands.CreateChatRoom;
using Application.UseCases.ChatRooms.Commands.JoinChatRoom;
using Application.UseCases.ChatRooms.Queries.GetChatRoom;
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
    //[Authorize]
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

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
        //        ?? throw new BadRequestException("User not found!");

        //    var query = new EnterChatRoomQuery(id, user.Id);

        //    var result = await _sender.Send(query);
        //    return Ok(result);
        //}

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
