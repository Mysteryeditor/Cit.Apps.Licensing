using Cit.Apps.Shared.Result;

namespace Cit.Apps.Licensing.Application.Interfaces.Repositories
{
    public interface IService
    {
        Task<ResultModel<string>> PasswordVerifier(string username,string password);
    }
}
