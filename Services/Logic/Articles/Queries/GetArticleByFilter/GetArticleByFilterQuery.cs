using Application.Messaging;
using Domain.Entities.Articles;

namespace Application.Logic.Articles.Queries.GetArticleById
{
    public class GetArticleByFilterQuery : IQuery<List<Article>>
    {
    }
}
