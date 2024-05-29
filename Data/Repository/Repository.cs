using Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context) => _context = context;

        public async Task<T?> GetById(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Create(T entity)
        {
            await _context.AddAsync<T>(entity);
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
