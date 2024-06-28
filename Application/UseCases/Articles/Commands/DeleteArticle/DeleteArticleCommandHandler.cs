using Application.UseCases.Articles.Commands.UpdateArticle;
using Application.Messaging;
using Domain.Exceptions;
using Domain.Repositories.Articles;

namespace Application.UseCases.Articles.Commands.DeleteArticle
{
    public class DeleteArticleCommandHandler : ICommandHandler<DeleteArticleCommand, int>
    {
        private readonly IArticleRepository _repository;

        public DeleteArticleCommandHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public int Id { get; set; }

        public async Task<int> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {                
            var article = await _repository.GetById(request.Id) ?? throw new NotFoundException("Article");

            await _repository.Delete(article.Id);

            return article.Id;
        }
    }
}
