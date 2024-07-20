using Application.Messaging;

namespace Application.UseCases.ChatMessages.Commands.SendChatMessage
{
    public sealed record SendChatMessageCommand
    (
        string Text,
        string ChatRoomId,
        string SenderId
    )
    : ICommand<string>;
}
