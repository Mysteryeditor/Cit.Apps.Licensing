using AutoMapper;
using Cit.Apps.Licensing.Application.Features.Users.Queries.GetUserByIdQuery;
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

namespace Cit.Apps.Licensing.Application.Features.ApplicationFeature.Queries.GetApplicationByIdQuery
{
    public record GetApplicationByIdQuery:IRequest<ResultModel<ApplicationReadModel>>
    {
        public int Id { get; set; }
    }

    public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, ResultModel<ApplicationReadModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApplicationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultModel<ApplicationReadModel>> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.Repository<ApplicationData>().GetByIdAsync(request.Id);
            if (record is not null)
            {
                var data = _mapper.Map<ApplicationReadModel>(record);
                var result = await Result<ApplicationReadModel>.ReturnResult(200, "Retrieval Successful", data);
                return result;
            }

            else
            {
                var result = await Result<ApplicationReadModel>.ReturnResult(404, "application not found", null);
                return result;
            }
        }
    }
}
