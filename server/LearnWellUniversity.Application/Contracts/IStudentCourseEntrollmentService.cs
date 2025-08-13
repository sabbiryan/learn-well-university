using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;

namespace LearnWellUniversity.Application.Contracts
{
    public interface IStudentCourseEntrollmentService : IApplicationService
    {
        Task<StudentCourseDto> EnrollAsync(StudentCourseRequest request);
        Task<StudentCourseDto> UnenrollAsync(StudentCourseRequest request);
        Task<List<StudentCourseDto>> GetEnrolledCoursesAsync(int studentId);
        Task<List<StudentCourseDto>> GetEnrolledStudentsAsync(int courseId);
    }
}
