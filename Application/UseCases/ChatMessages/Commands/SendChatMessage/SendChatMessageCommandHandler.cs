using Application.Mapping.Chats;
using Application.Messaging;
using Application.UseCases.ChatMessages.Queries.LoadChatMessages;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using FluentValidation;

namespace Application.UseCases.ChatMessages.Commands.SendChatMessage
{
    public sealed class SendChatMessageCommandHandler : ICommandHandler<SendChatMessageCommand, string>
    {
        private readonly IChatMessagesRepository _chatMessagesRepository;
        private readonly IChatRoomsRepository _chatRoomRepository;
        private readonly IChatRoomApplicationUserRepository _chatUserRepository;
        private readonly IValidator<SendChatMessageCommand> _validator;

        public SendChatMessageCommandHandler(
            IChatMessagesRepository chatMessagesRepository,
            IChatRoomsRepository chatRoomRepository,
            IChatRoomApplicationUserRepository chatUserRepository,
            IValidator<SendChatMessageCommand> validator
        )
        {
            _chatMessagesRepository = chatMessagesRepository;
            _chatRoomRepository = chatRoomRepository;
            _chatUserRepository = chatUserRepository;
            _validator = validator;
        }

        public async Task<string> Handle(SendChatMessageCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            if (!await _chatRoomRepository.IsExistById(request.ChatRoomId))
                throw new BadRequestException("Chat room does not exist!");

            if (!await _chatUserRepository.IsExistByFields(request.SenderId, request.ChatRoomId))
                throw new BadRequestException("You are not in this chat!");

            var newMessage = ChatMessageMapper.MapToChatMessage(request);
            await _chatMessagesRepository.Insert(newMessage);

            return newMessage.Id;
        }
    }
}
