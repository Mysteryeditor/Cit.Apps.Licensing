using AutoMapper;
using Cit.Apps.Licensing.Application.Features.Users.Queries.GetUsers;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Licensing.UI.ViewModels;
using Cit.Apps.Shared.Result;
using Cit.Apps.UI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;

namespace Cit.Apps.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IMediator mediator,IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var item=await _mediator.Send(new GetAllUsersQuery());

            //List<User> users=item.Data.ToList();
            return View(_mapper.Map<List<UserDetailsModel>>(item.Data));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}