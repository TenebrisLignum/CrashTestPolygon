using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IChatMessagesRepository
    {
        IQueryable<ChatMessage> GetAsQueryable();
        Task Insert(ChatMessage message);
    }
}
