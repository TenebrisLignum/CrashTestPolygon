using FluentValidation;

namespace Application.UseCases.ChatRooms.Commands.CreateChatRoom
{
    public class CreateChatRoomCommandValidator : AbstractValidator<CreateChatRoomCommand>
    {
        public CreateChatRoomCommandValidator() {
            RuleFor(cr => cr.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(64)
                .WithMessage("Name should contains from 4 to 64 symbols.");

            RuleFor(cr => cr.Password)
                .NotEmpty()
                .When(cr => cr.IsPrivate) 
                .WithMessage("Password is required for private chat rooms.");

            RuleFor(cr => cr.Password)
                .MinimumLength(4)
                .MaximumLength(16)
                .When(cr => cr.IsPrivate)
                .WithMessage("Password should contains from 4 to 16 symbols.");
        }
    }
}
