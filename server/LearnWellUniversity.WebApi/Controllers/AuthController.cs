using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes.Auths;
using LearnWellUniversity.Application.Services;
using LearnWellUniversity.WebApi.Controllers.Bases;
using LearnWellUniversity.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class AuthController(IAuthService authService) : ApiControllerV1
    {
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ApiResponse<SignupResponse>> RegisterAsync([FromBody] SignupRequest request)
        {
            try
            {
                var response = await authService.RegisterAsync(request);
                
                return new ApiResponse<SignupResponse>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<SignupResponse>(new ApiError(HttpStatusCode.BadRequest.ToString() , ex.Message), StatusCodes.Status400BadRequest);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ApiResponse<TokenResponse>> LoginAsync([FromBody] TokenRequest request)
        {
            try
            {

                var response = await authService.LoginAsync(request, HttpContext.GetClientIp());
                
                return new ApiResponse<TokenResponse>(response);

            }
            catch (Exception ex)
            {
                return new ApiResponse<TokenResponse>(new ApiError(HttpStatusCode.BadRequest.ToString(), ex.Message), StatusCodes.Status400BadRequest);
            }
        }


        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<ApiResponse<TokenResponse>> RefreshAsync([FromBody] TokenRefreshRequest request)
        {
            try
            {
                TokenResponse token = await authService.RefreshTokenAsync(request, HttpContext.GetClientIp());

                return new ApiResponse<TokenResponse>(token);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TokenResponse>(new ApiError(HttpStatusCode.BadRequest.ToString(), ex.Message), StatusCodes.Status400BadRequest);
            }
        }


        [HttpPost("Revoke")]
        public async Task<ApiResponse<bool>> Revoke([FromBody] TokenRefreshRequest request)
        {
            try
            {
                bool result = await authService.RevokeRefreshTokenAsync(request, HttpContext.GetClientIp());

                return new ApiResponse<bool>(result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(new ApiError(HttpStatusCode.BadRequest.ToString(), ex.Message), StatusCodes.Status400BadRequest);
            }
        }
    }

}

