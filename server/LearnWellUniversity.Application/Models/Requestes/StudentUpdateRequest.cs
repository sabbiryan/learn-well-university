using LearnWellUniversity.Application.Models.Requestes.Bases;
using LearnWellUniversity.Domain.Enums;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record StudentUpdateRequest(
        string Code,
        string FirstName, 
        string LastName, 
        string? Phone,         
        AcademicLevel AcademicLevel, 
        DateTime? DateOfBirth, 
        int DepartmentId,
        DateTime? EnrollmentDate
    ) : UpdateRequestBase<int>;

}
