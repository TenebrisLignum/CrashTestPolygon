using Domain.Entities.Chats;
using Domain.Repositories.Chats;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Chats
{
    public class ChatRoomsRepository : IChatRoomsRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatRoomsRepository(ApplicationDbContext context) => _context = context;

        public async Task<ChatRoom?> GetByName(string name)
        {
            return await _context
                .Set<ChatRoom>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name.Equals(name));
        }

        public async Task Insert(ChatRoom chatRoom)
        {
            await _context
                .Set<ChatRoom>()
                .AddAsync(chatRoom);

            await _context
                .SaveChangesAsync();
        }
    }
}
