using FluentAssertions;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes.Auths;
using LearnWellUniversity.Application.Models.Statics;
using LearnWellUniversity.WebApi.FunctionalTests.Bases;
using LearnWellUniversity.WebApi.FunctionalTests.Setups;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace LearnWellUniversity.WebApi.FunctionalTests
{
    public class AuthApiTests(FunctionalTestFixture fixture) : FunctionalTestApiBase(fixture)
    {


        [Fact]
        public async Task RegisterAsync_ShouldReturn401Status()
        {
            // Arrange
            var request = new SignupRequest("Sabbir", "Ahamed", "sabbiryan@gmail.com", "abc@123!", "+8801911831907", [1]);

            // Act
            var response = await HttpClient.PostAsJsonAsync($"{ApiV1}/auth/register", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


        [Fact]
        public async Task LoginAsync_ShouldReturnAccessToken()
        {
            // Arrange
            var request = new
            {
                StaticUser.Admin.Email,
                StaticUser.Admin.Password
            };

            // Act
            var response = await HttpClient.PostAsJsonAsync($"{ApiV1}/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TokenResponse>>();

            // Assert
            response.EnsureSuccessStatusCode();
            result!.Data.Should().NotBeNull();
        }
    }

}