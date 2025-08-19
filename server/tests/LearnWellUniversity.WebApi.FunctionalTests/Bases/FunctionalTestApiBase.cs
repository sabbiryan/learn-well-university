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


        private async Task<TokenResponse> GetToken(string email, string password)
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


        protected async Task<TokenResponse> GetAdminToken()
        {
            Token = await GetToken(StaticUser.Admin.Email, StaticUser.Admin.Password);
         
            return Token;
        }

        protected async Task<TokenResponse> GetStaffToken()
        {
            Token = await GetToken(StaticUser.Staff.Email, StaticUser.Staff.Password);
         
            return Token;
        }

        protected async Task<TokenResponse> GetStudentToken()
        {
            Token = await GetToken(StaticUser.Student.Email, StaticUser.Student.Password);
         
            return Token;
        }


        protected async Task<HttpResponseMessage> AuthorizedRequestAsync(string url, HttpMethod method, object? content = null)
        {            
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);

            var request = new HttpRequestMessage(method, url);
            
            if (content != null)
            {
                request.Content = JsonContent.Create(content);
            }

            var response = await HttpClient.SendAsync(request);
            
            return response;
        }

    }
}
