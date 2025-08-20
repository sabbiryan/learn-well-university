using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class StudentCourseEnrollmentController(
        IStudentCourseEntrollmentService studentCourseEntrollmentService,
        ICourseClassEnrollmentService courseClassEnrollmentService) : ApiControllerV1
    {

        [HttpPost("Enroll")]
        public async Task<ApiResponse<StudentCourseDto>> EnrollAsync([FromBody] StudentCourseRequest request)
        {
            if (request.StudentId <= 0 || request.CourseId <= 0)
            {
                return new ApiResponse<StudentCourseDto>("Invalid student or course ID", StatusCodes.Status400BadRequest);
            }

            var response = await studentCourseEntrollmentService.EnrollAsync(request);

            if (response == null)
            {
                return new ApiResponse<StudentCourseDto>("Enrollment failed", StatusCodes.Status500InternalServerError);
            }

            return new ApiResponse<StudentCourseDto>(response);
        }


        [HttpPost("Unenroll")]
        public async Task<ApiResponse<StudentCourseDto>> UnenrollAsync([FromBody] StudentCourseRequest request)
        {
            if (request.StudentId <= 0 || request.CourseId <= 0)
            {
                return new ApiResponse<StudentCourseDto>("Invalid student or course ID", StatusCodes.Status400BadRequest);
            }

            var response = await studentCourseEntrollmentService.UnenrollAsync(request);

            if (response == null)
            {
                return new ApiResponse<StudentCourseDto>("Unenrollment failed", StatusCodes.Status500InternalServerError);
            }

            return new ApiResponse<StudentCourseDto>(response);
        }


        [HttpGet("GetEnrolledCourses/{studentId}")]
        public async Task<ApiResponse<List<StudentCourseDto>>> GetEnrolledCoursesAsync(int studentId)
        {
            if (studentId <= 0)
            {
                return new ApiResponse<List<StudentCourseDto>>("Invalid student ID", StatusCodes.Status400BadRequest);
            }
            
            var enrolledCourses = await studentCourseEntrollmentService.GetEnrolledCoursesAsync(studentId);

            if (enrolledCourses == null || enrolledCourses.Count == 0)
            {
                return new ApiResponse<List<StudentCourseDto>>([], StatusCodes.Status404NotFound);
            }

            return new ApiResponse<List<StudentCourseDto>>(enrolledCourses);
        }


        [HttpGet("GetEnrolledStudents/{courseId}")]
        public async Task<ApiResponse<List<StudentCourseDto>>> GetEnrolledStudentsAsync(int courseId)
        {
            if (courseId <= 0)
            {
                return new ApiResponse<List<StudentCourseDto>>("Invalid course ID", StatusCodes.Status400BadRequest);
            }

            var enrolledStudents = await studentCourseEntrollmentService.GetEnrolledStudentsAsync(courseId);

            if (enrolledStudents == null || enrolledStudents.Count == 0)
            {
                return new ApiResponse<List<StudentCourseDto>>([], StatusCodes.Status404NotFound);
            }

            return new ApiResponse<List<StudentCourseDto>>(enrolledStudents);
        }


        [HttpGet("GetCourseEnrolledClasses/{courseId}")]
        public async Task<ApiResponse<List<CourseClassDto>>> GetCourseEnrolledClassesAsync(int courseId)
        {
            if (courseId <= 0)
            {
                return new ApiResponse<List<CourseClassDto>>("Invalid course ID", StatusCodes.Status400BadRequest);
            }

            var enrolledClasses = await courseClassEnrollmentService.GetCourseEnrolledClassesAsync(courseId);
            
            if (enrolledClasses == null || enrolledClasses.Count == 0)
            {
                return new ApiResponse<List<CourseClassDto>>([], StatusCodes.Status404NotFound);
            }

            return new ApiResponse<List<CourseClassDto>>(enrolledClasses);
        }
    }

}
