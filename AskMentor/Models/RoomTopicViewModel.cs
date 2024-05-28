namespace AskMentor.Models
{
    public class RoomTopicViewModel
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
