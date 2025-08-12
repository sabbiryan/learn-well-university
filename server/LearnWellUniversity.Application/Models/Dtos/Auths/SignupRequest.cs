namespace LearnWellUniversity.Application.Models.Dtos.Auths
{
    public record SignupRequest(string FirstName, string LastName, string Email, string Password, string? Phone, int[] RoleIds);
}
