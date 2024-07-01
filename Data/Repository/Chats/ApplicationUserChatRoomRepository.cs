using Domain.Entities.Chats;
using Domain.Repositories.Chats;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Chats
{
    internal class ChatRoomApplicationUserRepository : IChatRoomApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsExistByFields(string userId, string chatRoomId)
        {
            return await _context
                .Set<ApplicationUserChatRoom>()
                .AsNoTracking()
                .AnyAsync(e =>
                    e.ApplicationUserId == userId
                    && e.ChatRoomId == chatRoomId
                );
        }

        public async Task<bool> IsExist(ApplicationUserChatRoom entity)
        {
            return await _context
                .Set<ApplicationUserChatRoom>()
                .AsNoTracking()
                .AnyAsync(e => 
                    e.ApplicationUserId == entity.ApplicationUserId
                    && e.ChatRoomId == entity.ChatRoomId
                );
        }

        public async Task Insert(ApplicationUserChatRoom entity)
        {
            await _context
                .Set<ApplicationUserChatRoom>()
                .AddAsync(entity);

            await _context.
                SaveChangesAsync();
        }
    }
}
