using Application.Messaging;
using Domain.Entities.Articles;
using Domain.Exceptions;
using Domain.Repositories.Articles;
using FluentValidation;
using Mapster;

namespace Application.UseCases.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommandHandler : ICommandHandler<UpdateArticleCommand, int>
    {
        private readonly IArticleRepository _repository;
        private readonly IValidator<UpdateArticleCommand> _validator;

        public UpdateArticleCommandHandler(
            IArticleRepository repository,
            IValidator<UpdateArticleCommand> validator
            )
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            
            var article = await _repository.GetById(request.Id) ?? throw new NotFoundException("Article");

            TypeAdapterConfig<UpdateArticleCommand, Article>.NewConfig()
                .IgnoreNullValues(true);

            request.Adapt(article);
            await _repository.Update(article);

            return article.Id;
        }
    }
}
