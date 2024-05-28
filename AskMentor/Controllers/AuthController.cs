using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace AskMentor.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }
        public IActionResult Register()
        {
            ViewBag.Auth = "Auth";
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.Auth = "Auth";
            return View();
        }

        public IActionResult EditProfile()
        {
            ViewBag.Auth = "Auth";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
