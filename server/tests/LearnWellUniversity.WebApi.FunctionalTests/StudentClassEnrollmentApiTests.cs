using FluentAssertions;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.FunctionalTests.Bases;
using LearnWellUniversity.WebApi.FunctionalTests.Setups;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;

namespace LearnWellUniversity.WebApi.FunctionalTests
{
    public class StudentClassEnrollmentApiTests : FunctionalTestApiBase
    {
        public StudentClassEnrollmentApiTests(FunctionalTestFixture fixture) : base(fixture)
        {
            GetStaffToken().ConfigureAwait(false).GetAwaiter().GetResult();
        }


        [Fact]
        public async Task EnrollAsync_Should_Return_200_When_Success()
        {
            //Arrange
            var request = new
            {
                ClassId = 3,
                StudentId = 2
            };


            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentClassEnrollment/Enroll", HttpMethod.Post, request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<StudentClassDto>>();

            //Asset
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
            result.Data.ClassId.Should().Be(3);
            result.Data.StudentId.Should().Be(2);
        }

        [Fact]
        public async Task EnrollAsync_Should_Return_400_When_Invalid_Request()
        {
            //Arrange
            var request = new StudentClassRequest(0,0);

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentClassEnrollment/Enroll", HttpMethod.Post, request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<StudentClassDto>>();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            result.Data.Should().BeNull();
            result.Error.Should().NotBeNull();
            result.Error.Message.Should().Be("Invalid student or class ID");

        }

        [Fact]
        public async Task UnenrollAsync_Should_Return_200_When_Success()
        {
            //Arrange
            var request = new StudentClassRequest(2, 1);

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentClassEnrollment/Unenroll", HttpMethod.Post, request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<StudentClassDto>>();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
            result.Data.ClassId.Should().Be(1);
            result.Data.StudentId.Should().Be(2);
        }

        [Fact]
        public async Task GetEnrolledClassesAsync_Should_Return_List()
        {
            //Arrange
            int studentId = 1;

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentClassEnrollment/GetEnrolledClasses/{studentId}", HttpMethod.Get);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<StudentClassDto>>>();

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetEnrolledStudentsAsync_Should_Return_List()
        {
            //Arrange
            int classId = 1;

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentClassEnrollment/GetEnrolledStudents/{classId}", HttpMethod.Get);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<StudentClassDto>>>();
            
            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetClassEnrolledCoursesAsync_Should_Return_List()
        {
            //Arrange
            int classId = 1;

            //Act            
            var response = await AuthorizedRequestAsync($"{ApiV1}/StudentClassEnrollment/GetClassEnrolledCourses/{classId}", HttpMethod.Get);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<CourseClassDto>>>();

            //Asset
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Data.Should().NotBeNull();
        }


    }


}