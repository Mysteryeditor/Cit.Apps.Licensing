using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;

namespace Cit.Apps.Licensing.Application.Features.Subscriptions.Commands.TerminateSubscriptionCommand
{
    public class TerminateSubscriptionCommand:IRequest<ResultModel<int>>
    {
        public int Id { get; set; }
    }

    public class TerminateSubscriptionCommandHandler : IRequestHandler<TerminateSubscriptionCommand, ResultModel<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TerminateSubscriptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;           
        }
        public async Task<ResultModel<int>> Handle(TerminateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var chosenSubscription = await _unitOfWork.Repository<ClientSubscription>().GetByIdAsync(request.Id);
            if (chosenSubscription is not null) {
            chosenSubscription.IsTerminated = true;
                try
                {
                    await _unitOfWork.Repository<ClientSubscription>().UpdateAsync(chosenSubscription);
                    await _unitOfWork.Save(cancellationToken);
                    return await Result<int>.ReturnResult(200, "updated successfully", chosenSubscription.ClientId);

                }
                catch (Exception ex)
                {
                    return await Result<int>.ReturnResult(500, ex.Message, 0);
                }
         
            }
            return await Result<int>.ReturnResult(404,"record not found", 0);
        }
    }
}
