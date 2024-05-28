using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Libs.Repositories
{
    public interface IRoomTopicRepository : IRepository<RoomTopic>
    {
        public List<RoomTopic> getRoomTopicList(int skip, int take);
        public RoomTopic getRoomTopicById(int id);
        public void deleteRoomTopicById(int roomId);
        public bool AddUserToRoomTopic(int roomId, string userId);
        public bool DeleteUserToRoomTopic(int roomId, string userId);
        public int CreateOrUpdateRoomTopic(string userId, int topicId);
        public int GetRoomTopicIdByTopicId(int topicId);
    }
    public class RoomTopicRepository : RepositoryBase<RoomTopic>, IRoomTopicRepository
    {
        public RoomTopicRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public List<RoomTopic> getRoomTopicList(int skip, int take)
        {
            return _dbContext.RoomTopic.Skip(skip).Take(take).ToList();
        }

        public RoomTopic getRoomTopicById(int id)
        {
            return _dbContext.RoomTopic.FirstOrDefault(room => room.Id == id);
        }

        public void deleteRoomTopicById(int roomId)
        {
            RoomTopic rom = _dbContext.RoomTopic.FirstOrDefault(i => i.Id == roomId);
            _dbContext.Remove(rom);
        }
        public bool AddUserToRoomTopic(int roomId, string userId)
        {
            RoomTopic rom = _dbContext.RoomTopic.FirstOrDefault(i => i.Id == roomId);

            if (rom == null)
            {
                return false;
            }

            List<String> users = JsonConvert.DeserializeObject<List<String>>(rom.ListUserId);

            if(users == null)
            {
                users = new List<string>();
                users.Add(userId);
                rom.ListUserId = JsonConvert.SerializeObject(users);

            }
            else
            {
                string check = users.FirstOrDefault(i=>i == userId);
                if(check == null)
                {
                    users.Add(userId);
                    rom.ListUserId = JsonConvert.SerializeObject(users);
                }
            }
            _dbContext.Update(rom);
            return true;

        }
        public bool DeleteUserToRoomTopic(int roomId, string userId)
        {
            RoomTopic rom = _dbContext.RoomTopic.FirstOrDefault(i => i.Id == roomId);

            if (rom == null)
            {
                return false;
            }

            List<String> users = JsonConvert.DeserializeObject<List<String>>(rom.ListUserId);

            if(users == null)
            {
                return false;
            }
            else
            {
                users.Remove(userId);
                rom.ListUserId = JsonConvert.SerializeObject(users);
                _dbContext.Update(rom);
                return true;
            }

        }
        public int CreateOrUpdateRoomTopic(string userId, int topicId)
        {
            RoomTopic rom = _dbContext.RoomTopic.FirstOrDefault(i => i.TopicId == topicId);

            if(rom == null)
            {
                rom = new RoomTopic();
                rom.TopicId = topicId;
                rom.ListUserId = JsonConvert.SerializeObject(new List<string>() { userId});
                rom.CreateDate = DateTime.Now;
                _dbContext.Add(rom);
                _dbContext.SaveChanges();
                return rom.Id;
            }
            else
            {
                List<String> users = JsonConvert.DeserializeObject<List<String>>(rom.ListUserId);
                if(users == null)
                {
                    users = new List<string>();
                    users.Add(userId);
                    rom.ListUserId = JsonConvert.SerializeObject(users);
                    _dbContext.Update(rom);
                    _dbContext.SaveChanges();
                    return rom.Id;
                }
                else
                {
                    string check = users.FirstOrDefault(i=>i == userId);
                    if(check == null)
                    {
                        users.Add(userId);
                        rom.ListUserId = JsonConvert.SerializeObject(users);
                        _dbContext.Update(rom);
                        _dbContext.SaveChanges();
                        return rom.Id;
                    }
                    else
                    {
                        return rom.Id;
                    }
                }   
            }
        }

        public int GetRoomTopicIdByTopicId(int topicId)
        {
            RoomTopic rom = _dbContext.RoomTopic.FirstOrDefault(i => i.TopicId == topicId);
            if(rom == null)
            {
                rom = new RoomTopic();
                rom.TopicId = topicId;
                rom.ListUserId = JsonConvert.SerializeObject(new List<string>());
                rom.CreateDate = DateTime.Now;
                _dbContext.Add(rom);
                _dbContext.SaveChanges();
                return rom.Id;
            }
            return rom.Id;
        }   

    }
}
