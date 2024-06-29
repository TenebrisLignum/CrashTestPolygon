using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IApplicationUserChatRoomRepository
    {
        Task Insert(ApplicationUserChatRoom entity);
    }
}
