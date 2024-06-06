using Application.Messaging;

namespace Application.Logic.Articles.Commands.DeleteArticle
{
    public class DeleteArticleCommand : ICommand<int>
    {
        public int Id { get; set; }
    }
}
