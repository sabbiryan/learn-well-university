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
        public async Task Register_ShouldReturn401Status()
        {
            var request = new SignupRequest("Sabbir", "Ahamed", "sabbiryan@gmail.com", "abc@123!", "+8801911831907", [1]);

            var response = await HttpClient.PostAsJsonAsync($"{ApiV1}/auth/register", request);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


        [Fact]
        public async Task Login_ShouldReturnAccessToken()
        {
            var request = new
            {
                StaticUser.Admin.Email,
                StaticUser.Admin.Password
            };

            var response = await HttpClient.PostAsJsonAsync($"{ApiV1}/auth/login", request);
            
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TokenResponse>>();


            response.EnsureSuccessStatusCode();
            
            result!.Data.Should().NotBeNull();
        }
    }
}
