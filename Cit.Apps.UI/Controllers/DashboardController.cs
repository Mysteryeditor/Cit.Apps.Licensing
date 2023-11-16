using AutoMapper;
using Cit.Apps.Licensing.Application.Features.ApplicationFeature.Queries.GetAllApplicationsQuery;
using Cit.Apps.Licensing.Application.Features.Clients.Queries.GetAllClientsQuery;
using Cit.Apps.Licensing.UI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Collections.Generic;
using System.Security.Claims;

namespace Cit.Apps.Licensing.UI.Controllers
{
    
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        public DashboardController(IMediator mediator,IMapper mapper,IToastNotification toastNotification)
        {
            _mediator = mediator;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        
        public IActionResult HomePage()
        {
            ClaimsPrincipal principal = HttpContext.User;
            if (!principal.Identity.IsAuthenticated)
            {
                _toastNotification.AddErrorToastMessage("Session Expired");
                return RedirectToAction("Login", "Authentication");
            }
            string name=HttpContext.Session.GetString("username");
            TempData["name"] = name;
            return View();
        }

        public IActionResult Administration()
        {
            ClaimsPrincipal principal = HttpContext.User;
            if (!principal.Identity.IsAuthenticated)
            {
                _toastNotification.AddErrorToastMessage("Session Expired");
                return RedirectToAction("Login", "Authentication");
            }
            string? name = HttpContext.Session.GetString("username");

            return View();
        }

        public async Task<IActionResult> Applications()
        {
            ClaimsPrincipal principal = HttpContext.User;
            if (!principal.Identity.IsAuthenticated)
            {
                _toastNotification.AddErrorToastMessage("Session Expired");
                return RedirectToAction("Login", "Authentication");
            }
            var result = await _mediator.Send(new GetApplicationQuery());
            if(result.Statuscode == 200)
            {
                return View(_mapper.Map<IEnumerable<ApplicationViewModel>>(result.Data));
            }

            else if(result.Statuscode == 204)
            {
                //no values message determination
                return View();
            }
            //error handling
            return View();
        
        }

        public async Task<IActionResult> Clients()
        {
            ClaimsPrincipal principal = HttpContext.User;
            if (!principal.Identity.IsAuthenticated)
            {
                _toastNotification.AddErrorToastMessage("Session Expired");
                return RedirectToAction("Login", "Authentication");
            }
            var result = await _mediator.Send(new GetAllClientsQuery());
            if (result.Statuscode == 200)
            {
                    return View(_mapper.Map<IEnumerable<ClientViewModel>>(result.Data));
            }
            else if (result.Statuscode == 204)
            {
                //no values message determination
                return View();
            }
            //error handling
            return View();
        }   
    }
}
