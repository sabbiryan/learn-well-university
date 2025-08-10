using LearnWellUniversity.Application.Contracts.Jwt;
using LearnWellUniversity.Domain.Entities.Securities;
using LearnWellUniversity.Infrastructure.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Jwt
{
    public class JwtTokenGenerator() : IJwtTokenGenerator
    {
        public string GenerateToken(User user, IEnumerable<Role> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName ?? string.Empty),                
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
                claims.Add(new Claim(ClaimConstants.RoleId, role.Id.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingValues.JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: AppSettingValues.JwtIssuer,
                audience: AppSettingValues.JwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
