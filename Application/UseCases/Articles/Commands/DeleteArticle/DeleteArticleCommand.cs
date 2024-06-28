using Application.Messaging;

namespace Application.UseCases.Articles.Commands.DeleteArticle
{
    public sealed record DeleteArticleCommand
        (
            int Id
        )
        : ICommand<int>;
}
