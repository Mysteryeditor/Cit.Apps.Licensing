using Cit.Apps.Licensing.Application.Features.ApplicationFeature.Commands.CreateApplicationCommand;
using Cit.Apps.Licensing.Application.Features.ApplicationFeature.Commands.DeleteApplicationCommand;
using Cit.Apps.Licensing.Application.Features.ApplicationFeature.Commands.UpdateApplicationCommand;
using Cit.Apps.Licensing.Application.Features.ApplicationFeature.Queries.GetApplicationByIdQuery;
using Cit.Apps.Licensing.UI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cit.Apps.Licensing.UI.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IMediator _mediator;

        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;            
        }
        public IActionResult CreateApplication()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateApplication(string name)
        {
            var result = await _mediator.Send(new CreateApplicationCommand { Name = name });
            return View();
        }

        public async Task<IActionResult> EditApplication(int id)
        {
            var result = await _mediator.Send(new GetApplicationByIdQuery { Id = id });
            if(result is not null)
            {
                return View(new ApplicationViewModel { Id=id,Name=result.Data.Name});
            }
            return RedirectToAction("Applications","Dashboard");
        }

        [HttpPost]

        public async Task<IActionResult> EditApplication(ApplicationViewModel data)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateApplicationCommand  { Id = data.Id,ApplicationName=data.Name });
                if(result is not null)
                {
                    if (result.Statuscode == 200)
                    {
                        return RedirectToAction("Applications", "Dashboard");
                    }
                }
            }
            return View(data);
        }


        public async Task<IActionResult> DeleteApplication(int id)
        {
            var result = await _mediator.Send(new GetApplicationByIdQuery { Id = id});
            if (result is not null)
            {
                return View(new ApplicationViewModel { Id=result.Data.Id,Name=result.Data.Name});
            }
            return RedirectToAction("Applications","Dashboard");
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _mediator.Send(new DeleteApplicationCommand { Id = id });
            if (result is not null)
            {
                return RedirectToAction("Applications", "Dashboard");
            }
            return RedirectToAction("Applications", "Dashboard");
        }
    }
}
