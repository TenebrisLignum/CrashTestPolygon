using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IChatRoomsRepository
    {
        Task<bool> IsExist(string name);
        Task<ChatRoom?> GetByName(string name);
        Task Insert(ChatRoom chatRoom);
    }
}
