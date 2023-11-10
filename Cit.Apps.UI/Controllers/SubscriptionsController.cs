using AutoMapper;
using Cit.Apps.Licensing.Application.Features.ApplicationFeature.Queries.GetAllApplicationsQuery;
using Cit.Apps.Licensing.Application.Features.Clients.Queries.GetAllClientsQuery;
using Cit.Apps.Licensing.Application.Features.Plans.Queries.GetAllPlansQuery;
using Cit.Apps.Licensing.Application.Features.Subscriptions.Commands.ExtendSubscriptionCommand;
using Cit.Apps.Licensing.Application.Features.Subscriptions.Commands.TerminateSubscriptionCommand;
using Cit.Apps.Licensing.Application.Features.Subscriptions.Queries.GetAllSubscriptionsQuery;
using Cit.Apps.Licensing.Application.Features.Subscriptions.Queries.GetSingleSubscriptionByIdQuery;
using Cit.Apps.Licensing.Application.Features.Subscriptions.Queries.GetSubscriptionByClientQuery;
using Cit.Apps.Licensing.UI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Cit.Apps.Licensing.UI.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        public SubscriptionsController(IMediator mediator, IMapper mapper, IToastNotification toastNotification)
        {
            _mapper = mapper;
            _mediator = mediator;
            _toastNotification = toastNotification;
        }
        public IActionResult AllSubscriptions()
        {
            return View();
        }
        public async Task<IActionResult> ClientSubscriptions(int Id)
        {
            var clientsubscriptions = await _mediator.Send(new GetSubscriptionByClientQuery { Id = Id });
            if (clientsubscriptions.Data.Count() != 0)
            {
                var viewData = _mapper.Map<List<ClientSubscriptionsViewModel>>(clientsubscriptions.Data);
                return View(viewData);
            }
            else
            {
                _toastNotification.AddErrorToastMessage("No subscriptions for the chosen client)");
                return RedirectToAction("Clients", "Dashboard");
            }


        }

        #region subscription creation
        public async Task<IActionResult> CreateSubscription()
        {
            var plans = await _mediator.Send(new GetAllPlansQuery());
            var clients = await _mediator.Send(new GetAllClientsQuery());
            var applications = await _mediator.Send(new GetApplicationQuery());
            if (plans.Data.Count() != 0 && clients.Data.Count() != 0 && applications.Data.Count != 0)
            {
                ClientSubscriptionWrapper clientSubData = new ClientSubscriptionWrapper
                {
                    applications = _mapper.Map<List<ApplicationViewModel>>(applications.Data),
                    clients=_mapper.Map<List<ClientViewModel>>(clients.Data),
                    plans= _mapper.Map<List<SubscriptionPlansViewModel>>(plans.Data)
                };
                return View(clientSubData);
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Error Occured");
                return RedirectToAction("Clients", "Dashboard");
            }
        
        }
        #endregion

        #region subscription extension

        public async Task<IActionResult> ExtendSubscription(int id)
        {
            //var allsubscriptions = await _mediator.Send(new GetAllSubscriptionsQuery());
            var currentSubscription = await _mediator.Send(new GetSingleSubscriptionByIdQuery { Id = id });
            return View(_mapper.Map<ClientSubscriptionsViewModel>(currentSubscription.Data));
        }

        public async Task<IActionResult> ExtendConfirmed(int id)
        {
            //var allsubscriptions = await _mediator.Send(new GetAllSubscriptionsQuery());
            var currentSubscription = await _mediator.Send(new ExtendSubscriptionCommand { Id = id });
            if (currentSubscription is not null && currentSubscription.Statuscode == 200)
            {
                _toastNotification.AddSuccessToastMessage(currentSubscription.Message);
                return RedirectToAction("ClientSubscriptions", new { Id = currentSubscription.Data });

            }
            return View(_mapper.Map<ClientSubscriptionsViewModel>(currentSubscription.Data));
        }
        #endregion

        #region subscription termination
        public async Task<IActionResult> TerminatedSubscriptions(int id)
        {
            var clientsubscriptions = await _mediator.Send(new GetSubscriptionByClientQuery { Id = id });
            if (clientsubscriptions.Data.Count() != 0)
            {
                var viewData = _mapper.Map<List<ClientSubscriptionsViewModel>>(clientsubscriptions.Data);
                return View(viewData);
            }
            else
            {
                _toastNotification.AddErrorToastMessage("No subscription history found for the chosen client)");
                return RedirectToAction("Clients", "Dashboard");
            }
        }
        public async Task<IActionResult> TerminateSubscription(int id)
        {
            var allsubscriptions = await _mediator.Send(new GetAllSubscriptionsQuery());
            var currentSubscription = await _mediator.Send(new TerminateSubscriptionCommand { Id = id });
            return RedirectToAction("ClientSubscriptions", new { Id = currentSubscription.Data });
        }
        #endregion



    }
}
