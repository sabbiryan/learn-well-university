using LearnWellUniversity.Application.Contracts.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Caching
{
    public sealed class RedisTokenCache : ITokenCache
    {
        private readonly IDatabase _db;

        public RedisTokenCache(IConnectionMultiplexer mux)
        {
            _db = mux.GetDatabase();
        }

        private static string AccessKey(string userId) => $"token:{userId}:access";
        private static string RefreshKey(string userId) => $"token:{userId}:refresh";


        public async Task StoreAccessTokenAsync(string userId, string accessToken, DateTimeOffset expiresAtUtc, CancellationToken ct)
        {
            var ttl = expiresAtUtc - DateTimeOffset.UtcNow;
            if (ttl < TimeSpan.Zero) ttl = TimeSpan.FromSeconds(1); // avoid negative TTL // store as a simple string with TTL

            await _db.StringSetAsync(AccessKey(userId), accessToken, ttl);
        }

        public async Task StoreRefreshTokenAsync(string userId, string refreshToken, DateTimeOffset expiresAtUtc, CancellationToken ct)
        {
            var ttl = expiresAtUtc - DateTimeOffset.UtcNow;
            if (ttl < TimeSpan.Zero) ttl = TimeSpan.FromSeconds(1);

            await _db.StringSetAsync(RefreshKey(userId), refreshToken, ttl);
        }
    }
}
