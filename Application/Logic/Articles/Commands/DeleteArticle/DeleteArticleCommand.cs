using Application.Messaging;

namespace Application.Logic.Articles.Commands.DeleteArticle
{
    public sealed record DeleteArticleCommand
        (
            int Id
        )
        : ICommand<int>;
}
