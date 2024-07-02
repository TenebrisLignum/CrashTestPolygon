using Application.Messaging;
using Domain.Repositories.Chats;
using Mapster;

namespace Application.UseCases.ChatRooms.Queries.GetChatRoomsByUserId
{
    public class GetChatRoomsByUserIdQueryHandler : IQueryHandler<GetChatRoomsByUserIdQuery, List<ChatRoomItemViewModel>>
    {
        private readonly IChatRoomsRepository _chatRoomsRepository;
        
        public GetChatRoomsByUserIdQueryHandler(IChatRoomsRepository chatRoomsRepository)
        {
            _chatRoomsRepository = chatRoomsRepository;
        }

        public async Task<List<ChatRoomItemViewModel>> Handle(GetChatRoomsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _chatRoomsRepository.GetChatRoomsContainsUser(request.UserId, cancellationToken);
            var chatRoomVMs = result.Adapt<List<ChatRoomItemViewModel>>();

            return chatRoomVMs;
        }
    }
}
