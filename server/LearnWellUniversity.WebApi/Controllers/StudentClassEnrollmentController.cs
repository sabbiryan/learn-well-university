using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class StudentClassEnrollmentController(IStudentClassEntrollmentService studentClassEntrollmentService,
        ICourseClassEnrollmentService courseClassEnrollmentService) : ApiControllerV1
    {

        [HttpPost("Enroll")]
        public async Task<ApiResponse<StudentClassDto>> EnrollAsync([FromBody] StudentClassRequest request)
        {
            if (request.StudentId <= 0 || request.ClassId <= 0)
            {
                return new ApiResponse<StudentClassDto>("Invalid student or class ID", StatusCodes.Status400BadRequest);
            }

            var response = await studentClassEntrollmentService.EnrollAsync(request);

            if (response == null)
            {
                return new ApiResponse<StudentClassDto>("Enrollment failed", StatusCodes.Status500InternalServerError);
            }

            return new ApiResponse<StudentClassDto>(response);
        }


        [HttpPost("Unenroll")]
        public async Task<ApiResponse<StudentClassDto>> UnenrollAsync([FromBody] StudentClassRequest request)
        {
            if (request.StudentId <= 0 || request.ClassId <= 0)
            {
                return new ApiResponse<StudentClassDto>("Invalid student or class ID", StatusCodes.Status400BadRequest);
            }

            var response = await studentClassEntrollmentService.UnenrollAsync(request);

            if (response == null)
            {
                return new ApiResponse<StudentClassDto>("Unenrollment failed", StatusCodes.Status500InternalServerError);
            }

            return new ApiResponse<StudentClassDto>(response);
        }


        [HttpGet("GetEnrolledClasses/{studentId}")]
        public async Task<ApiResponse<List<StudentClassDto>>> GetEnrolledClassesAsync(int studentId)
        {
            if (studentId <= 0)
            {
                return new ApiResponse<List<StudentClassDto>>("Invalid student ID", StatusCodes.Status400BadRequest);
            }

            var enrolledClasses = await studentClassEntrollmentService.GetEnrolledClassesAsync(studentId);

            if (enrolledClasses == null || enrolledClasses.Count == 0)
            {
                return new ApiResponse<List<StudentClassDto>>([], StatusCodes.Status404NotFound);
            }

            return new ApiResponse<List<StudentClassDto>>(enrolledClasses);
        }


        [HttpGet("GetEnrolledStudents/{classId}")]
        public async Task<ApiResponse<List<StudentClassDto>>> GetEnrolledStudentsAsync(int classId)
        {
            if (classId <= 0)
            {
                return new ApiResponse<List<StudentClassDto>>("Invalid class ID", StatusCodes.Status400BadRequest);
            }


            var enrolledStudents = await studentClassEntrollmentService.GetEnrolledStudentsAsync(classId);
            if (enrolledStudents == null || enrolledStudents.Count == 0)
            {
                return new ApiResponse<List<StudentClassDto>>([], StatusCodes.Status404NotFound);
            }

            return new ApiResponse<List<StudentClassDto>>(enrolledStudents);
        }


        [HttpGet("GetEnrolledCourses/{classId}")]
        public async Task<ApiResponse<List<CourseClassDto>>> GetEnrolledCoursesAsync(int classId)
        {
            if (classId <= 0)
            {
                return new ApiResponse<List<CourseClassDto>>("Invalid class ID", StatusCodes.Status400BadRequest);
            }

            var enrolledCourses = await courseClassEnrollmentService.GetEnrolledCoursesAsync(classId);
            
            if (enrolledCourses == null || enrolledCourses.Count == 0)
            {
                return new ApiResponse<List<CourseClassDto>>([], StatusCodes.Status404NotFound);
            }

            return new ApiResponse<List<CourseClassDto>>(enrolledCourses);
        }
    }
}
