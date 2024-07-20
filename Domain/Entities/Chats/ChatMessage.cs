using Domain.Entities.Abstract;
using Domain.Entities.Users;

namespace Domain.Entities.Chats
{
    public sealed class ChatMessage : EntityGuidId
    {
        public string Text { get; set; }
        public string ChatRoomId { get; set; }
        public string SenderId { get; set; }
        public DateTime CreatedDate { get; set; }

        public ChatRoom ChatRoom { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}
