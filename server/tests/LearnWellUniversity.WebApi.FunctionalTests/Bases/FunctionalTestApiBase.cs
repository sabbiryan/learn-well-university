using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Requestes.Auths;
using LearnWellUniversity.Application.Models.Statics;
using LearnWellUniversity.WebApi.FunctionalTests.Setups;
using System.Net.Http.Json;

namespace LearnWellUniversity.WebApi.FunctionalTests.Bases
{
    public abstract class FunctionalTestApiBase(FunctionalTestFixture fixture) : IClassFixture<FunctionalTestFixture>
    {
        protected readonly HttpClient HttpClient = fixture.Factory.CreateClient();

        protected const string ApiV1 = "/api/v1";

        protected TokenResponse Token { get; set; } = default!;


        protected async Task<TokenResponse> GetToken(string email, string password)
        {

            var request = new
            {
                email,
                password
            };

            var response = await HttpClient.PostAsJsonAsync($"{ApiV1}/auth/login", request);

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TokenResponse>>();

            if (result?.Data == null) throw new Exception("Authentication failed. Unable to contiue test.");

            return result.Data;
        }
    }
}
