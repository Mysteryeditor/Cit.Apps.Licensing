using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Common;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.Features.Plans.Commands.CreatePlanCommand
{
    public class CreatePlanCommand : BaseEntity, IRequest<ResultModel<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BillingCycleType { get; set; }
        public decimal Cost { get; set; }
        public int ApplicationId { get; set; }
    }

    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultModel<string>> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.Repository<SubscriptionPlan>().AddAsync(_mapper.Map<SubscriptionPlan>(request));
                await _unitOfWork.Save(cancellationToken);
                return await Result<string>.ReturnResult(200, "Created Successfully", null);

            }
            catch (Exception ex)
            {
                return await Result<string>.ReturnResult(500, ex.Message, null);
            }
        }
    }
}
