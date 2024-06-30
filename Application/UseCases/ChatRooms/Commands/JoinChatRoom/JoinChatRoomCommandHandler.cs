using Application.Common;
using Application.Messaging;
using Domain.Entities.Chats;
using Domain.Exceptions;
using Domain.Repositories.Chats;

namespace Application.UseCases.ChatRooms.Commands.JoinChatRoom
{
    public class JoinChatRoomCommandHandler : ICommandHandler<JoinChatRoomCommand, string>
    {
        private readonly IChatRoomsRepository _chatRoomRepository;
        private readonly IApplicationUserChatRoomRepository _chatUserRepository;

        public JoinChatRoomCommandHandler(
            IChatRoomsRepository chatRoomsRepository,
            IApplicationUserChatRoomRepository chatUserRepository
        )
        {
            _chatRoomRepository = chatRoomsRepository;
            _chatUserRepository = chatUserRepository;
        }

        public async Task<string> Handle(JoinChatRoomCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = await _chatRoomRepository.GetByName(request.ChatRoomName) 
                ?? throw new BadRequestException($"Chat with the name {request.ChatRoomName} does not exist.");

            if (chatRoom.IsPrivate )
            {
                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new BadRequestException("Please, enter password!");

                var password = PasswordHasherHelper.HashPassword(request.Password);

                if (chatRoom.Password != password)
                    throw new BadRequestException("Invalid password!");
            }

            var newChatUser = new ApplicationUserChatRoom()
            {
                ApplicationUserId = request.UserId,
                ChatRoomId = chatRoom.Id
            };

            if (await _chatUserRepository.IsExist(newChatUser))
                throw new BadRequestException("You are already in this chat.");

            await _chatUserRepository.Insert(newChatUser);
            return chatRoom.Id;
        }
    }
}
