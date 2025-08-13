namespace LearnWellUniversity.Application.Models.Dtos
{
    public record StudentClassDto(
        int StudentId, 
        string? StudentName, 
        int ClassId, 
        string? ClassName,
        DateTime? EnrollmentDate,
        int? EnrollmentStaffId,
        string? EnrollmentStaffName 
    );
}
