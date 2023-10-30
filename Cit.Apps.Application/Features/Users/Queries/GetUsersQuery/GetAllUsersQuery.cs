using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;

namespace Cit.Apps.Licensing.Application.Features.Users.Queries.GetUsers
{
    public record GetAllUsersQuery:IRequest<ResultModel<List<UserReadModel>>>;
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResultModel<List<UserReadModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<ResultModel<List<UserReadModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users=_unitOfWork.Repository<User>().Entities.ToList();
            var readmodel=_mapper.Map<List<UserReadModel>>(users);
            var result = Result<List<UserReadModel>>.ReturnResult(200, "Retrieval Success", readmodel);
            return result;
        }

    }
}
