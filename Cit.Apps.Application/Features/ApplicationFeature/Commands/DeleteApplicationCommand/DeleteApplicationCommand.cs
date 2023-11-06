using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.Features.ApplicationFeature.Commands.DeleteApplicationCommand
{
    public record DeleteApplicationCommand:IRequest<ResultModel<string>>
    {
        public int Id { get; set;}
    }

    public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteApplicationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ResultModel<string>> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var successResult = await Result<string>.ReturnResult(200, "Deleted Successfully", null);
            var badResult = await Result<string>.ReturnResult(500, "Deleted Failed", null);

            try
            {
                var deleteData = await _unitOfWork.Repository<ApplicationData>().GetByIdAsync(request.Id);
                if (deleteData is not null)
                {
                    await _unitOfWork.Repository<ApplicationData>().DeleteAsync(deleteData);
                    await _unitOfWork.Save(cancellationToken);
                    return successResult;
                }

                else
                {
                    badResult.Message = "ApplicationData Not Found! Sorry!";
                    return badResult;
                }
            }

            catch (Exception ex)
            {
                badResult.Message = ex.Message;
                return badResult;
            }
        }
    }
    }



