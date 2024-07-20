using Application.Common;
using Application.Messaging;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;

namespace Application.UseCases.Articles.Queries.GetArticleById
{
    public class GetArticlesByFilterQueryHandler : IQueryHandler<GetArticlesByFilterQuery, PagedList<Article>>
    {
        private readonly IArticleRepository _repository;

        public GetArticlesByFilterQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<Article>> Handle(GetArticlesByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _repository
                .GetAsQuery();

            if (!string.IsNullOrWhiteSpace(request.SearchWord))
                query = query
                    .Where(a => a.Title.Contains(request.SearchWord));

            query = query
                .OrderByDescending(a => a.CreatedDate);

            return await PagedList<Article>.CreateAsync(query, request.Page, request.PageSize);
        }
    }
}
