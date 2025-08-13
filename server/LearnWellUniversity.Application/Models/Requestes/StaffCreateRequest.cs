using LearnWellUniversity.Application.Models.Requestes.Bases;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record StaffCreateRequest(
        string FirstName, 
        string LastName, 
        string Email, 
        string? Phone, 
        string Code, 
        string? Position, 
        DateTime? DateOfBirth, 
        int DepartmentId
    ): CreateRequestBase
    {        
        public string Password { get; set; } = default!;
        public int[] RoleIds { get; set; } = default!;

        public int? UserId { get; set; }
    }
}
