using JobService.Services.UserService;
using JobService.Services.VacancyService;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    public class JobVacancyController : Controller
    {
        private readonly ILogger<JobVacancyController> _logger;
        private readonly IVacancyService _vacancyService;
        private readonly IUserService _userService;

        public JobVacancyController(ILogger<JobVacancyController> logger, IVacancyService vacancyService, IUserService userService)
        {
            _logger = logger;
            _vacancyService = vacancyService;
            _userService = userService;
        }

        public IActionResult Index(int? jobVacancyId)
        {
            if (HttpContext.Request.Cookies.ContainsKey("mode"))
            {
                string mode = HttpContext.Request.Cookies["mode"]!;

                if (mode.Equals("jobseeker"))
                {
                    ViewBag.Mode = "jobseeker";
                }
                else if (mode.Equals("employer"))
                {
                    ViewBag.Mode = "employer";
                }
            }
            else
            {
                HttpContext.Response.Cookies.Append("mode", "jobseeker");
                ViewBag.Mode = "jobseeker";
            }

            if (jobVacancyId == null)
            {
                return Redirect("~/");
            }
            else
            {
                ViewBag.Own = false;
                var vacancy = _vacancyService.GetVacancy((int)jobVacancyId);
                if (vacancy != null && HttpContext.User.Identity != null)
                {
                    if (vacancy.User!.Username == HttpContext.User.Identity.Name)
                    {
                        ViewBag.Own = true;
                        ViewBag.SuitableJobSeekers = _vacancyService.vacancySuitableJobSeekers(jobVacancyId);
                    }
                }

                return View(vacancy);
            }
        }

        public IActionResult Search(string? searchWord, int? settlementId)
        {
            return View(_vacancyService.SearchJobVacancies(searchWord, settlementId));
        }
    }
}
