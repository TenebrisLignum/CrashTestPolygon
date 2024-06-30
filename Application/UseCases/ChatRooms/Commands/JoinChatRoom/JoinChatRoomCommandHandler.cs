using Application.Common;
using Application.Messaging;
using Domain.Entities.Chats;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using Mapster;

namespace Application.UseCases.ChatRooms.Commands.JoinChatRoom
{
    public sealed class JoinChatRoomCommandHandler : ICommandHandler<JoinChatRoomCommand, ChatRoomViewModel>
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

        public async Task<ChatRoomViewModel> Handle(JoinChatRoomCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = await _chatRoomRepository.GetByName(request.ChatRoomName) 
                ?? throw new BadRequestException($"Chat with the name {request.ChatRoomName} does not exist.");

            var newChatUser = new ApplicationUserChatRoom()
            {
                ApplicationUserId = request.UserId,
                ChatRoomId = chatRoom.Id
            };

            if (!await _chatUserRepository.IsExist(newChatUser) && chatRoom.IsPrivate)
            {
                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new BadRequestException("Please, enter password!");

                var password = PasswordHasherHelper.HashPassword(request.Password);

                if (chatRoom.Password != password)
                    throw new BadRequestException("Invalid password!");

                await _chatUserRepository.Insert(newChatUser);
            }

            var chatRoomVM = chatRoom.Adapt<ChatRoomViewModel>();

            return chatRoomVM;
        }
    }
}
