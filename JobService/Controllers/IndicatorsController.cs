using JobService.Services.IndicatorsService;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    public class IndicatorsController : Controller
    {
        private readonly ILogger<IndicatorsController> _logger;
        private readonly IIndicatorsService _indicatorsService;

        public IndicatorsController(ILogger<IndicatorsController> logger, IIndicatorsService indicatorsService)
        {
            _logger = logger;
            _indicatorsService = indicatorsService;
        }

        [HttpPost]
        public JsonResult hardSkillYearSallaryIndicators(int hardSkillId)
        {
            return new JsonResult(Ok(_indicatorsService.GetHardSkillSalaryIndicators(hardSkillId, 12)));
        }
    }
}
