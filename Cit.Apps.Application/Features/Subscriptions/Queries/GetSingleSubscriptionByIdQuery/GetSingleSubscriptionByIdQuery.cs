using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System.Net.WebSockets;

namespace Cit.Apps.Licensing.Application.Features.Subscriptions.Queries.GetSingleSubscriptionByIdQuery
{
    public class GetSingleSubscriptionByIdQuery : IRequest<ResultModel<ClientSubscriptionReadModel>>
    {
        public int Id { get; set; }
    }

    public class GetSingleSubscriptionQueryHandler : IRequestHandler<GetSingleSubscriptionByIdQuery, ResultModel<ClientSubscriptionReadModel>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleSubscriptionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultModel<ClientSubscriptionReadModel>> Handle(GetSingleSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.Repository<ClientSubscription>().GetSingleSubscription(request.Id);
                if (result is not null)
                {
                    var returndata = _mapper.Map<ClientSubscriptionReadModel>(result);
                    return await Result<ClientSubscriptionReadModel>.ReturnResult(200, "Entity Retrieved", returndata);
                }

                else
                {
                    return await Result<ClientSubscriptionReadModel>.ReturnResult(404, "Entity Not Found", null);

                }
            }

            catch (Exception ex)
            {
                return await Result<ClientSubscriptionReadModel>.ReturnResult(500, ex.Message, null);
            }
        }


    }
}
