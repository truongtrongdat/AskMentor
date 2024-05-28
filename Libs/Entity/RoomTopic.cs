using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class RoomTopic
    {
        [Key]
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string ListUserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
