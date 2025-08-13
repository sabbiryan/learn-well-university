namespace LearnWellUniversity.Domain.Entities.Auths
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByIp { get; set; } = string.Empty;
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
