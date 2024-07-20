using Application.Helpers;
using Application.UseCases.ChatRooms.Commands.CreateChatRoom;
using Application.UseCases.ChatRooms.Queries.GetChatRoomsByUserId;
using Domain.Entities.Chats;
using Mapster;

namespace Application.Mapping.Chats
{
    public static class ChatRoomMapper
    {
        public static ChatRoom MapToChatRoom(CreateChatRoomCommand request)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<CreateChatRoomCommand, ChatRoom>()
                .Map(dest => dest.Id, src => Guid.NewGuid().ToString())
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.IsPrivate, src => src.IsPrivate)
                .Map(dest => dest.Password, src => src.IsPrivate ? PasswordHasherHelper.HashPassword(src.Password) : null)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.CreatedDate, src => DateTime.UtcNow);

            return request.Adapt<ChatRoom>(config);
        }

        public static List<ChatRoomItemViewModel> MapChatRoomsToItemViewModels(List<ChatRoom> chatRooms)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<ChatRoom, ChatRoomItemViewModel>()
                .Map(dest => dest.UsersCount, src => src.UserChatRooms.Count);

            return chatRooms.Adapt<List<ChatRoomItemViewModel>>(config);
        }
    }
}
