using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Application.Features.Users.Commands.DeleteUserCommand
{
    public record DeleteUserCommand : IRequest<ResultModel<string>>
    {
        public string Id { get; set; }
        public int UserId { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultModel<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var successResult =await Result<string>.ReturnResult(200, "Deleted Successfully", null);
            var badResult =await  Result<string>.ReturnResult(500, "Deleted Successfully", null);

            try
            {
                var deleteData = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
                if (deleteData is not null)
                {
                   await _unitOfWork.Repository<User>().DeleteAsync(deleteData);
                    await _unitOfWork.Save(cancellationToken);
                    return successResult;
                }

                else
                {
                    return badResult;
                }


            }

            catch (Exception ex)
            {
                return badResult;
            }

             return badResult;

        }
    }
}
