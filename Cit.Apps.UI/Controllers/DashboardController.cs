using Microsoft.AspNetCore.Mvc;

namespace Cit.Apps.Licensing.UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult Administration() {
            return View();
        }

        public IActionResult Applications()
        {
            return View();
        }

        public IActionResult Clients() {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
