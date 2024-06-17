using Application.Logic.Articles.Commands.UpdateArticle;
using Domain.Entities.Articles;
using Domain.Repositories.Articles;
using FluentValidation;
using Moq;
using UnitTests.Mocks.Repositories;

namespace UnitTests.Tests.Articles.Commands
{
    public class UpdateArticleCommandHandlerTests
    {
        private readonly Mock<IArticleRepository> _articleRepositoryMock;
        private readonly UpdateArticleCommandValidator _validatorMock;

        public UpdateArticleCommandHandlerTests()
        {
            _articleRepositoryMock = MockArticleRepository.GetMock();
            _validatorMock = new UpdateArticleCommandValidator();
        }


        [Fact]
        public async Task Handler_Should_ReturnSuccess()
        {
            var command = new UpdateArticleCommand(2, "Second Article Updated", "Test");
            var handler = new UpdateArticleCommandHandler(_articleRepositoryMock.Object, _validatorMock);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<int>(result);
            Assert.True(result == 2);

            var updatedArticle = MockArticleRepository.Articles.FirstOrDefault(x => x.Id == 2);
            Assert.True(String.Compare(updatedArticle.Title, command.Title) == 0);

            _articleRepositoryMock.Verify(repo => repo.Update(It.IsAny<Article>()), Times.Once);
        }

        [Fact]
        public async Task Handler_Should_ReturnValidationException()
        {
            var command = new UpdateArticleCommand(2, null, null);
            var handler = new UpdateArticleCommandHandler(_articleRepositoryMock.Object, _validatorMock);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, default));
            Assert.Contains("The article cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task Handler_Should_ReturnValidationException2()
        {
            var command = new UpdateArticleCommand(2, "", "");
            var handler = new UpdateArticleCommandHandler(_articleRepositoryMock.Object, _validatorMock);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, default));
            Assert.Contains("The article cannot be empty.", exception.Message);
        }


        [Fact]
        public async Task Handler_Should_ReturnValidationException3()
        {
            var command = new UpdateArticleCommand(0, "Zero Article", "Test");
            var handler = new UpdateArticleCommandHandler(_articleRepositoryMock.Object, _validatorMock);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, default));
            Assert.Contains("Article Id cannot be less than 1.", exception.Message);
        }
    }
}
