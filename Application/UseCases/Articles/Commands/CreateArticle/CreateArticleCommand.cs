using Application.Messaging;

namespace Application.UseCases.Articles.Commands.CreateArticle
{
    public sealed record CreateArticleCommand
        (
            string Title, 
            string Text
        ) 
        : ICommand<int>;
}
