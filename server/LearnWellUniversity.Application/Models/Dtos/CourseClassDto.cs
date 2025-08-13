namespace LearnWellUniversity.Application.Models.Dtos
{
    public record CourseClassDto(
        int CourseId,
        string? CourseCode,
        string? CourseName,
        int ClassId,
        string? ClassCode,
        string? ClassName
    );    
}
