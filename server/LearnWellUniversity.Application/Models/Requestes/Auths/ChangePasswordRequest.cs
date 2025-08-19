namespace LearnWellUniversity.Application.Models.Requestes.Auths
{
    public record ChangePasswordRequest(int UserId, string CurrentPassword, string NewPassword);
}
    