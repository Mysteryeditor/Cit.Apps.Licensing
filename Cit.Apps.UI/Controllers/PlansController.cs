using AutoMapper;
using Cit.Apps.Licensing.Application.Features.Plans.Commands.CreatePlanCommand;
using Cit.Apps.Licensing.Application.Features.Plans.Queries.GetAllPlansQuery;
using Cit.Apps.Licensing.UI.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cit.Apps.Licensing.UI.Controllers
{
    public class PlansController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public PlansController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<IActionResult> GetAllPlans()
        {
            var result =await _mediator.Send(new GetAllPlansQuery());
            if(result.Statuscode==200)return View(_mapper.Map<IEnumerable<SubscriptionPlansViewModel>>(result.Data));
            return View();
        }

        public IActionResult CreatePlan()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan(SubscriptionPlansViewModel newPlan)
        {
            var result = await _mediator.Send(new CreatePlanCommand
            {
                Name= newPlan.Name,
                ApplicationId = newPlan.ApplicationId,
                BillingCycleType = newPlan.BillingCycleType,
                Cost = newPlan.Cost,
                Description = newPlan.Description,
                CreatedAt=DateTime.Now,
                CreatedBy=1,
                ModifiedBy=1
            });

            if (result.Statuscode == 200)
            {
                return RedirectToAction("GetAllPlans");
            }
            return View(newPlan);
        }
    }
}
