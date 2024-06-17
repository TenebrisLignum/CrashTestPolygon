using Application.Messaging;

namespace Application.Logic.Articles.Commands.UpdateArticle
{
    public sealed class UpdateArticleCommand : ICommand<int>
    {
        public UpdateArticleCommand
        (
            int id,
            string title,
            string text
        )
        {
            Id = id;
            Title = title;
            Text = text;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
}
