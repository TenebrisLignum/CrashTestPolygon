using Application.Messaging;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using Domain.Exceptions;

namespace Application.UseCases.Articles.Queries.GetArticleById
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
            return await _repository.GetById(request.Id) ?? throw new NotFoundException("Article");
        }
    }
}
