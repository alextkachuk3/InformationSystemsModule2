using JobService.Services.HardSkillService;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    public class HardSkillController : Controller
    {
        private readonly ILogger<SettlementController> _logger;
        private readonly IHardSkillService _hardSkillService;

        public HardSkillController(ILogger<SettlementController> logger, IHardSkillService hardSkillService)
        {
            _logger = logger;
            _hardSkillService = hardSkillService;
        }

        public JsonResult GetHardSkills()
        {
            return new JsonResult(Ok(_hardSkillService.GetHardSkills()));
        }

        [HttpPost]
        public JsonResult GetHardSkills(string searchWord)
        {
            return new JsonResult(Ok(_hardSkillService.GetHardSkills(searchWord).Select(s => new
            {
                id = s.Id,
                text = s.Name
            })));
        }
    }
}
