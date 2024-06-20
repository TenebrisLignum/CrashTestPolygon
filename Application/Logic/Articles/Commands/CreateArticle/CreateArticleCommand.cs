using Application.Messaging;

namespace Application.Logic.Articles.Commands.CreateArticle
{
    public sealed record CreateArticleCommand
        (
            string Title, 
            string Text
        ) 
        : ICommand<int>;
}
