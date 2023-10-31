using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Shared.Result;
using MediatR;
using System.Numerics;

namespace Cit.Apps.Licensing.Application.Features.Users.Commands.UpdateUserCommand
{
    public record UpdateUserCommand:IRequest<ResultModel<string>>{
        public int Id { get; set; } 
        public string FirstName{ get; set; }
        public string UserName { get; set; }

        public string LastName { get; set; }

    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _mapper = mapper;

        }
        public async Task<ResultModel<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateUser=await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
                if (updateUser is not null)
                {
                    updateUser.FirstName = request.FirstName;
                    updateUser.LastName = request.LastName;
                    updateUser.UserName = request.UserName;
                    updateUser.ModifiedAt=DateTime.Now;

                }
                await _unitOfWork.Repository<User>().UpdateAsync(_mapper.Map<User>(updateUser));
                await _unitOfWork.Save(cancellationToken);
                var result=await Result<string>.ReturnResult(200,"Successfully updated", null);
                return result;
            }
            catch (Exception ex)
            {
                return await Result<string>.ReturnResult(500, ex.Message, null);
            }


        }
    }
}
