namespace Libs.Entity
{
    public class Evaluate
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public string MentorId { get; set; }
        public string? Content { get; set; }
        public ApplicationUser User { get; set; }
    }
}
