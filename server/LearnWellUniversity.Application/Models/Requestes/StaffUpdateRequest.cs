using LearnWellUniversity.Application.Models.Requestes.Bases;

namespace LearnWellUniversity.Application.Models.Requestes
{

    public record StaffUpdateRequest(
        string FirstName, 
        string LastName, 
        string Email, 
        string? Phone, 
        string Code, 
        string? Position, 
        DateTime? DateOfBirth, 
        int DepartmentId
    ): UpdateRequestBase<int>;
}
