using Application.Common;
using Application.Messaging;
using Domain.Entities.Articles;

namespace Application.UseCases.Articles.Queries.GetArticleById
{
    public sealed record GetArticlesByFilterQuery
        (
            string? SearchWord,
            int PageSize = 10,
            int Page = 1
        )
        : IQuery<PagedList<Article>>;
}
