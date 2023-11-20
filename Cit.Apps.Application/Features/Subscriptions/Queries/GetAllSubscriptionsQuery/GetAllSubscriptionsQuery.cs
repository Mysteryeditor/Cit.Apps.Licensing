using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.Features.Subscriptions.Queries.GetAllSubscriptionsQuery
{
    public record GetAllSubscriptionsQuery:IRequest<ResultModel<string>>
    {
    }

    public class GetAllSubscriptionsQueryHandler : IRequestHandler<GetAllSubscriptionsQuery, ResultModel<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllSubscriptionsQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;            
        }
        public async Task<ResultModel<string>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var result=await _unitOfWork.Repository<ClientSubscription>().GetSubscriptionData();
            var dataMap=_mapper.Map<List<ClientSubscriptionReadModel>>(result);
            return await Result<string>.ReturnResult(222, "", null);
        }
    }
}
