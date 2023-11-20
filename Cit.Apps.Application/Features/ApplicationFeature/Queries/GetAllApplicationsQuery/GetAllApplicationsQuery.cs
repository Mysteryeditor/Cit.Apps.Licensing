using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;

namespace Cit.Apps.Licensing.Application.Features.ApplicationFeature.Queries.GetAllApplicationsQuery
{
    public record GetApplicationQuery : IRequest<ResultModel<List<ApplicationReadModel>>> { }

    public class GetApplicationQueryHandler : IRequestHandler<GetApplicationQuery, ResultModel<List<ApplicationReadModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApplicationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<ResultModel<List<ApplicationReadModel>>> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.Repository<ApplicationData>().Entities.ToList();
            if (result is not null)
            {
                return Result<List<ApplicationReadModel>>.ReturnResult(200, "Retrieval Success", _mapper.Map<List<ApplicationReadModel>>(result));
            }
            else return Result<List<ApplicationReadModel>>.ReturnResult(204, "No Values found", null);
        }
    }
}

