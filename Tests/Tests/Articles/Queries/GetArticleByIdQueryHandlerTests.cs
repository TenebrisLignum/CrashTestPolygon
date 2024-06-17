using Application.Logic.Articles.Queries.GetArticleById;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using Moq;
using UnitTests.Mocks.Repositories;

namespace UnitTests.Tests.Articles.Commands
{
    public class GetArticleByIdQueryHandlerTests
    {
        private readonly Mock<IArticleRepository> _articleRepositoryMock;

        public GetArticleByIdQueryHandlerTests()
        {
            _articleRepositoryMock = MockArticleRepository.GetMock();
        }

        [Fact]
        public async Task Handler_Should_ReturnSuccess()
        {
            var command = new GetArticleByIdQuery(1);
            var handler = new GetArticleByIdQueryHandler(_articleRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Article>(result);
            Assert.True(result.Id == 1);

            _articleRepositoryMock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
