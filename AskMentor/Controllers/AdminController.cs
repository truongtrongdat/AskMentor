
using Newtonsoft.Json;

namespace AskMentor.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var user = _userManager.Users.ToList();
            return View(user);
        }

        [HttpGet("admin/TopicChat")]
        public IActionResult TopicChat()
        {
            List<RoomTopicViewModel> roomTopicViewModel = new List<RoomTopicViewModel>();
            foreach (var item in _dbContext.RoomTopic.ToList())
            {
                List<string> users = JsonConvert.DeserializeObject<List<string>>(item.ListUserId);
                List<ApplicationUser> usersInRoom = new List<ApplicationUser>();
                if(users != null)
                {
                    foreach (var user in users)
                    {
                        ApplicationUser userInRoom = _userManager.Users.FirstOrDefault(i => i.Id == user);
                        usersInRoom.Add(userInRoom);
                    }
                }
                
                RoomTopicViewModel roomTopic = new RoomTopicViewModel
                {
                    Id = item.Id,
                    TopicName = _dbContext.Topics.FirstOrDefault(i=>i.Id == item.TopicId)?.Name??"",
                    Users = usersInRoom
                };
                roomTopicViewModel.Add(roomTopic);
            }

            return View(roomTopicViewModel);
        }
    }
}
