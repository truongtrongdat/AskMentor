namespace Libs.Repositories
{
    public interface ITopicRepository : IRepository<Topic>
    {
        public List<Topic> getTopicsList();
        public List<Topic> getTopicsListByFieldId(int fieldId);
        public Topic getTopicById(int id);
    }
    public class TopicRepository : RepositoryBase<Topic>, ITopicRepository
    {
        public TopicRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public List<Topic> getTopicsList()
        {
            return _dbContext.Topics.ToList();
        }
        public List<Topic> getTopicsListByFieldId(int fieldId)
        {
            return _dbContext.Topics.Where(i => i.FieldId == fieldId).ToList();
        }

        public Topic getTopicById(int id)
        {
            return _dbContext.Topics.FirstOrDefault(topic => topic.Id == id);
        }
    }
}
