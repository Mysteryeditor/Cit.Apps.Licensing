using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.Features.ApplicationFeature.Commands.CreateApplicationCommand
{
    public record CreateApplicationCommand : IRequest<ResultModel<string>>
    {
        public string Name { get; set; }
    }

    public record CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, ResultModel<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateApplicationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<ResultModel<string>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            ApplicationData newData = new ApplicationData
            {
                Name = request.Name
            };
            try
            {
                var result = _unitOfWork.Repository<ApplicationData>().AddAsync(newData);
                _unitOfWork.Save(cancellationToken);
                return Result<string>.ReturnResult(200, "Created Successfully", null);
            }
            catch (Exception ex)
            {
                return Result<string>.ReturnResult(500, ex.Message, null);

            }

        }
    }
}
