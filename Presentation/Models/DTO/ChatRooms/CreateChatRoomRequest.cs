namespace Presentation.Models.DTO.ChatRooms
{
    public sealed record CreateChatRoomRequest
    (
        string Name,
        bool IsPrivate,
        string? Password
    );
}
