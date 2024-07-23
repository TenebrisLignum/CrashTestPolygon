using Application.Messaging;

namespace Application.UseCases.ChatMessages.Queries.LoadChatMessages
{
    public sealed record LoadChatMessagesQuery
    (
        string ChatRoomId,
        string UserId,
        string LastMessageId
    )
    : IQuery<ChatMessagesViewModel>;
}
