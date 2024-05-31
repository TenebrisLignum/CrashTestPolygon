using Application.Messaging;

namespace Application.Logic.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommand : ICommand<long>
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
