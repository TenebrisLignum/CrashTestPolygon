using Domain.Entities.Articles;

namespace Domain.Repositories.Articles
{
    public interface IArticleRepository
    {
        Task<Article?> GetById(long id);
        Task Insert(Article article);
        Task Update(Article article);
        Task Delete(long id);
    }
}
