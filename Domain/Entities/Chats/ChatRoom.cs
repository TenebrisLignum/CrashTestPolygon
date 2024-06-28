using Domain.Entities.Abstract;
using Domain.Entities.Users;

namespace Domain.Entities.Chats
{
    public sealed class ChatRoom : EntityGuidId
    {
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
