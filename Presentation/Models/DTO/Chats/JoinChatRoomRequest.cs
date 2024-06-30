namespace Presentation.Models.DTO.Chats
{
    public sealed record JoinChatRoomRequest
    (
        string ChatRoomName,
        string? Password
    );
}
