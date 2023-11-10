using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;

namespace Cit.Apps.Licensing.Application.Features.Plans.Queries.GetAllPlansQuery
{
    public record GetAllPlansQuery : IRequest<ResultModel<IEnumerable<SubscriptionPlanReadModel>>> { }

    public class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, ResultModel<IEnumerable<SubscriptionPlanReadModel>>>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllPlansQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<ResultModel<IEnumerable<SubscriptionPlanReadModel>>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.Repository<SubscriptionPlan>().Entities.ToList();
            if (result is not null)
            {
                return Result<IEnumerable<SubscriptionPlanReadModel>>.ReturnResult(200, "Plans Retrieved", _mapper.Map<IEnumerable<SubscriptionPlanReadModel>>(result));
            }

            else
            {
                return Result<IEnumerable<SubscriptionPlanReadModel>>.ReturnResult(204, "Zero Plans found", null);
            }

        }

    }


}
