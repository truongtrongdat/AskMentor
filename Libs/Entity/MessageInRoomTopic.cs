using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class MessageInRoomTopic
    {
        [Key]
        public int Id { get; set; }
        public int RoomTopicId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
