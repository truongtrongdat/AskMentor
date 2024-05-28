using Libs.Services;

namespace AskMentor.Hubs
{
    public class ChatTopicHub: Hub
    {
        private readonly AskMentor.Helper.Helper _helper;
        private readonly RoomTopicService _roomTopicService;
        private readonly MessageInRoomTopicService _messageInRoomTopicService;

        public ChatTopicHub(AskMentor.Helper.Helper helper, RoomTopicService roomTopicService, MessageInRoomTopicService messageInRoomTopicService)
        {
            _helper = helper;
            _roomTopicService = roomTopicService;
            _messageInRoomTopicService = messageInRoomTopicService;
        }

        public async Task SendMessageTopic(string fromUserId, int topicId, string message)
        {
           int roomId = _roomTopicService.GetRoomTopicIdByTopicId(topicId);

            _messageInRoomTopicService.insertMessage(new MessageInRoomTopic
            {
                Content = message,
                RoomTopicId = roomId,
                CreateDate = DateTime.Now,
                UserId = fromUserId,
            });

            await Clients.All.SendAsync("ReceiveMessageTopic", fromUserId, topicId, message);
        }

        public async Task GetHistoryTopic(int TopicId)
        {
            
            await Clients.All.SendAsync("HistoryChatRecordTopic", TopicId, "[]");
        }

        public async Task JoinRomTopic(string fromUserId, int TopicId)
        {
            try
            {
                int roomId = _roomTopicService.CreateOrUpdateRoomTopic(fromUserId, TopicId);
                List<MessageInRoomTopic> messageInRoomTopics = _messageInRoomTopicService.getMessageInRoomTopicListByRomId(roomId, 100, 0);
                
                List<MessageInRoomTopicViewModel> messageInRoomTopicsViewModel = new List<MessageInRoomTopicViewModel>();
                foreach (var item in messageInRoomTopics)
                {
                    messageInRoomTopicsViewModel.Add(new MessageInRoomTopicViewModel
                    {
                        Id = item.Id,
                        Content = item.Content,
                        RoomTopicId = item.RoomTopicId,
                        CreateDate = item.CreateDate.ToString("dd/MM/yyyy HH:mm:ss"),
                        userEmail = await _helper.GetNameUserById(item.UserId),
                        UserId = item.UserId
                    });
                }

                await Clients.All.SendAsync("HistoryChatRecordTopic", TopicId, messageInRoomTopics);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
