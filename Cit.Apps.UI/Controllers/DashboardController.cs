using AutoMapper;
using Cit.Apps.Licensing.Application.Features.ApplicationFeature.Queries.GetAllApplicationsQuery;
using Cit.Apps.Licensing.Application.Features.Clients.Queries.GetAllClientsQuery;
using Cit.Apps.Licensing.UI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cit.Apps.Licensing.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public DashboardController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public IActionResult HomePage()
        {
            string name=HttpContext.Session.GetString("username");
            TempData["name"] = name;
            return View();
        }

        public IActionResult Administration()
        {
            string? name = HttpContext.Session.GetString("username");

            return View();
        }

        public async Task<IActionResult> Applications()
        {
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
        public IActionResult Index()
        {
            return View();
        }

    
    }
}
