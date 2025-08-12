using LearnWellUniversity.Application.Models.Requestes.Bases;
using LearnWellUniversity.Domain.Enums;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record StudentCreateRequest(
        string FirstName, 
        string LastName, 
        string? Phone, 
        string Code, 
        AcademicLevel AcademicLevel, 
        DateTime? EnrollmentDate, 
        DateTime? DateOfBirth, 
        int DepartmentId): CreateRequestBase;

}
