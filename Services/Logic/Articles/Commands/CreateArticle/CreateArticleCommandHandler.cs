using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using Mapster;
using MediatR;

namespace Application.Logic.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, long>
    {
        private readonly IArticleRepository _repository;

        public CreateArticleCommandHandler(IArticleRepository repository) => _repository = repository;

        public async Task<long> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = request.Adapt<Article>();
            await _repository.Insert(article);

            return article.Id;
        }
    }
}
