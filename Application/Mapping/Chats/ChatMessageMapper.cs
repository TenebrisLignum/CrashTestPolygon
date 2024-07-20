using Application.UseCases.ChatMessages.Commands.SendChatMessage;
using Application.UseCases.ChatMessages.Queries.LoadChatMessages;
using Domain.Entities.Chats;
using Mapster;

namespace Application.Mapping.Chats
{
    public static class ChatMessageMapper
    {
        public static ChatMessage MapToChatMessage(SendChatMessageCommand request)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<SendChatMessageCommand, ChatMessage>()
                .Map(dest => dest.Id, src => Guid.NewGuid().ToString())
                .Map(dest => dest.Text, src => src.Text)
                .Map(dest => dest.SenderId, src => src.SenderId)
                .Map(dest => dest.ChatRoomId, src => src.ChatRoomId)
                .Map(dest => dest.CreatedDate, src => DateTime.UtcNow);

            return request.Adapt<ChatMessage>(config);
        }

        public static List<ChatMessageViewModel> MapChatMessagesToChatMessageViewModels(List<ChatMessage> request)
        {
            var config = new TypeAdapterConfig();

            config
                .NewConfig<ChatMessage, ChatMessageViewModel>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Text, src => src.Text)
                .Map(dest => dest.OwnerId, src => src.SenderId)
                .Map(dest => dest.OwnerName, src => src.Sender.UserName)
                .Map(dest => dest.CreatedDate, src => src.CreatedDate);

            return request.Adapt<List<ChatMessageViewModel>>(config);
        }
    }
}
