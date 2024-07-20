using Application.Messaging;

namespace Application.UseCases.ChatRooms.Commands.CreateChatRoom
{
    public sealed record CreateChatRoomCommand
        (
            string Name,
            bool IsPrivate,
            string? Password,
            string OwnerId
        )
        : ICommand<string>;
}
