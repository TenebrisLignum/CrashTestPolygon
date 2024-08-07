﻿using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IChatRoomApplicationUserRepository
    {
        Task<bool> IsExistByFields(string userId, string chatRoomId);
        Task<bool> IsExist(ApplicationUserChatRoom entity);
        Task Insert(ApplicationUserChatRoom entity);
    }
}
