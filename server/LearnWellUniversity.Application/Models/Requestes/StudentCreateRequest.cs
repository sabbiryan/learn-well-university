using LearnWellUniversity.Application.Models.Requestes.Bases;
using LearnWellUniversity.Domain.Enums;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record StudentCreateRequest(
        string Code,
        string FirstName, 
        string LastName, 
        string Email,
        string? Phone,         
        AcademicLevel AcademicLevel,         
        DateTime? DateOfBirth, 
        int DepartmentId,
        DateTime? EnrollmentDate) : CreateRequestBase
    {
        public string Password { get; set; } = default!;
        public int[] RoleIds { get; set; } = default!;

        public int? UserId { get; set; }
    }

}
