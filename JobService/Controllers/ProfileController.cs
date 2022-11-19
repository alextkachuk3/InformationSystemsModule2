using JobService.Models;
using JobService.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IUserService _userService;

        public ProfileController(ILogger<ProfileController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index(string? username)
        {
            if (username == null)
            {
                return LocalRedirect("~/");
            }
            else
            {
                var user = _userService.GetUser(username);
                return View(new UserDto(user!.Username, user.FirstName, user.LastName, user.PhoneNumber, user.Email, user.InSearch, user.Settlement, user.HardSkills));
            }
        }
    }
}
