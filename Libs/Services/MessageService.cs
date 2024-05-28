
namespace Libs.Services
{
    public class MessageService
    {
        private ApplicationDbContext dbContext;
        private IMessageRepository MessageRepository;

        public MessageService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.MessageRepository = new MessageRepository(dbContext);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public List<Message> getMessageListByRomId(int id, int numRecord, int skip)
        {
            return this.MessageRepository.getMessageListByRomId(id, numRecord, skip);
        }
        public Message getMessageById(int id)
        {
            return this.MessageRepository.getMessageById(id);
        }
        public void insertMessage(Message message)
        {
            MessageRepository.insertMessageByRomId(message);
            Save();
        }

        public void deleteMessageById(int id)
        {
            this.MessageRepository.deleteMessageById(id);
            Save();
        }
    }
}
