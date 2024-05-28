namespace Libs.Services
{
    public class RoomService
    {
        private ApplicationDbContext dbContext;
        private IRoomRepository RoomRepository;

        public RoomService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.RoomRepository = new RoomRepository(dbContext);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public List<Room> getRoomsList(string userId)
        {
            return RoomRepository.getRoomsList(userId);
        }
        public Room getRoomById(int id)
        {
            return this.RoomRepository.getRoomById(id);
        }
        public void insertRoom(Room Room)
        {
            dbContext.Rooms.Add(Room);
            Save();
        }
        public void updateRoom(Room Room)
        {
            RoomRepository.Update(Room);
            Save();
        }

        public Room getRoomByUserId(string userid1, string userid2)
        {
            return RoomRepository.getRoomByUserId(userid1, userid2);
        }

        public void deleteRoom(int id)
        {
            Room Room = RoomRepository.getRoomById(id);
            if (Room != null)
            {
                RoomRepository.deleteRoomById(id);
                Save();
            }
        }
    }
}
