using Microsoft.AspNetCore.Mvc;
using JobService.Services.VacancyService;
using Microsoft.AspNetCore.Authorization;
using JobService.Services.SettlementService;

namespace JobService.Controllers
{
    public class EmployerController : Controller
    {
        private readonly ILogger<EmployerController> _logger;
        private readonly IVacancyService _vacancyService;
        private readonly ISettlementService _settlementService;

        public EmployerController(ILogger<EmployerController> logger, IVacancyService vacancyService, ISettlementService settlementService)
        {
            _logger = logger;
            _vacancyService = vacancyService;
            _settlementService = settlementService;
        }

        public IActionResult Index()
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
                HttpContext.Response.Cookies.Append("mode", "employer");
                ViewBag.Mode = "employer";
            }

            if (!HttpContext.User!.Identity!.IsAuthenticated)
            {
                return LocalRedirect(Url.Action("Login", "Authorization")!);
            }

            var jobVacations = _vacancyService.UserJobVacancies(User.Identity!.Name!);

            return View(jobVacations);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(List<int>? vacationsIds)
        {
            if (vacationsIds == null)
                return Index();
            foreach (var id in vacationsIds)
            {                
                _vacancyService.DeleteVacancy(id, HttpContext.User!.Identity!.Name!);
            }
            return Index();
        }

        public IActionResult AddVacancy()
        {
            var regions = _settlementService.GetRegionsList();
            return View(regions);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddVacancy(string title, int? settlementId, string salary, string description)
        {
            var user = HttpContext.User.Identity;
            if (salary == null)
                salary = "0";
            _vacancyService.AddVacancy(user!.Name!, title, settlementId, int.Parse(salary), description);
            return LocalRedirect("~/employer");
        }

        [Authorize]
        public IActionResult EditVacancy(int jobVacancyId)
        {
            var vacancy = _vacancyService.GetVacancy(jobVacancyId);
            return View(vacancy);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditVacancy(int vacancyId, string title, int? settlementId, string salary, string description)
        {
            var user = HttpContext.User.Identity;
            if (salary == null)
                salary = "0";
            _vacancyService.EditVacancy(vacancyId, user!.Name!, title, settlementId, int.Parse(salary), description);
            return LocalRedirect("~/employer");
        }

        public IActionResult ToJobseekerMode()
        {
            HttpContext.Response.Cookies.Append("mode", "jobseeker");
            return LocalRedirect("~/");
        }

    }
}
