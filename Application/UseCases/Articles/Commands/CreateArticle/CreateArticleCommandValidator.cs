using FluentValidation;

namespace Application.UseCases.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator() {
            RuleFor(command => command.Title)
                .NotEmpty()
                .WithMessage("The article title cannot be empty.");

            RuleFor(command => command.Text)
                .NotEmpty()
                .WithMessage("The article cannot be empty.");
        }
    }
}
