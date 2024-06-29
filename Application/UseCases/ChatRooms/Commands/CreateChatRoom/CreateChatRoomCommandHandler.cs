using Application.Messaging;
using Domain.Entities.Chats;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using FluentValidation;
using Mapster;

namespace Application.UseCases.ChatRooms.Commands.CreateChatRoom
{
    public sealed class CreateChatRoomCommandHandler : ICommandHandler<CreateChatRoomCommand, string>
    {
        private readonly IChatRoomsRepository _chatRoomRepository;
        private readonly IApplicationUserChatRoomRepository _chatUserRepository;
        private readonly IValidator<CreateChatRoomCommand> _validator;

        public CreateChatRoomCommandHandler(
            IChatRoomsRepository chatRoomsRepository,
            IApplicationUserChatRoomRepository chatUserRepository,
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

            if (await _chatRoomRepository.IsExist(request.Name))
                throw new BadRequestException($"Chat with the name {request.Name} is already exist.");

            var chatRoom = request.Adapt<ChatRoom>();

            await InsertChatRoom(chatRoom);
            await AddOwnerToChatRoomUsers(chatRoom);

            return chatRoom.Id;
        }

        private async Task InsertChatRoom(ChatRoom chatRoom)
        {
            chatRoom.Id = Guid.NewGuid().ToString();
            chatRoom.CreatedDate = DateTime.UtcNow;

            await _chatRoomRepository.Insert(chatRoom);
        }

        private async Task AddOwnerToChatRoomUsers(ChatRoom chatRoom)
        {
            var userChatRoom = new ApplicationUserChatRoom
            {
                ChatRoomId = chatRoom.Id,
                ApplicationUserId = chatRoom.OwnerId
            };
            await _chatUserRepository.Insert(userChatRoom);
        }
    }
}
