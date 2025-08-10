using LearnWellUniversity.Application.Common;
using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearnWellUniversity.WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthControllerController(IUserService userService) : ApiControllerBaseV1
    {
        [HttpPost("register")]
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

        [HttpPost("login")]
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
