using Application.Messaging;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using FluentValidation;
using Mapster;

namespace Application.Logic.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandHandler : ICommandHandler<CreateArticleCommand, int>
    {
        private readonly IArticleRepository _repository;
        private readonly IValidator<CreateArticleCommand> _validator;

        public CreateArticleCommandHandler(
            IArticleRepository repository,
            IValidator<CreateArticleCommand> validator
            )
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var article = request.Adapt<Article>();
            article.CreatedDate = DateTime.UtcNow;

            await _repository.Insert(article);

            return article.Id;
        }
    }
}
