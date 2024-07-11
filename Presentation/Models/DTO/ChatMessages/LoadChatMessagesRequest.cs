namespace Presentation.Models.DTO.ChatMessages
{
    public sealed record LoadChatMessagesRequest
    (
        string ChatRoomId,
        int Page = 1
    );
}
