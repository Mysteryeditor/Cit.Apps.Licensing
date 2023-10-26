using Microsoft.AspNetCore.Mvc;

namespace Cit.Apps.Licensing.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult AllUsers()
        {
            return View();
        }
    }
}
