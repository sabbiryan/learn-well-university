namespace LearnWellUniversity.Application.Models.Requestes
{
    public record StaffCreateRequest(string FirstName, string LastName, string Email, string? Phone, string Code, string? Position, DateTime? DateOfBirth, int DepartmentId);
}
