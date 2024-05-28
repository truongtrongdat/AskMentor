using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.ViewModel
{
    internal class TopicVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FieldId { get; set; }
        public Field Field { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Skill>? Skills { get; set; }

        public static implicit operator TopicVM(Topic item)
        {
            return new TopicVM
            {
                Id = item.Id,
                Name = item.Name,
                FieldId = item.FieldId,
            };
        }

        public static implicit operator Topic(TopicVM item)
        {
            return new Topic
            {
                Id = item.Id,
                Name = item.Name,
                FieldId = item.FieldId,
            };
        }

    }
}
