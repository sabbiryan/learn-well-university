using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Events
{
    public sealed class TokenIssuedEvent
    {
        public required string UserId { get; init; }
        public required string AccessToken { get; init; }
        public required DateTimeOffset AccessTokenExpiresAtUtc { get; init; }
        public required string RefreshToken { get; init; }
        public required DateTimeOffset RefreshTokenExpiresAtUtc { get; init; }
    }
}
