namespace Libs.Services
{
    public class RoomTopicService
    {
        private ApplicationDbContext dbContext;
        private IRoomTopicRepository RoomTopicRepository;

        public RoomTopicService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.RoomTopicRepository = new RoomTopicRepository(dbContext);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public List<RoomTopic> getRoomTopicList(int skip, int take)
        {
            return this.RoomTopicRepository.getRoomTopicList(skip, take);
        }
        public RoomTopic getRoomTopicById(int id)
        {
            return this.RoomTopicRepository.getRoomTopicById(id);
        }
        public void insertRoom(RoomTopic Room)
        {
            dbContext.RoomTopic.Add(Room);
            Save();
        }
        public void updateRoom(RoomTopic Room)
        {
            this.RoomTopicRepository.Update(Room);
            Save();
        }

        public int CreateOrUpdateRoomTopic(string userId, int topicId)
        {
            return this.RoomTopicRepository.CreateOrUpdateRoomTopic(userId, topicId);
        }

        public int GetRoomTopicIdByTopicId(int topicId)
        {
            return this.RoomTopicRepository.GetRoomTopicIdByTopicId(topicId);
        }
        
    }
}
