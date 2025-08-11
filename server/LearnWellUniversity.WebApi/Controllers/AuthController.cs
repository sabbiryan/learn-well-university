using LearnWellUniversity.Application.Common;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Dtos.Auths;
using LearnWellUniversity.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearnWellUniversity.WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthController(IAuthService userService) : ApiControllerBaseV1
    {
        [HttpPost("Register")]
        public async Task<ApiResponse<AuthResponse>> Register(RegisterRequest request)
        {
            try
            {
                var response = await userService.RegisterAsync(request);
                
                return new ApiResponse<AuthResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthResponse>(new ApiError(HttpStatusCode.BadRequest.ToString() , ex.Message), StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("Login")]
        public async Task<ApiResponse<AuthResponse>> Login(LoginRequest request)
        {
            try
            {
                var response = await userService.LoginAsync(request);
                
                return new ApiResponse<AuthResponse>(response);

            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthResponse>(new ApiError(HttpStatusCode.BadRequest.ToString(), ex.Message), StatusCodes.Status400BadRequest);
            }
        }
    }
}
