using JobService.Models;
using JobService.Services.SettlementService;
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
                var user = _userService.GetUserDetailed(username);
                return View(new UserDto(user!.Username, user.FirstName, user.LastName, user.PhoneNumber, user.Email, user.InSearch, user.Settlement, user.HardSkills));
            }
        }

        public IActionResult Edit(string? username)
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

        [HttpPost]
        public IActionResult Edit(string username, string firstName, string lastName, string? phoneNumber, string? email, int? settlementId, List<int>? hardSkills) 
        {
            if(username == null)
            {
                throw new Exception("Not authorized user!");
            }
            if(username != User.Identity!.Name)
            {
                throw new Exception("User tried to edit not own profile!");
            }

            _userService.UpdateProfile(username, firstName, lastName, phoneNumber, email, settlementId, hardSkills);
            
            return LocalRedirect("~/profile/?username=" + username);
        }
    }
}
