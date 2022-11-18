using JobService.Services.SettlementService;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    public class SettlementController : Controller
    {
        private readonly ILogger<SettlementController> _logger;
        private readonly ISettlementService _settlementService;

        public SettlementController(ILogger<SettlementController> logger, ISettlementService settlementService)
        {
            _logger = logger;
            _settlementService = settlementService;
        }

        [HttpPost]
        public JsonResult GetRegionSettlements(int regionId)
        {
            return new JsonResult(Ok(_settlementService.GetRegionSettlementsList(regionId)));
        }

        [HttpGet]
        public JsonResult GetRegions()
        {
            return new JsonResult(Ok(_settlementService.GetRegionsList()));
        }
    }
}
