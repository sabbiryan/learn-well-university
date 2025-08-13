namespace LearnWellUniversity.Application.Models.Dtos
{
    public record StudentCourseDto(
       int StudentId,
       string? StudentCode,
       string? StudentName,
       int CourseId,
       string? CourseCode,
       string? CourseName,       
       decimal? Score,
       int? GradingId,
       string? GradingName,
       DateTime? EnrollmentDate = null,
       int? EnrollmentStaffId = null,
       string? EnrollmentStaffName = null
   );
}
