namespace Presentation.Models.DTO.ChatRooms
{
    public sealed record JoinChatRoomRequest
    (
        string ChatRoomName,
        string? Password
    );
}
