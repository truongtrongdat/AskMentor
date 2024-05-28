using Libs.Entity;

namespace AskMentor.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MessageService messageService;
        private readonly RoomService roomService;
        private readonly AskMentor.Helper.Helper _helper;
        public ChatHub(MessageService messageService, RoomService roomService, AskMentor.Helper.Helper helper)
        {
            this.messageService = messageService;
            this.roomService = roomService;
            _helper = helper;
        }
        public async Task SendMessage(string fromId, string toId, int roomId, string message)
        {
            messageService.insertMessage(new Message
            {
                Content = message,
                RoomId = roomId,
                CreateDate = DateTime.Now,
                FromUserId = fromId,
            });
            await Clients.All.SendAsync("ReceiveMessage", fromId, toId, message);
        }

        public async Task GetHistory(int roomId, int skip)
        {
            List<Message> messages = messageService.getMessageListByRomId(roomId, 20, skip);
            await Clients.All.SendAsync("HistoryChatRecord", messages);
        }

        public async Task JoinRom(string fromId, string toId)
        {
            Room room = roomService.getRoomByUserId(fromId, toId);
            List<Message> messages = new List<Message>();
            if (room == null)
            {
                roomService.insertRoom(new Room
                {
                    ThoiGian = DateTime.Now,
                    UserId1 = fromId,
                    UserId2 = toId
                });
            }
            else
            {
                messages = messageService.getMessageListByRomId(room.Id, 20, 0);
            }
            await Clients.All.SendAsync("History", fromId, toId, room.Id, messages);
        }

        public async Task GetName(string userId, string fromId)
        {

            string name = await _helper.GetNameUserById(userId);
            await Clients.All.SendAsync("NameUser", fromId, name);
        }
    }
}
