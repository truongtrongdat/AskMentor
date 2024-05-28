namespace AskMentor.Helper
{
    public class Helper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment _env;

        public Helper(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _userManager = userManager;
            this.context = context;
            _env = env;
        }

        public async Task<List<ApplicationUser>> GetUserByRole(string role)
        {
            return (List<ApplicationUser>)await _userManager.GetUsersInRoleAsync(role);
        }
        public List<ApplicationUser> GetUserByTopic(int topicId, int skip, int take)
        {
            return context.ApplicationUser.Where(i => i.TopicId == topicId).Skip(skip).Take(take).ToList();
        }

        public async Task<string> GetUserId(string email)
        {
            if (email == null || email == "")
            {
                return string.Empty;
            }
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            return user.Id;
        }

        public  async Task<ApplicationUser> GetUserById(string id) 
        {
            return await context.ApplicationUser.FirstOrDefaultAsync(m => m.Id == id);
        } 

        public async Task<string> GetNameUserByEmail(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            return user.Name;
        }

        public async Task<string> GetNameUserById(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            return user.Name;
        }

        public string GetTopicNameById(int id)
        {
            Topic topic = context.Topics.FirstOrDefault(i => i.Id == id);
            return topic.Name;
        }
        public string GetFieldNameById(int id)
        {
            Field field = context.Fields.FirstOrDefault(i => i.Id == id);
            return field.Name;
        }

        public async Task<string> UpLoadAvatar(IFormFile file)
        {
            string path = string.Empty;
            if (file != null)
            {
                string file_name = $"{DateTime.Now.Ticks.ToString()}{Path.GetExtension(file.FileName)}";
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot/upload/avatar", file_name);

                if (file.FileName == null)
                    path = "img.png";
                else
                    path = uploadsFolder;
                string filePath = Path.Combine(uploadsFolder, path);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return $"/upload/avatar/{file_name}";
            }
            else
            {
                return path;
            }

        }

        public async Task<string> UpLoadCertificate(IFormFile file)
        {
            string path = string.Empty;

            if (file != null)
            {
                string file_name = $"{DateTime.Now.Ticks.ToString()}{Path.GetExtension(file.FileName)}";
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot/upload/certificate", file_name);

                if (file.FileName == null)
                    path = "img.png";
                else
                    path = uploadsFolder;
                string filePath = Path.Combine(uploadsFolder, path);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return $"/upload/certificate/{file_name}";
            }
            else
            {
                return path;
            }
        }

        public async Task<string> GetUserRoleByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                // Nếu người dùng có nhiều vai trò, bạn có thể xử lý nhiều vai trò ở đây
                if (roles.Any())
                {
                    return roles.First(); // Trả về vai trò đầu tiên của người dùng
                }
            }

            return null; // Trả về null nếu không tìm thấy người dùng hoặc người dùng không có vai trò nào
        }

        public List<Topic> GetTopics(int skip, int take)
        {
            return context.Topics.Skip(skip).Take(take).ToList();
        }
        public List<Topic> GetTopicsByFieldId(int fieldId, int skip, int take)
        {
            return context.Topics.Where(i => i.FieldId == fieldId).Skip(skip).Take(take).ToList();
        }

        public List<Field> GetFields(int skip, int take)
        {
            return context.Fields.Skip(skip).Take(take).ToList();
        }

        public string GetCertificationName(string certificationPath)
        {
            if (string.IsNullOrEmpty(certificationPath)) return "";
            return certificationPath.Split("/").LastOrDefault();
        }

        public async Task<int> GetRatingOfMentor(string id, string mentorId)
        {
            var evaluatesE =  await context.Evaluates.FirstOrDefaultAsync(m => m.UserId == id && m.MentorId == mentorId);

            return evaluatesE == null ? 0 : evaluatesE.Rating;
        }
    }
}
