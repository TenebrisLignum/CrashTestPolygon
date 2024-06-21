using Application.Messaging;

namespace Application.Logic.Articles.Commands.UpdateArticle
{
    public sealed record UpdateArticleCommand
        (
            int Id,
            string Title,
            string Text
        )
        : ICommand<int>;
}
