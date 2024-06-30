namespace Presentation.Models.DTO.Chats
{
    public sealed record SendChatMessageRequest
    (
        string Text,
        string ChatRoomId
    );
}
