using Application.Common;
using Application.Mapping.Chats;
using Application.Messaging;
using Domain.Entities.Chats;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.ChatMessages.Queries.LoadChatMessages
{
    public sealed class LoadChatMessagesQueryHandler : IQueryHandler<LoadChatMessagesQuery, ChatMessagesViewModel>
    {
        private readonly IChatMessagesRepository _chatMessagesRepository;
        private readonly IChatRoomsRepository _chatRoomRepository;
        private readonly IChatRoomApplicationUserRepository _chatUserRepository;

        public LoadChatMessagesQueryHandler
        (
            IChatMessagesRepository chatMessagesRepository,
            IChatRoomsRepository chatRoomsRepository,
            IChatRoomApplicationUserRepository chatUserRepository
        ) 
        {
            _chatMessagesRepository = chatMessagesRepository;
            _chatRoomRepository = chatRoomsRepository;
            _chatUserRepository = chatUserRepository;
        }
        public async Task<ChatMessagesViewModel> Handle(LoadChatMessagesQuery request, CancellationToken cancellationToken)
        {
            if (!await _chatRoomRepository.IsExistById(request.ChatRoomId))
                throw new BadRequestException("Chat room does not exist!");

            if (!await _chatUserRepository.IsExistByFields(request.UserId, request.ChatRoomId))
                throw new BadRequestException("You are not in this chat!");

            var query = _chatMessagesRepository.GetAsQueryable().Include(cm => cm.Sender);
            var messages = await PagedList<ChatMessage>.CreateAsync(query, request.Page, 20);

            var messagesVMs = ChatMessageMapper.MapChatMessagesToChatMessageViewModels(messages.Items);

            var result = new ChatMessagesViewModel
            {
                Messages = messagesVMs,
                Page = messages.Page,
                HasNextPage = messages.HasNextPage
            };

            return result;
        }
    }
}
