using Domain.Entities.Articles;

namespace Domain.Repositories.Articles
{
    public interface IArticleRepository
    {
        Task<Article?> GetByIdAsync(int id);
        Task Insert(Article article);
        Task Update(Article article);
        Task Delete(int id);
    }
}
