
namespace Libs.Services
{
    public class TopicService
    {
        private ApplicationDbContext dbContext;
        private ITopicRepository TopicRepository;

        public TopicService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.TopicRepository = new TopicRepository(dbContext);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public List<Topic> getTopicsList()
        {
            return TopicRepository.getTopicsList();
        }
        public List<Topic> getTopicsListByFieldId(int fieldId)
        {
            return TopicRepository.getTopicsListByFieldId(fieldId);
        }
        public Topic getTopicById(int id)
        {
            return this.TopicRepository.getTopicById(id);
        }
        public void insertTopic(Topic Topic)
        {
            dbContext.Topics.Add(Topic);
            Save();
        }
        public void updateTopic(Topic Topic)
        {
            TopicRepository.Update(Topic);
            Save();
        }
        public void deleteTopic(int id)
        {
            Topic Topic = TopicRepository.getTopicById(id);
            if (Topic != null)
            {
                TopicRepository.Delete(Topic);
                Save();
            }
        }
    }
}
