using Cit.Apps.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cit.Apps.Licensing.Shared.Password
{
    public interface IPasswordService
    {
        string GenerateRandomPassword();

        Task<ResultModel<string?>> CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);

        Task<ResultModel<string?>> VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
