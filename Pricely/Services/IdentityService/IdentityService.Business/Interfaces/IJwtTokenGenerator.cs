using IdentityService.Domain.Entities;
using System.Security.Claims;

namespace IdentityService.Business.Interfaces
{
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates jwt token for user
        /// </summary>
        /// <returns></returns>
        string GenerateToken(Company user, ClaimsIdentity claims, bool rememberMe = false);
    }
}
