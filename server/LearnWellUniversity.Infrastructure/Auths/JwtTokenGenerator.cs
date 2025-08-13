using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Domain.Entities.Auths;
using LearnWellUniversity.Infrastructure.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LearnWellUniversity.Infrastructure.Auths
{
    public class JwtTokenGenerator() : IJwtTokenGenerator
    {
        public (string AccessToken, DateTime AccessTokenExpiresAt) GenerateAccessToken(User user, int? staffId, int? studentId, IEnumerable<Role> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Email),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Name, user.FullName ?? string.Empty),                
                
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
                claims.Add(new Claim(ClaimConstants.RoleId, role.Id.ToString()));
            }

            if(staffId.HasValue)
            {
                claims.Add(new Claim(ClaimConstants.StaffId, staffId.Value.ToString()));
            }   
            else if(studentId.HasValue)
            {
                claims.Add(new Claim(ClaimConstants.StudentId, studentId.Value.ToString()));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingValues.JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: AppSettingValues.JwtIssuer,
                audience: AppSettingValues.JwtAudience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenString, expiresAt);
        }


        public (string RefreshToken, DateTime RefreshTokenExpiresAt) GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);

            var refreshToken = Convert.ToBase64String(bytes);

            var expiresAt = DateTime.UtcNow.AddDays(7);

            return (refreshToken, expiresAt);
        }
    }
}
