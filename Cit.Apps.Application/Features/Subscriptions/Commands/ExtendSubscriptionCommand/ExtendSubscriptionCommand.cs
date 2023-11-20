using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System.Security.Cryptography;

namespace Cit.Apps.Licensing.Application.Features.Subscriptions.Commands.ExtendSubscriptionCommand
{
    public class ExtendSubscriptionCommand:IRequest<ResultModel<string>>
    {
        public int Id { get; set; }

    }

    public class ExtendSubscriptionCommandHandler : IRequestHandler<ExtendSubscriptionCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExtendSubscriptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        public async Task<ResultModel<string>> Handle(ExtendSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var chosenSubscription = await _unitOfWork.Repository<ClientSubscription>().GetSingleSubscription(request.Id);

                if(chosenSubscription is not null)
                {
                    chosenSubscription.StartDate=DateTime.Now;
                    chosenSubscription.IsActive=true;
                    if (chosenSubscription.SubscriptionPlan.BillingCycleType.ToLower() == "monthly")
                    {
                        chosenSubscription.EndDate=(DateTime.Now).AddMonths(1);
                    }
                    else if(chosenSubscription.SubscriptionPlan.BillingCycleType.ToLower() == "yearly")
                    {
                        chosenSubscription.EndDate = (DateTime.Now).AddYears(1);
                    }
                    _unitOfWork.Save(cancellationToken);
                }
                return await Result<string>.ReturnResult(200, "Update Successfull", chosenSubscription.ClientId.ToString());

            }
            catch (Exception ex) {
                return await Result<string>.ReturnResult(500, ex.Message, null);
            }
        }
    }
}
