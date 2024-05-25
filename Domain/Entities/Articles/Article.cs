using Domain.Entities.Abstract;

namespace Domain.Entities.Articles
{
    public class Article : Entity
    {
        public string? Message { get; set; }
    }
}
