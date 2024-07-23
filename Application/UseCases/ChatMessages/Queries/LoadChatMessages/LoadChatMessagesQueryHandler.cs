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

            var messages = await QueryingMessages(request, cancellationToken);

            var messagesVMs = ChatMessageMapper.MapChatMessagesToChatMessageViewModels(messages);

            var result = new ChatMessagesViewModel
            {
                Messages = messagesVMs,
                LastMessageId = messagesVMs.FirstOrDefault()?.Id ?? null
            };

            return result;
        }

        private async Task<List<ChatMessage>> QueryingMessages(LoadChatMessagesQuery request, CancellationToken cancellationToken)
        {
            var query = _chatMessagesRepository
                .GetAsQueryable()
                .Where(cm => cm.ChatRoomId == request.ChatRoomId)
                .Include(cm => cm.Sender)
                .OrderByDescending(x => x.CreatedDate)
                .AsQueryable();

            if (request.LastMessageId != null)
            {
                var lastMessage = await _chatMessagesRepository
                    .GetAsQueryable()
                    .Where(x => x.Id == request.LastMessageId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (lastMessage != null)
                {
                    query = query.Where(x => x.CreatedDate < lastMessage.CreatedDate);
                }
            }

            if (await query.AnyAsync(cancellationToken))
                return await query
                    .Take(20)
                    .OrderBy(x => x.CreatedDate)
                    .ToListAsync(cancellationToken);
            else
                return [];
        }
    }
}
