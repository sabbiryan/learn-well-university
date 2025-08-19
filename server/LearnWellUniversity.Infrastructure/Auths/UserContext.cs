using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace LearnWellUniversity.Infrastructure.Auths
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

        public string? UserId => User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        public string? UserName => User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;
        public string? Email => User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public IEnumerable<string> Roles => User?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? [];
        public IEnumerable<string> RoleIds => User?.FindAll(ClaimConstants.RoleId).Select(c => c.Value) ?? [];

        public IEnumerable<string> Permissions => User?.FindAll(ClaimConstants.Permission).Select(c => c.Value) ?? [];


        public string? StaffId => User?.Claims?.FirstOrDefault(x => x.Type == ClaimConstants.StaffId)?.Value;
        public string? StudentId => User?.Claims?.FirstOrDefault(x => x.Type == ClaimConstants.StudentId)?.Value;

        public string? GetClaim(string claimType) => User?.Claims?.FirstOrDefault(x => x.Type == claimType)?.Value;


        public T? GetTypedClaim<T>(string claimType)
        {
            var claimValue = User?.Claims?.FirstOrDefault(x => x.Type == claimType)?.Value;
            if (claimValue == null)
            {
                return default;
            }

            try
            {
                var value = JsonSerializer.Deserialize<T>(claimValue);

                return value;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize claim '{claimType}' to type '{typeof(T).Name}'.", ex);
            }
        }

        public T? GetTypedFromValue<T>(string? claimValue)
        {
            if(claimValue == null)
            {
                return default;
            }

            try
            {
                var value = JsonSerializer.Deserialize<T>(claimValue);
                return value;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize value '{claimValue}' to type '{typeof(T).Name}'.", ex);
            }
        }
    }
}
