﻿using Domain.Entities.Chats;

namespace Domain.Repositories.Chats
{
    public interface IChatRoomsRepository
    {
        IQueryable<ChatRoom> GetAsQueryable();
        Task<bool> IsExistById(string id);
        Task<bool> IsExistByName(string name);
        Task<ChatRoom?> GetByName(string name);
        Task<List<ChatRoom>> GetChatRoomsContainsUser(string userId, CancellationToken cancellationToken);
        Task Insert(ChatRoom chatRoom);
    }
}
