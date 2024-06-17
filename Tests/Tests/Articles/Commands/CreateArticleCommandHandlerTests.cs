using Application.Logic.Articles.Commands.CreateArticle;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using FluentValidation;
using Moq;
using UnitTests.Mocks.Repositories;

namespace UnitTests.Tests.Articles.Commands
{
    public class CreateArticleCommandHandlerTests
    {
        private readonly Mock<IArticleRepository> _articleRepositoryMock;
        private readonly CreateArticleCommandValidator _validatorMock;

        public CreateArticleCommandHandlerTests()
        {
            _articleRepositoryMock = MockArticleRepository.GetMock();
            _validatorMock = new CreateArticleCommandValidator();
        }


        [Fact]
        public async Task Handler_Should_ReturnSuccess()
        {
            var command = new CreateArticleCommand("Thrid Article", "Test");
            var handler = new CreateArticleCommandHandler(_articleRepositoryMock.Object, _validatorMock);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<int>(result);
            Assert.True(result == 3);
            _articleRepositoryMock.Verify(repo => repo.Insert(It.IsAny<Article>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ReturnValidationException()
        {
            var command = new CreateArticleCommand(null, null);
            var handler = new CreateArticleCommandHandler(_articleRepositoryMock.Object, _validatorMock);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, default));
            Assert.Contains("The article cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task Handler_Should_ReturnValidationException2()
        {
            var command = new CreateArticleCommand("", "");
            var handler = new CreateArticleCommandHandler(_articleRepositoryMock.Object, _validatorMock);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, default));
            Assert.Contains("The article cannot be empty.", exception.Message);
        }
    }
}
