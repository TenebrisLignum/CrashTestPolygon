using Application.Messaging;
using Domain.Entities.Articles;

namespace Application.Logic.Articles.Queries.GetArticleById
{
    public class GetArticleByIdQuery : IQuery<Article?>
    {
        public GetArticleByIdQuery(int id) { this.Id = id; }

        public int Id { get; set; }
    }
}
