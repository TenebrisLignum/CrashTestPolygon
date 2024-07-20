using Application.Messaging;

namespace Application.UseCases.ChatRooms.Queries.GetChatRoomsByUserId
{
    public sealed record GetChatRoomsByUserIdQuery
    (
        string UserId
    )
    : IQuery<List<ChatRoomItemViewModel>>;
}
