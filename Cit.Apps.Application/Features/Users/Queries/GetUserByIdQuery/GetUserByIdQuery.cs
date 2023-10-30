using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.Features.Users.Queries.GetUserByIdQuery
{
    public record GetUserByIdQuery : IRequest<ResultModel<UserReadModel>>
    {
        public int id;

        public GetUserByIdQuery(int id)
        {
            this.id = id;
        }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResultModel<UserReadModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultModel<UserReadModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var record =await _unitOfWork.Repository<User>().GetByIdAsync(request.id);
            if (record is not null)
            {
                var data = _mapper.Map<UserReadModel>(record);
                var result =await Result<UserReadModel>.ReturnResult(200, "Retrieval Successful", data);
                return result;
            }

            else
            {
                var result= await Result<UserReadModel>.ReturnResult(400, "User not found", null);
                return result;
            }
        }
    }
}
