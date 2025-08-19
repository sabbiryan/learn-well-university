using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes.Auths;

namespace LearnWellUniversity.Application.Contracts.Auths
{
    public interface IAuthService: IApplicationService
    {
        Task<SignupResponse> RegisterAsync(SignupRequest request);
        Task<TokenResponse> LoginAsync(TokenRequest request, string ipAddress);

        Task<TokenResponse> RefreshTokenAsync(TokenRefreshRequest request, string ipAddress);
        Task<bool> RevokeRefreshTokenAsync(TokenRefreshRequest request, string ipAddress);


        Task AssingUserToRoles(int userId, int[] RoleIds);
        Task RemoveUserFromRoles(int id);
        Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
        Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
