using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace LearnWellUniversity.Application.Services
{


    public class StudentCourseEnrollmentService(IUnitOfWork unitOfWork,
        IMapper mapper,
        ICourseClassEnrollmentService courseClassEnrollmentService,
        IUserContext userContext,
        ILogger<StudentCourseEnrollmentService> logger) : IStudentCourseEntrollmentService
    {
        public async Task<StudentCourseDto> EnrollAsync(StudentCourseRequest request)
        {
            var studentCourse = await unitOfWork.Repository<StudentCourse>()
                .FindAsync(sc => sc.StudentId == request.StudentId && sc.CourseId == request.CourseId);

            if ( studentCourse != null)
            {
                throw new InvalidOperationException("Already enrolled student in this course.");
            }

            try
            {
                studentCourse = mapper.Map<StudentCourse>(request);
                studentCourse.EnrollmentStaffId = userContext.GetTypedFromValue<int?>(userContext.StaffId);

                await unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    await unitOfWork.Repository<StudentCourse>().AddAsync(studentCourse);

                    await unitOfWork.SaveChangesAsync();

                    await courseClassEnrollmentService.EnrollAsync(new CourseClassRequest(request.CourseId, request.ClassId));
                });

                return mapper.Map<StudentCourseDto>(studentCourse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while enrolling student in the course");

                throw new InvalidOperationException("Failed to enroll student in the course.", ex);
            }
        }

        public async Task<StudentCourseDto> UnenrollAsync(StudentCourseRequest request)
        {
            var studentCourse = await unitOfWork.Repository<StudentCourse>()
                .FindAsync(sc => sc.StudentId == request.StudentId && sc.CourseId == request.CourseId);

            if (studentCourse == null)
            {
                throw new InvalidOperationException("Student enrollment not found.");
            }

            try
            {

                await unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    unitOfWork.Repository<StudentCourse>().Remove(studentCourse);

                    await unitOfWork.SaveChangesAsync();

                    await courseClassEnrollmentService.UnenrollAsync(new CourseClassRequest(request.CourseId, request.ClassId));
                });

                return mapper.Map<StudentCourseDto>(studentCourse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while unenrolling student from the course");

                throw new InvalidOperationException("Failed to unenroll student from the course.", ex);
            }
        }

        public async Task<List<StudentCourseDto>> GetEnrolledCoursesAsync(int studentId)
        {
            var studentCourses = await unitOfWork.Repository<StudentCourse>()
                .FilterAsync(sc => sc.StudentId == studentId, x => x.Student, x => x.Course);

            var result = mapper.Map<List<StudentCourseDto>>(studentCourses);

            return result;
        }

        public async Task<List<StudentCourseDto>> GetEnrolledStudentsAsync(int courseId)
        {
            var studentCourses = await unitOfWork.Repository<StudentCourse>()
                .FilterAsync(sc => sc.CourseId == courseId, x => x.Student, x => x.Course);
            
            var result = mapper.Map<List<StudentCourseDto>>(studentCourses);

            return result;
        }

      
    }
}
