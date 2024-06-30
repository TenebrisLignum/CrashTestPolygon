using Application.Messaging;
using Application.UseCases.ChatRooms.Commands.JoinChatRoom;
using Domain.Entities.Chats;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using Mapster;

namespace Application.UseCases.ChatRooms.Queries.GetChatRoom
{
    public sealed class EnterChatRoomQueryHandler : IQueryHandler<EnterChatRoomQuery, ChatRoomViewModel>
    {
        private readonly IChatRoomsRepository _chatRoomRepository;
        private readonly IApplicationUserChatRoomRepository _chatUserRepository;

        public EnterChatRoomQueryHandler
        (
            IChatRoomsRepository chatRoomRepository,
            IApplicationUserChatRoomRepository chatUserRepository
        )
        {
            _chatRoomRepository = chatRoomRepository;
            _chatUserRepository = chatUserRepository;
        }

        public async Task<ChatRoomViewModel> Handle(EnterChatRoomQuery request, CancellationToken cancellationToken)
        {
            var chatRoom = await _chatRoomRepository.EnterChatRoom(request.Id, cancellationToken) 
                ?? throw new BadRequestException("Invalid chatroom id!");

            var chatUser = new ApplicationUserChatRoom { ApplicationUserId = request.UserId, ChatRoomId = request.Id };

            if (!await _chatUserRepository.IsExist(chatUser))
            {
                if (chatRoom.IsPrivate)
                    throw new BadRequestException("You are not in this chat!");
                else
                    await _chatRoomRepository.Insert(chatRoom);
            }

            var chatRoomVM = chatRoom.Adapt<ChatRoomViewModel>();

            return chatRoomVM;
        }
    }
}
