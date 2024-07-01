using Application.Messaging;
using Application.UseCases.ChatRooms.Commands.JoinChatRoom;

namespace Application.UseCases.ChatRooms.Queries.GetChatRoom
{
    public sealed record GetChatRoomQuery
        (
            string Id,
            string UserId
        )
        : IQuery<ChatRoomViewModel>;
}
