using Application.Messaging;
using Application.UseCases.ChatMessages.Queries.LoadChatMessages;
using Domain.Exceptions;
using Domain.Repositories.Chats;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.ChatMessages.Queries.GetMessageQuery
{
    public sealed class GetChatMessageQueryHandler : IQueryHandler<GetChatMessageQuery, ChatMessageViewModel>
    {
        private readonly IChatMessagesRepository _chatMessagesRepository;

        public GetChatMessageQueryHandler(
            IChatMessagesRepository chatMessagesRepository
        )
        {
            _chatMessagesRepository = chatMessagesRepository;
        }

        public async Task<ChatMessageViewModel> Handle(GetChatMessageQuery request, CancellationToken cancellationToken)
        {
            var messageVM = await
                _chatMessagesRepository
                .GetAsQueryable()
                .Include(cm => cm.Sender)
                .Select(cm => new ChatMessageViewModel()
                {
                    Id = cm.Id,
                    OwnerId = cm.SenderId,
                    OwnerName = cm.Sender.UserName,
                    CreatedDate = cm.CreatedDate,
                    Text = cm.Text
                })
                .Where(cm => cm.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new BadRequestException("Message is not found!");

            return messageVM;
        }
    }
}
