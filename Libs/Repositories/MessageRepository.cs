namespace Libs.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        public List<Message> getMessageListByRomId(int roomId, int numRecord, int skip);
        public Message getMessageById(int id);
        public Message insertMessageByRomId(Message message);
        public bool deleteMessageByRomId(int romId);
        public bool deleteMessageById(int id);
    }
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Message getMessageById(int id)
        {
            return _dbContext.Message.FirstOrDefault(i => i.Id == id);
        }

        public List<Message> getMessageListByRomId(int romId, int numRecord, int skip)
        {
            return _dbContext.Message.Where(i => i.RoomId == romId).OrderByDescending(i => i.CreateDate).Skip(skip).Take(numRecord).ToList();
        }

        public Message insertMessageByRomId(Message message)
        {
            _dbContext.Add(message);
            return message;
        }
        public bool deleteMessageByRomId(int romId)
        {
            List<Message> mes = _dbContext.Message.Where(i => i.RoomId == romId).ToList();
            _dbContext.RemoveRange(mes);
            return true;
        }

        public bool deleteMessageById(int id)
        {
            Message mes = _dbContext.Message.FirstOrDefault(i => i.Id == id);
            _dbContext.Remove(mes);
            return true;
        }
    }
}
