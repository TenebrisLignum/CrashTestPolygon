using Domain.Entities.Chats;
using Domain.Repositories.Chats;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Chats
{
    public class ChatRoomsRepository : IChatRoomsRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatRoomsRepository(ApplicationDbContext context) => _context = context;

        public async Task<bool> IsExistById(string id)
        {
            return await _context
                .Set<ChatRoom>()
                .AsNoTracking()
                .AnyAsync(a => a.Id.Equals(id));
        }

        public async Task<bool> IsExistByName(string name)
        {
            return await _context
                .Set<ChatRoom>()
                .AsNoTracking()
                .AnyAsync(a => a.Name.Equals(name));
        }

        public async Task<ChatRoom?> GetByName(string name)
        {
            return await _context
                .Set<ChatRoom>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name.Equals(name));
        }

        public async Task<ChatRoom?> GetById(string id, CancellationToken cancellationToken)
        {
            return await _context
                .Set<ChatRoom>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
        }

        public async Task<List<ChatRoom>> GetChatRoomsContainsUser(string userId, CancellationToken cancellationToken)
        {
            return await _context
                .Set<ChatRoom>()
                .Include(cr => cr.UserChatRooms)
                .Where(cr => cr.UserChatRooms
                    .Any(u => u.ApplicationUserId == userId)
                )
                .ToListAsync(cancellationToken);
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
