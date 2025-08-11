using LearnWellUniversity.Application.Common;
using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{
    
    public class UserController(IUserService userService) : ApiControllerBaseV1
    {
        [HttpGet]
        public async Task<ApiResponse<PaginatedResult<UserDto>>> GetUsersAsync([FromQuery] DynamicQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var result = await userService.GetAllAsync(query);

            return new ApiResponse<PaginatedResult<UserDto>>(result);
        }
    }
}
