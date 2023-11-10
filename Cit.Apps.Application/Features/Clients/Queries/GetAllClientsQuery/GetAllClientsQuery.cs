using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;

namespace Cit.Apps.Licensing.Application.Features.Clients.Queries.GetAllClientsQuery
{
    public record GetAllClientsQuery:IRequest<ResultModel<IEnumerable<ClientReadModel>>>{}

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, ResultModel<IEnumerable<ClientReadModel>>>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllClientsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;            
        }
        public Task<ResultModel<IEnumerable<ClientReadModel>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.Repository<Client>().Entities.ToList();
            if(result is not null)
            {
                return Result<IEnumerable<ClientReadModel>>.ReturnResult(200, "Clients Retrieved", _mapper.Map<IEnumerable<ClientReadModel>>(result));
            }

            else
            {
                return Result<IEnumerable<ClientReadModel>>.ReturnResult(204, "Zero Clients found", null);
            }

        }

    }
}
