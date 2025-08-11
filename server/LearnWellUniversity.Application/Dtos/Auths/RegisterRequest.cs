namespace LearnWellUniversity.Application.Dtos.Auths
{
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password, string? Phone, int RoleId);
}
