namespace LearnWellUniversity.Application.Contracts.Auths
{
    public interface IUserContext
    {
        string? UserId { get; }
        string? UserName { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        IEnumerable<string> Roles { get; }
        IEnumerable<string> RoleIds { get; }

        string? GetClaim(string claimType);
    }
}
