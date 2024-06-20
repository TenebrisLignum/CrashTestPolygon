using Application.Messaging;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using Microsoft.EntityFrameworkCore;

namespace Application.Logic.Articles.Queries.GetArticleById
{
    public class GetArticlesByFilterQueryHandler : IQueryHandler<GetArticlesByFilterQuery, List<Article>>
    {
        private readonly IArticleRepository _repository;

        public GetArticlesByFilterQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Article>> Handle(GetArticlesByFilterQuery request, CancellationToken cancellationToken)
        {
            return await _repository
                .GetAsQuery()
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
