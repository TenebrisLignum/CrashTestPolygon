using Application.Messaging;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;

namespace Application.Logic.Articles.Queries.GetArticleById
{
    public class GetArticleByIdQueryHandler : IQueryHandler<GetArticleByIdQuery, Article?>
    {
        private readonly IArticleRepository _repository;

        public GetArticleByIdQueryHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Article?> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetById(request.Id);
        }
    }
}
