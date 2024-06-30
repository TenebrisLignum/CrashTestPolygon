namespace Presentation.Models.DTO.Chats
{
    public sealed record CreateChatRoomRequest
    (
        string Name,
        bool IsPrivate,
        string? Password
    );
}
