using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IChatRoomsRepository
    {
        Task<bool> IsExistById(string id);
        Task<bool> IsExistByName(string name);
        Task<ChatRoom?> GetByName(string name);
        Task<ChatRoom?> GetById(string id, CancellationToken cancellationToken);
        Task<List<ChatRoom>> GetChatRoomsContainsUser(string userId, CancellationToken cancellationToken);
        Task Insert(ChatRoom chatRoom);
    }
}
