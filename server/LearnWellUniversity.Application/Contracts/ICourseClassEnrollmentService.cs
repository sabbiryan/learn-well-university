using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;

namespace LearnWellUniversity.Application.Contracts
{
    public interface ICourseClassEnrollmentService : IApplicationService
    {
        Task<CourseClassDto> EnrollAsync(CourseClassRequest request);
        Task<CourseClassDto> UnenrollAsync(CourseClassRequest request);
        Task<List<CourseClassDto>> GetEnrolledClassesAsync(int courseId);
        Task<List<CourseClassDto>> GetEnrolledCoursesAsync(int classId);
    }
}
