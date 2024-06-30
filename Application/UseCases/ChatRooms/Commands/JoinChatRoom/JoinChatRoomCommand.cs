
using Application.Messaging;

namespace Application.UseCases.ChatRooms.Commands.JoinChatRoom
{
    public sealed record JoinChatRoomCommand
    (
        string ChatRoomName,
        string UserId,
        string? Password
    )
    : ICommand<ChatRoomViewModel>;
}
