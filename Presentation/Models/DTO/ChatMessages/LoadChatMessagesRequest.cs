namespace Presentation.Models.DTO.ChatMessages
{
    public sealed record LoadChatMessagesRequest
    (
        string ChatRoomId,
        string? LastMessageId
    );
}
