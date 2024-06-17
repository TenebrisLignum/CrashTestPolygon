using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using Moq;

namespace UnitTests.Mocks.Repositories
{
    public static class MockArticleRepository
    {
        public static List<Article> Articles =
            [
                new Article
                {
                    Id = 1,
                    Title = "First Article",
                    Text = "Test",
                    CreatedDate = DateTime.UtcNow.AddDays(-1)
                },
                new Article
                {
                    Id = 2,
                    Title = "Second Article",
                    Text = "Test",
                    CreatedDate = DateTime.UtcNow
                }
            ];

        public static Mock<IArticleRepository> GetMock()
        {

            var mockRepo =
                new Mock<IArticleRepository>();

            mockRepo
                .Setup(r => r.GetById(It.IsAny<int>()))
                .ReturnsAsync((int id) => Articles.FirstOrDefault(a => a.Id == id));

            mockRepo
                .Setup(r => r.GetAsQuery())
                .Returns(Articles.AsQueryable());

            mockRepo
                .Setup(r => r.Insert(It.IsAny<Article>()))
                .Callback((Article article) =>
                {
                    article.Id = Articles.Count + 1;
                    Articles.Add(article);
                })
                .Returns(Task.CompletedTask);

            mockRepo
                .Setup(r => r.Update(It.IsAny<Article>()))
                .Callback((Article updatedArticle) =>
                {
                    var existingArticle = Articles.FirstOrDefault(a => a.Id == updatedArticle.Id);
                    if (existingArticle != null)
                    {
                        existingArticle.Title = updatedArticle.Title;
                        existingArticle.Text = updatedArticle.Text;
                    }
                })
                .Returns(Task.CompletedTask);

            mockRepo
                .Setup(r => r.Delete(It.IsAny<int>()))
                .Callback((int id) =>
                {
                    var existingArticle = Articles.FirstOrDefault(a => a.Id == id);
                    if (existingArticle != null)
                    {
                        Articles.Remove(existingArticle);
                    }
                })
                .Returns(Task.CompletedTask);

            return mockRepo;
        }
    }

}
