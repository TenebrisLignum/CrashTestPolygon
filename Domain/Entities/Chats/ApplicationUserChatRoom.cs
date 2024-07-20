using Domain.Entities.Chats;
using Domain.Entities.Users;

namespace Domain.Entities.Chats
{
    public class ApplicationUserChatRoom
    {
        public string ApplicationUserId { get; set; }
        public string ChatRoomId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
