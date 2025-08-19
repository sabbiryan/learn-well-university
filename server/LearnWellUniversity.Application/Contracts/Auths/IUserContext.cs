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

        IEnumerable<string> Permissions { get; }

        string? StaffId { get; }
        string? StudentId { get; }

        string? GetClaim(string claimType);

        T? GetTypedClaim<T>(string claimType);

        T? GetTypedFromValue<T>(string? claimValue);
    }
}
