using FluentValidation;

namespace Application.UseCases.ChatMessages.Commands.SendChatMessage
{
    public sealed class SendChatMessageCommandValidator : AbstractValidator<SendChatMessageCommand>
    {
        public SendChatMessageCommandValidator() {
            RuleFor(cm => cm.Text)
                .NotEmpty()
                .WithMessage("You cannot send an empty message.");
        }
    }
}
