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

namespace Cit.Apps.Licensing.Application.Features.Subscriptions.Queries.GetSubscriptionByClientQuery
{
    public record GetSubscriptionByClientQuery:IRequest<ResultModel<IEnumerable<ClientSubscriptionReadModel>>>
    {
        public int Id { get; set; }
        public string ColumnName { get; set; }

    }

    public class GetSubscriptionByClientQueryHandler : IRequestHandler<GetSubscriptionByClientQuery, ResultModel<IEnumerable<ClientSubscriptionReadModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetSubscriptionByClientQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;            
        }
        public async Task<ResultModel<IEnumerable<ClientSubscriptionReadModel>>> Handle(GetSubscriptionByClientQuery request, CancellationToken cancellationToken)
        {
            //var data= _unitOfWork.Repository<ClientSubscription>().GetByExpression(request.ColumnName,request.Id);
            var data =await _unitOfWork.Repository<ClientSubscription>().GetSubscriptionDataByClient(request.Id);

            if (data is not null)
            {
               var result=await Result<IEnumerable<ClientSubscriptionReadModel>>.ReturnResult(200, "Data retrieved", _mapper.Map<IEnumerable<ClientSubscriptionReadModel>>(data));
                return result;
            }
            else
            {
                return await Result<IEnumerable<ClientSubscriptionReadModel>>.ReturnResult(204, "No Record Found", null);
            }
        
        }
    }
}
