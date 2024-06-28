using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IChatMessagesRepository
    {
        Task Insert(ChatMessage message);
    }
}
