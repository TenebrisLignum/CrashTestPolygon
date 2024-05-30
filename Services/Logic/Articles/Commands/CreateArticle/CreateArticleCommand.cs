using MediatR;

namespace Application.Logic.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommand : IRequest<long>
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
