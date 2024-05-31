using Application.Messaging;

namespace Application.Logic.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommand : ICommand<int>
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
