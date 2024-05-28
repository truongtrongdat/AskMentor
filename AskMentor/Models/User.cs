namespace AskMentor.Models
{
    public class User
    {
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public string? Name { get; set; }
        public string? University { get; set; }
        public string? Major { get; set; }
        public string? Address { get; set; }
        public IFormFile? Avt { get; set; }
    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserInfo
    {
        public string Email { set; get; }
        public int FileId { set; get; }
        public int TopicId { set; get; }
        public string certificate { set; get; }
    }

}