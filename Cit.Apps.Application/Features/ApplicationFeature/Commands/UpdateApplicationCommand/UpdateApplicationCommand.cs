using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Common;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.Features.ApplicationFeature.Commands.UpdateApplicationCommand
{
    public record UpdateApplicationCommand : IRequest<ResultModel<string>>
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
    }

    public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateApplicationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultModel<string>> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateApplication = await _unitOfWork.Repository<ApplicationData>().GetByIdAsync(request.Id);
                if (updateApplication is not null)
                {
                    updateApplication.Name = request.ApplicationName;
                    updateApplication.ModifiedAt = DateTime.Now;

                }
                await _unitOfWork.Repository<ApplicationData>().UpdateAsync(updateApplication);
                await _unitOfWork.Save(cancellationToken);
                var result = await Result<string>.ReturnResult(200, "Successfully updated", null);
                return result;
            }
            catch (Exception ex)
            {
                return await Result<string>.ReturnResult(500, ex.Message, null);
            }
        }
    }
}
