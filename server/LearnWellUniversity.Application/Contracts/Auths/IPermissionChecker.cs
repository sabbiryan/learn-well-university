namespace LearnWellUniversity.Application.Contracts.Auths
{
    public interface IPermissionChecker
    {
        bool HasPermission(params string[] permissionCodes);
        Task<bool> HasPermissionAsync(string permissionCode);
    }
}
