using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Cit.Apps.Licensing.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IService _service;//has the verify password method.

        public AuthenticationController(IService service,IToastNotification toastNotification)
        {
            _service = service;
            _toastNotification = toastNotification;
        }
        public IActionResult Login()
        {
            return View(new UserCredentialsViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserCredentialsViewModel userCredentials)
        {
            if(ModelState.IsValid)
            {
                var result=await _service.PasswordVerifier(userCredentials.Username, userCredentials.Password);

                if (result.Statuscode == 200)
                {
                    _toastNotification.AddSuccessToastMessage("Login successfull");
                    return RedirectToAction("HomePage", "Dashboard");
                }

                else if(result.Statuscode ==401)
                {
                    _toastNotification.AddErrorToastMessage("Incorrect Password");
                    return View();
                }

                else
                {
                    _toastNotification.AddErrorToastMessage("User Not Found");
                    return View();
                }
  
            }
            return View(userCredentials);
        }
    }
}
