using System.ComponentModel.DataAnnotations;

namespace Libs.Entity
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string FromUserId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
