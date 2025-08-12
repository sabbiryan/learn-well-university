using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes.Auths;
using LearnWellUniversity.Application.Services;
using LearnWellUniversity.WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearnWellUniversity.WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthController(IAuthService userService) : ApiControllerV1
    {
        [HttpPost("Register")]
        public async Task<ApiResponse<SignupResponse>> Register([FromBody] SignupRequest request)
        {
            try
            {
                var response = await userService.RegisterAsync(request);
                
                return new ApiResponse<SignupResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<SignupResponse>(new ApiError(HttpStatusCode.BadRequest.ToString() , ex.Message), StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("Login")]
        public async Task<ApiResponse<TokenResponse>> Login([FromBody] TokenRequest request)
        {
            try
            {
                var response = await userService.LoginAsync(request);
                
                return new ApiResponse<TokenResponse>(response);

            }
            catch (Exception ex)
            {
                return new ApiResponse<TokenResponse>(new ApiError(HttpStatusCode.BadRequest.ToString(), ex.Message), StatusCodes.Status400BadRequest);
            }
        }
    }
}
