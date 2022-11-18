using JobService.Models;
using JobService.Services.VacancyService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobService.Controllers
{
    public class JobVacancyController : Controller
    {
        private readonly ILogger<JobVacancyController> _logger;
        private readonly IVacancyService _vacancyService;

        public JobVacancyController(ILogger<JobVacancyController> logger, IVacancyService vacancyService)
        {
            _logger = logger;
            _vacancyService = vacancyService;
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
                return LocalRedirect("~/");
            }
            else
            {
                var vacancy = _vacancyService.GetVacancy((int)jobVacancyId);
                return View(vacancy);
            }            
        }

        public IActionResult Search(string? searchWord, int? settlementId)
        {
            return View(_vacancyService.SearchJobVacancies(searchWord, settlementId));
        }
    }
}