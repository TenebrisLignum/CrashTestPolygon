using Domain.Entities.Abstract;

namespace Domain.Entities.Articles
{
    public class Article : Entity
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
