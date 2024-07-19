using Application.Messaging;
using Application.UseCases.ChatMessages.Queries.LoadChatMessages;

namespace Application.UseCases.ChatMessages.Queries.GetMessageQuery
{
    public sealed record GetChatMessageQuery
    (
        string Id    
    )
    : IQuery<ChatMessageViewModel>;
}
