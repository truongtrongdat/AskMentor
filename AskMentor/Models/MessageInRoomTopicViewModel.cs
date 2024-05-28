namespace AskMentor.Models
{
    public class MessageInRoomTopicViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int RoomTopicId { get; set; }
        public string CreateDate { get; set; }
        public string userEmail { get; set; }
        public string UserId { get; set; }
    }
}
