using FluentValidation;

namespace Application.Logic.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotNull()
                .WithMessage("Article Id cannot be null.");

            RuleFor(command => command.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Article Id cannot be less than 1.");

            RuleFor(command => command.Title)
                .NotEmpty()
                .WithMessage("The article title cannot be empty.");

            RuleFor(command => command.Text)
                .NotEmpty()
                .WithMessage("The article cannot be empty.");
        }
    }
}
