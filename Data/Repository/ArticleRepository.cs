using Domain.Entities.Articles;
using Domain.Repositories.Articles;

namespace Data.Repository
{
    internal class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context) { 
            _context = context;
        }

        public Article? GetById(int id)
        {
            return _context.Set<Article>().Find(id);
        }

        public async Task Insert(Article article)
        {
            await _context.Set<Article>().AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public void Update(Article article)
        {
            _context.Set<Article>().Update(article);
        }

        public void Delete(int id)
        {
            Article? article = GetById(id);
            if (article != null)
            {
                _context.Set<Article>().Remove(article);
            }
        }
    }
}
