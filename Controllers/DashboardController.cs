using Microsoft.AspNetCore.Mvc;

namespace UITraining.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
