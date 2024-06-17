using Domain.Repositories.Articles;
using Moq;
using UnitTests.Mocks.Repositories;

namespace UnitTests.Tests.Articles.Commands
{
    public class GetArticleByFilterQueryHandlerTests
    {
        private readonly Mock<IArticleRepository> _articleRepositoryMock;

        public GetArticleByFilterQueryHandlerTests()
        {
            _articleRepositoryMock = MockArticleRepository.GetMock();
        }


        // TODO: IMPLEMENT
    }
}
