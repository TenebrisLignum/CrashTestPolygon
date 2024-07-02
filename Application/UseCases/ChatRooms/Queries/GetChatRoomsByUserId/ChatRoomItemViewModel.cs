namespace Application.UseCases.ChatRooms.Queries.GetChatRoomsByUserId
{
    public class ChatRoomItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public int UsersCount { get; set; }
    }
}
