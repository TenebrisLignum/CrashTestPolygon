namespace Presentation.Models.DTO.Chats
{
    public sealed record LoadChatMessagesRequest
    (
        string ChatRoomId,
        int Page = 1
    );
}
