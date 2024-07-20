using Application.Messaging;

namespace Application.UseCases.ChatRooms.Queries.GetChatRoomDetails
{
    public record GetChatRoomDetailsQuery
    (
        string Id,
        string UserId
    )
    : IQuery<ChatRoomDetailsViewModel>;
}
