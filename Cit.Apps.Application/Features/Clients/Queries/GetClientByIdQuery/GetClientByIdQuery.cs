using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;

namespace Cit.Apps.Licensing.Application.Features.Clients.Queries.GetClientByIdQuery
{
    public record GetClientByIdQuery:IRequest<ResultModel<ClientReadModel>>
    {
        public int Id { get; set; }
    }

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ResultModel<ClientReadModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetClientByIdQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<ResultModel<ClientReadModel>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result =await _unitOfWork.Repository<Client>().GetByIdAsync(request.Id);
                if(result is not null)
                {
                    var returndata=_mapper.Map<ClientReadModel>(result);
                    return await Result<ClientReadModel>.ReturnResult(200, "Entity Retrieved", returndata);
                }

                else
                {
                    return await Result<ClientReadModel>.ReturnResult(404, "Entity Not Found", null);

                }
            }

            catch (Exception ex) {
                return await Result<ClientReadModel>.ReturnResult(500, ex.Message, null);
            }
        }
    }


}
