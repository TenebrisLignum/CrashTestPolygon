using Application.Messaging;
using Domain.Entities.Chats;
using Domain.Repositories.Chats;
using FluentValidation;
using Mapster;

namespace Application.UseCases.ChatRooms.Commands.CreateChatRoom
{
    public sealed class CreateChatRoomCommandHandler : ICommandHandler<CreateChatRoomCommand, string>
    {
        private readonly IChatRoomsRepository _repository;
        private readonly IValidator<CreateChatRoomCommand> _validator;

        public CreateChatRoomCommandHandler(
            IChatRoomsRepository repository,
            IValidator<CreateChatRoomCommand> validator
        )
        {
            _repository = repository;
            _validator = validator;

        }
        public async Task<string> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var chatRoom = request.Adapt<ChatRoom>();

            chatRoom.Id = Guid.NewGuid().ToString();
            chatRoom.CreatedDate = DateTime.UtcNow;

            await _repository.Insert(chatRoom);
            return chatRoom.Id;
        }
    }
}
