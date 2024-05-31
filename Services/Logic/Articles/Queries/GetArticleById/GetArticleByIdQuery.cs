using Application.Messaging;
using Domain.Entities.Articles;

namespace Application.Logic.Articles.Queries.GetArticleById
{
    public class GetArticleByIdQuery : IQuery<Article?>
    {
        public long Id { get; set; }
    }
}
