using Application.Messaging;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using Mapster;

namespace Application.UseCases.ChatRooms.Queries.GetChatRoomDetails
{
    public class GetChatRoomDetailsQueryHandler : IQueryHandler<GetChatRoomDetailsQuery, ChatRoomDetailsViewModel>
    {
        private readonly IChatRoomsRepository _chatRoomsRepository;

        public GetChatRoomDetailsQueryHandler(
            IChatRoomsRepository chatRoomsRepository    
        )
        {
            _chatRoomsRepository = chatRoomsRepository;
        }

        public async Task<ChatRoomDetailsViewModel> Handle(GetChatRoomDetailsQuery request, CancellationToken cancellationToken)
        {
            var chat = await _chatRoomsRepository.GetById(request.Id, cancellationToken) 
                ?? throw new BadRequestException("Chat does not exist.");

            var chatVM = chat.Adapt<ChatRoomDetailsViewModel>();

            return chatVM;
        }
    }
}
