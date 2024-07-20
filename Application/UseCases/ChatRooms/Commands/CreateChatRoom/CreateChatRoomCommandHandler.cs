using Application.Mapping.Chats;
using Application.Messaging;
using Domain.Entities.Chats;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using FluentValidation;

namespace Application.UseCases.ChatRooms.Commands.CreateChatRoom
{
    public sealed class CreateChatRoomCommandHandler : ICommandHandler<CreateChatRoomCommand, string>
    {
        private readonly IChatRoomsRepository _chatRoomRepository;
        private readonly IChatRoomApplicationUserRepository _chatUserRepository;
        private readonly IValidator<CreateChatRoomCommand> _validator;

        public CreateChatRoomCommandHandler(
            IChatRoomsRepository chatRoomsRepository,
            IChatRoomApplicationUserRepository chatUserRepository,
            IValidator<CreateChatRoomCommand> validator
        )
        {
            _chatRoomRepository = chatRoomsRepository;
            _chatUserRepository = chatUserRepository;
            _validator = validator;

        }
        public async Task<string> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            if (await _chatRoomRepository.IsExistByName(request.Name))
                throw new BadRequestException($"Chat with the name {request.Name} is already exist.");

            var chatRoom = ChatRoomMapper.MapToChatRoom(request);
            await _chatRoomRepository.Insert(chatRoom);

            var userChatRoom = new ApplicationUserChatRoom
            {
                ChatRoomId = chatRoom.Id,
                ApplicationUserId = chatRoom.OwnerId
            };
            await _chatUserRepository.Insert(userChatRoom);

            return chatRoom.Id;
        }
    }
}
