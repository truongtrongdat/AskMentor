using Libs.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AskMentor.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly AskMentor.Helper.Helper helper;
        private readonly RoomService roomService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TopicService _topicService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context, Helper.Helper helper, RoomService roomService, IHttpContextAccessor httpContextAccessor, TopicService topicService, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _userManager = userManager;
            _dbContext = context;
            this.helper = helper;
            this.roomService = roomService;
            _httpContextAccessor = httpContextAccessor;
            _topicService = topicService;
            _hostingEnvironment = hostingEnvironment;
        }

        public  IActionResult Index()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.IsAuthenticated = true;
            }
            var category = helper.GetFields(0,5);
            return View(category);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Leaner()
        {
            string userId = await helper.GetUserId(User.Identity.Name);
            ViewBag.userId = userId;
            List<ApplicationUser> Mentors = await helper.GetUserByRole("Mentor");
            return View(Mentors);
        }


        [HttpGet("field/{id}")]
        public async Task<IActionResult> Field(int id)
        {

            List<Topic> topics = _topicService.getTopicsListByFieldId(id);
            return View(topics);
        }
        [HttpGet("topic/{id}")]
        public async Task<IActionResult> Topic(int id)
        {

            List<ApplicationUser> Mentors = await helper.GetUserByRole("Mentor");
            List<ApplicationUser> MentorsInTopic = new List<ApplicationUser>();
            foreach (var item in Mentors)
            {
                if (item.TopicId == id)
                {
                    MentorsInTopic.Add(item);
                }
            }
            return View(MentorsInTopic);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Chat()
        {
            string userId = await helper.GetUserId(User.Identity.Name);
            ViewBag.userId = userId;
            List<Room> listRoom = roomService.getRoomsList(userId);
            return View(listRoom);
        }

        public async Task<IActionResult> Profile(string id)
        {
            var model = await helper.GetUserById(id);
            return View(model);
        }

        public async Task<IActionResult> EditProfile()
        {
            var userId = await helper.GetUserId(User.Identity.Name);
            var model = await helper.GetUserById(userId);

            var fieldE = helper.GetFields(0, 999);
            var topicE = helper.GetTopics(0, 999);
            ViewBag.Fields = null;
            ViewBag.Topics = null;
            if (fieldE != null)
            {
                ViewBag.Fields = fieldE.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name,
                }).ToList();
            }
            if (topicE != null)
            {
                ViewBag.Topics = topicE.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name,
                }).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(ApplicationUser model) 
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (model.CertificationFile != null)
            {
                model.Certification = await helper.UpLoadCertificate(model.CertificationFile);
                user.Certification = model.Certification;
            }

            if (model.AvatarFile != null)
            {
                model.Avt = await helper.UpLoadAvatar(model.AvatarFile);
                user.Avt = model.Avt;
            }

            user.Address = model.Address;
            user.Name = model.Name;
            user.Major = model.Major;
            user.University = model.University;
            await _userManager.UpdateAsync(user);
            TempData["Successful"] = true;
            return View(model);
        }

        public async Task<IActionResult> DownloadFile(string id)
        {
            var model = await helper.GetUserById(id);
            string webRootPath = Path.Combine(_hostingEnvironment.WebRootPath, "upload/certificate");
            string filePath = Path.Combine(webRootPath, helper.GetCertificationName(model.Certification));
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf");
        }

        public async Task<IActionResult> Search(string q="")
        {
            List<ApplicationUser> Mentors = await helper.GetUserByRole("Mentor");
            var Ids = Mentors.Select(m => m.Id).ToList() ?? new List<string>();
            var iQueryable = _userManager.Users.Include(m => m.Field).Include(m => m.Topic)
                .Where(m => m.Field != null && m.Topic != null && Ids.Contains(m.Id));
            if(!string.IsNullOrWhiteSpace(q))
            {
                iQueryable = iQueryable.Where(m => EF.Functions.Like(m.Topic.Name.ToLower(), $"%{q.ToLower()}%")
                 || EF.Functions.Like(m.Field.Name.ToLower(), $"%{q.ToLower()}%"));
            }
            var models = await iQueryable.ToListAsync();
            ViewBag.KeySearch = q;
            return View(models);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("chatTopic/{id}")]
        public IActionResult TopicChat(int id)
        {
            Topic topic = _dbContext.Topics.Find(id);
            return View(topic);
        }
    }
}
