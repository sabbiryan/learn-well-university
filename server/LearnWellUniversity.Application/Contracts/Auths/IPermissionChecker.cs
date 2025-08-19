namespace LearnWellUniversity.Application.Contracts.Auths
{
    public interface IPermissionChecker
    {
        Task<bool> HasPermissionAsync(string permissionCode);
    }
}
