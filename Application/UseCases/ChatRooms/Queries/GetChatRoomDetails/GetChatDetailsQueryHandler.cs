using Application.Messaging;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.ChatRooms.Queries.GetChatRoomDetails
{
    public class GetChatRoomDetailsQueryHandler : IQueryHandler<GetChatRoomDetailsQuery, ChatRoomDetailsViewModel>
    {
        private readonly IChatRoomsRepository _chatRoomsRepository;
        private readonly IChatRoomApplicationUserRepository _chatUserRepository;

        public GetChatRoomDetailsQueryHandler(
            IChatRoomsRepository chatRoomsRepository,
            IChatRoomApplicationUserRepository chatUserRepository
        )
        {
            _chatRoomsRepository = chatRoomsRepository;
            _chatUserRepository = chatUserRepository;
        }

        public async Task<ChatRoomDetailsViewModel> Handle(GetChatRoomDetailsQuery request, CancellationToken cancellationToken)
        {
            if (!await _chatUserRepository.IsExistByFields(request.UserId, request.Id))
                throw new BadRequestException("User not in this chat!");

            var chat = await _chatRoomsRepository
                .GetAsQueryable()
                .Where(cr => cr.Id == request.Id)
                .Include(cr => cr.UserChatRooms)
                .Select(cr => new ChatRoomDetailsViewModel
                {
                    Id = cr.Id,
                    Name = cr.Name,
                    UsersCount = cr.UserChatRooms.Count
                })
                .FirstOrDefaultAsync(cancellationToken) 
                ?? throw new BadRequestException("Chat does not exist.");

            return chat;
        }
    }
}
