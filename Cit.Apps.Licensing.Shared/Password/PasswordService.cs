using Cit.Apps.Shared.Result;
using System.Text;

namespace Cit.Apps.Licensing.Shared.Password
{
    public class PasswordService :IPasswordService
    {
        ////private readonly IOptions<IdentityOptions> _identityOptions;
        //private readonly ILogger<PasswordService> _logger;

        //public PasswordService(ILogger<PasswordService> logger)
        //{
        //    // _identityOptions = identityOptions;
        //    _logger = logger;
        //}

        /// <summary>
        /// Generates a password according to Identity password options configured
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomPassword()
        {
            //var passwordOpt = _identityOptions.Value.Password;
            bool requiredDigit = true;
            int requiredUniqueChars = 4;
            int requiredLength = 8;
            bool requireLowercase = true;
            bool requireNonAlpha = true;
            bool requireUppercase = true;
            return GeneratePassword(requiredLength, requiredUniqueChars, requiredDigit, requireLowercase, requireNonAlpha, requireUppercase);
        }

        public Task<ResultModel<string?>> CreatePasswordHash(string password, out string passwordHash,
            out string passwordSalt)
        {
            
            passwordHash = ""; passwordSalt = "";
            try
            {

                if (string.IsNullOrEmpty(password)) return Result<string?>.ReturnResult(404, "Password required.", null);
                if (string.IsNullOrWhiteSpace(password)) return Result<string?>.ReturnResult(404, "Password required.", null); ;

                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = Convert.ToBase64String(hmac.Key);
                    passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                }
                return Result<string?>.ReturnResult(200, "Password Created Successfully", null); 
            }
            catch (Exception ex)
            {
                //ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to verify the password.");
            }
            return Result<string?>.ReturnResult(500, "Try again later", null);
        }

        public Task<ResultModel<string?>> VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            try
            {
                if (string.IsNullOrEmpty(password)) return Result<string?>.ReturnResult(404, "Password required.", null);
                if (string.IsNullOrWhiteSpace(password)) return Result<string?>.ReturnResult(404, "Password cannot be empty or whitespace only string..", null);
                if (storedHash.Length != 64) return Result<string?>.ReturnResult(404, "Invalid Password hash found", null);
                if (storedSalt.Length != 128) return Result<string?>.ReturnResult(404, "Invalid Password salt found", null);

                using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    if (computedHash.SequenceEqual(storedHash))
                        return Result<string?>.ReturnResult(200, "Password is Right", null);
                }
            }
            catch (Exception ex)
            {
                //ServicesHelper.HandleServiceError(ref serviceResult, _logger, ex, "Error while trying to verify the password.");
            }
            return Result<string?>.ReturnResult(200, "Password is Right", null);
        }

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        private string GeneratePassword(int requiredLength = 6, int requiredUniqueChars = 4,
            bool requireDigit = true, bool requireLowercase = true,
            bool requireNonAlphanumeric = true, bool requireUppercase = true)
        {
            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            @"-._@+\"                       // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (requireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (requireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (requireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (requireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < requiredLength
                || chars.Distinct().Count() < requiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
