﻿namespace Application.UseCases.ChatMessages.Queries.LoadChatMessages
{
    public class ChatMessagesViewModel
    {
        public List<ChatMessageViewModel> Messages { get; set; }

        public string LastMessageId { get; set; }
    }

    public class ChatMessageViewModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
