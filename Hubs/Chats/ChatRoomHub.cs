using Application.UseCases.ChatMessages.Queries.LoadChatMessages;
using Domain.Repositories.Chats;
using Microsoft.AspNetCore.SignalR;

namespace Hubs.Chats
{
    public class ChatRoomHub : Hub<IChatRoomHub>
    {

    }

    public interface IChatRoomHub
    {
        Task ReceiveMessage(ChatMessageViewModel message);
    }
}
