namespace LearnWellUniversity.Application.Models.Dtos
{
    public record StudentCourseDto(
       int StudentId,
       string? StudentCode,
       string? StudentName,
       int CourseId,
       string? CourseCode,
       string? CourseName,
       DateTime? EnrollmentDate,
       decimal? Score,
       int? GradingId,
       string? GradingName
   );
}
