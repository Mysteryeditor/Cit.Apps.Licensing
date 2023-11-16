using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.UI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;

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
            ClaimsPrincipal principal = HttpContext.User;
            if(principal.Identity.IsAuthenticated)
            {
           
                return RedirectToAction("Homepage", "Dashboard");
            }
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
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, userCredentials.Username)
                        //new Claim("OtherProperties")


                    };
                    ClaimsIdentity claimsIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties() { 
                    AllowRefresh = true,
                    //IsPersistent=mod
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),properties);
                    HttpContext.Session.SetString("username",userCredentials.Username);
                    HttpContext.Session.SetString("userId", result.Data);
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _toastNotification.AddSuccessToastMessage("Logged Out Successfully");
            return RedirectToAction("Login");
        }
    }
}
