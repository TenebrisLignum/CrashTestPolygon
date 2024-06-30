using Domain.Entities.Chats;
using System.Threading;

namespace Domain.Repositories.Chats
{
    public interface IChatRoomsRepository
    {
        Task<bool> IsExist(string name);
        Task<ChatRoom?> GetByName(string name);
        Task<ChatRoom?> EnterChatRoom(string id, CancellationToken cancellationToken);
        Task Insert(ChatRoom chatRoom);
    }
}
