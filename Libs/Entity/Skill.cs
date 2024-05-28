namespace Libs.Entity
{
    public class Skill
    {
        public int Id { get; set; }
        public string NameSkill { get; set; }
        public string DescriptionSkill { get; set; }
        public int TopicId { get; set; }
        public Topic Topics { get; set; }
    }
}
