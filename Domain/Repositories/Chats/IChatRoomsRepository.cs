using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IChatRoomsRepository
    {
        Task<bool> IsExistById(string id);
        Task<bool> IsExistByName(string name);
        Task<ChatRoom?> GetByName(string name);
        Task<ChatRoom?> EnterChatRoom(string id, CancellationToken cancellationToken);
        Task Insert(ChatRoom chatRoom);
    }
}
