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

    }
}
