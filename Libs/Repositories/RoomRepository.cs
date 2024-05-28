namespace Libs.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        public List<Room> getRoomsList(string userId);
        public Room getRoomById(int id);

        public Room getRoomByUserId(string userid1, string userid2);
        public void deleteRoomById(int roomId);
    }
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public List<Room> getRoomsList(string userId)
        {
            return _dbContext.Rooms.Where(i => i.UserId1 == userId || i.UserId2 == userId).OrderByDescending(i => i.ThoiGian).ToList();
        }

        public Room getRoomById(int id)
        {
            return _dbContext.Rooms.FirstOrDefault(room => room.Id == id);
        }

        public void deleteRoomById(int roomId)
        {
            Room rom = _dbContext.Rooms.FirstOrDefault(i => i.Id == roomId);
            _dbContext.Remove(rom);
        }

        public Room getRoomByUserId(string userid1, string userid2)
        {
            Room room = _dbContext.Rooms.FirstOrDefault(i => i.UserId1 == userid1 && i.UserId2 == userid2);

            if (room == null)
            {
                room = _dbContext.Rooms.FirstOrDefault(i => i.UserId1 == userid2 && i.UserId2 == userid1);
            }

            return room;

        }

    }
}
