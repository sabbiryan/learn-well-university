namespace LearnWellUniversity.Application.Models.Requestes.Auths
{
    public record ResetPasswordRequest(string Email, string Token, string NewPassword);
}
    