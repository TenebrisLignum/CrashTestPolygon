using Domain.Entities.Chats;
using Domain.Repositories.Chats;

namespace Data.Repository.Chats
{
    public class ChatMessagesRepository : IChatMessagesRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatMessagesRepository(ApplicationDbContext context) => _context = context;

        public async Task Insert(ChatMessage message)
        {
            await _context
                .Set<ChatMessage>()
                .AddAsync(message);

            await _context
                .SaveChangesAsync();
        }
    }
}
