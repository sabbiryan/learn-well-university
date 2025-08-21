using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Contracts.Caching
{
    public interface ITokenCache
    {
        Task StoreAccessTokenAsync(string userId, string accessToken, DateTimeOffset expiresAtUtc, CancellationToken ct);
        Task StoreRefreshTokenAsync(string userId, string refreshToken, DateTimeOffset expiresAtUtc, CancellationToken ct);
    }
}
