using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IChatRoomsRepository
    {
        Task<ChatRoom?> GetByName(string name);
        Task Insert(ChatRoom chatRoom);
    }
}
