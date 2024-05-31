using Application.Messaging;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using Microsoft.EntityFrameworkCore;

namespace Application.Logic.Articles.Queries.GetArticleById
{
    public class GetArticleByFilterQueryHandler : IQueryHandler<GetArticleByFilterQuery, List<Article>>
    {
        private readonly IArticleRepository _repository;

        public GetArticleByFilterQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Article>> Handle(GetArticleByFilterQuery request, CancellationToken cancellationToken)
        {
            return await _repository
                .GetAsQuery()
                .Where(a => a.Id <= 2)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
