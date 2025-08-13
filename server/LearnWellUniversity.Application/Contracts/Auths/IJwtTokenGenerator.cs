using LearnWellUniversity.Domain.Entities.Auths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Contracts.Auths
{
    public interface IJwtTokenGenerator
    {
        (string AccessToken, DateTime AccessTokenExpiresAt) GenerateAccessToken(User user, int? staffId, int? studentId, IEnumerable<Role> roles);
        (string RefreshToken, DateTime RefreshTokenExpiresAt) GenerateRefreshToken();
    }
}
