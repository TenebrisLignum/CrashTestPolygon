using Application.Messaging;

namespace Application.UseCases.ChatMessages.Queries.LoadChatMessages
{
    public sealed record LoadChatMessagesQuery
    (
        string ChatRoomId,
        string UserId,
        int Page = 1
    )
    : IQuery<ChatMessagesViewModel>;
}
