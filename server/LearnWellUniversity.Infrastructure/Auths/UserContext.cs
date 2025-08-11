using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearnWellUniversity.Infrastructure.Auths
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

        public string? UserId => User?.Claims?.FirstOrDefault(x=> x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        public string? UserName => User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;
        public string? Email => User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public IEnumerable<string> Roles => User?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? [];
        public IEnumerable<string> RoleIds => User?.FindAll(ClaimConstants.RoleId).Select(c => c.Value) ?? [];

        public string? GetClaim(string claimType) => User?.Claims?.FirstOrDefault(x => x.Type == claimType)?.Value;
    }
}
