using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Licensing.Shared.Password;
using Cit.Apps.Shared.Result;
using System.Data.Entity;

namespace Cit.Apps.Licensing.Persistence.Repositories
{
    public class Service : IService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUnitOfWork _unitOfWork;
        public Service(IPasswordService passwordService, IUnitOfWork unitOfWork)
        {
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultModel<string?>> PasswordVerifier(string username, string password)
        {
            ResultModel<string?> result;
            var userData = _unitOfWork.Repository<User>().Entities.FirstOrDefault(x => x.UserName == username);

            if (userData is not null)
            {
                result = await _passwordService.VerifyPasswordHash(password, Convert.FromBase64String(userData.PasswordHash), Convert.FromBase64String(userData.PasswordSalt));
                if (result.Statuscode == 200)
                {
                    result.Data=userData.Id.ToString();
                    return result;
                }
                else
                {
                    return await Result<string?>.ReturnResult(401, "Incorrect Password", null);
                }
            }

            else
            {
                return await Result<string?>.ReturnResult(404, "User not found", null);
            }

        }
    }
}
