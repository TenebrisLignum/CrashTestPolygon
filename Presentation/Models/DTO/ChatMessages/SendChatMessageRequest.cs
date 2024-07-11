namespace Presentation.Models.DTO.ChatMessages
{
    public sealed record SendChatMessageRequest
    (
        string Text,
        string ChatRoomId
    );
}
