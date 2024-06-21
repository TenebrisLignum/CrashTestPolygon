using Application.Messaging;
using Domain.Entities.Articles;

namespace Application.Logic.Articles.Queries.GetArticleById
{
    public sealed record GetArticleByIdQuery
        (
            int Id
        ) 
        : IQuery<Article?>;
}
