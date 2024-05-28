using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class MessageInRoomTopicService
    {
        private ApplicationDbContext dbContext;
        private IMessageInRoomTopicRepository _messageInRoomTopicRepository;

        public MessageInRoomTopicService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this._messageInRoomTopicRepository = new MessageInRoomTopicRepository(dbContext);
        }

        public List<MessageInRoomTopic> getMessageInRoomTopicListByRomId(int roomId, int numRecord, int skip)
        {
            return this._messageInRoomTopicRepository.getMessageInRoomTopicListByRomId(roomId, numRecord, skip);
        }

        public void insertMessage(MessageInRoomTopic mess)
        {
            this._messageInRoomTopicRepository.insertMessageInRoomTopicByRomId(mess);
            
        }
    }
}
