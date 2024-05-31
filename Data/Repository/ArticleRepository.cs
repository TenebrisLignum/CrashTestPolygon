using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    internal class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context) => _context = context;

        public async Task<Article?> GetById(int id)
        {
            return await _context
                .Set<Article>()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public IQueryable<Article> GetAsQuery()
        {
            return _context
                .Set<Article>()
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task Insert(Article article)
        {
            await _context
                .Set<Article>()
                .AddAsync(article);

            await _context
                .SaveChangesAsync();
        }

        public async Task Update(Article article)
        {
            _context
                .Set<Article>()
                .Update(article);

            await _context
                .SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Article? article = await GetById(id);

            if (article != null)
                _context
                    .Set<Article>()
                    .Remove(article);

            await _context
                .SaveChangesAsync();
        }
    }
}
