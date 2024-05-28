namespace Libs.Repositories
{
    public interface IMessageInRoomTopicRepository : IRepository<MessageInRoomTopic>
    {
        public List<MessageInRoomTopic> getMessageInRoomTopicListByRomId(int roomId, int numRecord, int skip);
        public MessageInRoomTopic insertMessageInRoomTopicByRomId(MessageInRoomTopic message);
        public bool deleteMessageInRoomTopicByRomId(int romId);
        public bool deleteMessageInRoomTopicById(int id);
    }
    public class MessageInRoomTopicRepository : RepositoryBase<MessageInRoomTopic>, IMessageInRoomTopicRepository
    {
        public MessageInRoomTopicRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public MessageInRoomTopic getMessageInRoomTopicById(int id)
        {
            return _dbContext.MessageInRoomTopic.FirstOrDefault(i => i.Id == id);
        }

        public List<MessageInRoomTopic> getMessageInRoomTopicListByRomId(int romId, int numRecord, int skip)
        {
            return _dbContext.MessageInRoomTopic.Where(i => i.RoomTopicId == romId).OrderByDescending(i => i.CreateDate).Skip(skip).Take(numRecord).ToList();
        }

        public MessageInRoomTopic insertMessageInRoomTopicByRomId(MessageInRoomTopic message)
        {
            _dbContext.Add(message);
            return message;
        }
        public bool deleteMessageInRoomTopicByRomId(int romId)
        {
            List<MessageInRoomTopic> mes = _dbContext.MessageInRoomTopic.Where(i => i.RoomTopicId == romId).ToList();
            _dbContext.RemoveRange(mes);
            return true;
        }

        public bool deleteMessageInRoomTopicById(int id)
        {
            MessageInRoomTopic mes = _dbContext.MessageInRoomTopic.FirstOrDefault(i => i.Id == id);
            _dbContext.Remove(mes);
            return true;
        }

      
    }
}
