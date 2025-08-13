namespace LearnWellUniversity.Application.Models.Requestes
{
    public record StudentCourseRequest(
        int StudentId,
        int CourseId
    )
    {
        public int ClassId { get; set; }
    }
}
