using AutoMapper;
using Cit.Apps.Licensing.Application.Features.Clients.Commands.CreateClientCommand;
using Cit.Apps.Licensing.Application.Features.Clients.Commands.DeleteClientCommand;
using Cit.Apps.Licensing.Application.Features.Clients.Commands.UpdateClientCommand;
using Cit.Apps.Licensing.Application.Features.Clients.Queries.GetClientByIdQuery;
using Cit.Apps.Licensing.UI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Cit.Apps.Licensing.UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        public ClientController(IMediator mediator,IMapper mapper,IToastNotification toastNotification)
        {
            _mediator= mediator;
            _mapper= mapper;
            _toastNotification = toastNotification;

        }
        public ActionResult CreateClient()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientViewModel clientData)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateClientCommand()
                {
                    Name = clientData.Name,
                    ContactPerson = clientData.ContactPerson,
                    //SubscriptionKey = clientData.SubscriptionKey,
                    ContactNumber = clientData.ContactNumber,
                    ModifiedBy = 1,
                    CreatedBy = 1
                });

                if (result.Statuscode == 200)
                {
                    return RedirectToAction("Clients", "Dashboard");
                }
            }
           
            return View();
        }

        public async Task<IActionResult> UpdateClient(int id)
        {
            var clientData=await _mediator.Send(new GetClientByIdQuery { Id = id });
            return View(_mapper.Map<CreateClientViewModel>(clientData.Data));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateClient(CreateClientViewModel updateData)
        {
            if (ModelState.IsValid)
            {
                var result =await _mediator.Send(new UpdateClientCommand
                {
                    Id= updateData.Id,
                    Name = updateData.Name,
                    ContactPerson = updateData.ContactPerson,
                    ContactNumber= updateData.ContactNumber
                });

                if (result.Statuscode == 200)
                {
                    _toastNotification.AddSuccessToastMessage("Updated Successfully");
                    return RedirectToAction("Clients","Dashboard");
                }

            }
            return View(updateData);
        }

        public async Task<IActionResult> DeleteClient(int id)
        {
            var clientData = await _mediator.Send(new GetClientByIdQuery { Id = id });
            return View(_mapper.Map<CreateClientViewModel>(clientData.Data));
        }

        public async Task<IActionResult> ConfirmDeleteClient(int id)
        {
            var deleteresult = await _mediator.Send(new DeleteClientCommand { Id = id });
            _toastNotification.AddSuccessToastMessage("Deleted Successfully");

            return RedirectToAction("Clients", "Dashboard");
        }


        public async Task<IActionResult> ViewClientDetails(int id)
        {
            var clientData = await _mediator.Send(new GetClientByIdQuery { Id = id });
            return View(_mapper.Map<ClientViewModel>(clientData.Data));
        }

    }
}
