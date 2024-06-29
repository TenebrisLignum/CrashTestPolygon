using Domain.Entities.Chats;
using Domain.Repositories.Chats;

namespace Data.Repository.Chats
{
    internal class ApplicationUserChatRoomRepository : IApplicationUserChatRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserChatRoomRepository(ApplicationDbContext context)
        {
            _context = context;
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
