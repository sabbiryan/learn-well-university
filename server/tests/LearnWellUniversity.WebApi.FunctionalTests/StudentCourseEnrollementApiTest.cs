using FluentAssertions;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.FunctionalTests.Bases;
using LearnWellUniversity.WebApi.FunctionalTests.Setups;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace LearnWellUniversity.WebApi.FunctionalTests
{
    public class StudentCourseEnrollementApiTest : FunctionalTestApiBase
    {
        public StudentCourseEnrollementApiTest(FunctionalTestFixture fixture) : base(fixture)
        {
            GetStaffToken().ConfigureAwait(false).GetAwaiter().GetResult();
        }


        [Fact]
        public async Task EnrollAsync_Should_Return_200_When_Success()
        {
            //Arrange
            var request = new
            {
                CourseId = 3,
                StudentId = 2,
                ClassId = 2
            };


            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentCourseEnrollment/Enroll", HttpMethod.Post, request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<StudentCourseDto>>();

            //Asset
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
            result.Data.CourseId.Should().Be(3);
            result.Data.StudentId.Should().Be(2);
        }

        [Fact]
        public async Task EnrollAsync_Should_Return_400_When_Invalid_Request()
        {
            //Arrange
            var request = new StudentCourseRequest(0, 0);

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentCourseEnrollment/Enroll", HttpMethod.Post, request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<StudentCourseDto>>();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Data.Should().BeNull();
            result.Error.Should().NotBeNull();
            result.Error.Message.Should().Be("Invalid student or course ID");

        }

        [Fact]
        public async Task UnenrollAsync_Should_Return_200_When_Success()
        {
            //Arrange
            var request = new StudentCourseRequest(2, 2) { ClassId = 2 };

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentCourseEnrollment/Unenroll", HttpMethod.Post, request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<StudentCourseDto>>();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
            result.Data.CourseId.Should().Be(2);
            result.Data.StudentId.Should().Be(2);
        }

        [Fact]
        public async Task GetEnrolledCoursesAsync_Should_Return_List()
        {
            //Arrange
            int studentId = 1;

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentCourseEnrollment/GetEnrolledCourses/{studentId}", HttpMethod.Get);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<StudentCourseDto>>>();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetEnrolledStudentsAsync_Should_Return_List()
        {
            //Arrange
            int courseId = 1;

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentCourseEnrollment/GetEnrolledStudents/{courseId}", HttpMethod.Get);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<StudentCourseDto>>>();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCourseEnrolledClassesAsync_Should_Return_List()
        {
            //Arrange
            int courseId = 1;

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentCourseEnrollment/GetCourseEnrolledClasses/{courseId}", HttpMethod.Get);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<CourseClassDto>>>();

            //Asset
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
        }


    }

}