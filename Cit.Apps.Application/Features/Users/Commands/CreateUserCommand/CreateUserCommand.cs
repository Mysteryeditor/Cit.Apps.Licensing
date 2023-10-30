using AutoMapper;
using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Licensing.Shared.Password;
using Cit.Apps.Shared.Result;
using MediatR;
using System.Reflection;

namespace Cit.Apps.Licensing.Application.Features.Users.Commands.CreateUserCommand
{
#nullable disable
    public record CreateUserCommand : IRequest<ResultModel<string>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultModel<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPasswordService passwordService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }

        public async Task<ResultModel<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var PasswordResult = await _passwordService.CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);
            if (PasswordResult is not null && PasswordResult.Statuscode != 200)
            {
                return await Result<string>.ReturnResult(500, "Try again later", null);
            }

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTime.Now
            };
            try
            {
                await _unitOfWork.Repository<User>().AddAsync(user);
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception ex)
            {
                return await Result<string>.ReturnResult(500, ex.Message, null);
            }


            return await Result<string>.ReturnResult(200, "Created SuccessFully", null);
        }
    }
}
