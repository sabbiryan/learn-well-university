using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Dtos.Auths;

namespace LearnWellUniversity.Application.Contracts.Auths
{
    public interface IAuthService: IApplicationService
    {
        Task<SignupResponse> RegisterAsync(SignupRequest request);
        Task<TokenResponse> LoginAsync(TokenRequest request);

        Task AssingUserToRoles(int userId, int[] RoleIds);
        Task RemoveUserFromRoles(int id);
    }
}
