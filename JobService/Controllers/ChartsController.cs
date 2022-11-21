using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    public class ChartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
