using Application.UseCases.ChatMessages.Queries.LoadChatMessages;
using Microsoft.AspNetCore.SignalR;

namespace Hubs.Chats
{
    public class ChatRoomHub : Hub<IChatRoomHub>
    {
        public async Task JoinChatRoom(string chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);
        }

        public async Task LeaveChatRoom(string chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
        }
    }

    public interface IChatRoomHub
    {
        Task ReceiveMessage(ChatMessageViewModel message);
    }
}
