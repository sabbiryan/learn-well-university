using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Common.Paginations;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Application.Services;
using LearnWellUniversity.WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{

    public class UserController(IUserService userService) : ApiControllerV1
    {
        [HttpGet]
        public async Task<ApiResponse<PaginatedResult<UserDto>>> GetAllAsync([FromQuery] DynamicQueryRequest query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var result = await userService.GetPagedAsync(query);

            return new ApiResponse<PaginatedResult<UserDto>>(result);
        }


        [HttpGet("{id}")]
        public async Task<ApiResponse<UserDto>> GetByIdAsync([FromRoute] int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

            var result = await userService.GetByIdAsync(id);

            if (result == null)
            {
                return new ApiResponse<UserDto>("User not found", StatusCodes.Status404NotFound);
            }

            return new ApiResponse<UserDto>(result);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<int>> UpdateAsync([FromRoute] int id, [FromBody] UserUpdateRequest request)
        {
            if (id != request.Id) throw new ArgumentException("Id in the route does not match Id in the request body.");

            ArgumentNullException.ThrowIfNull(request);

            if (request.Id <= 0) throw new ArgumentOutOfRangeException(nameof(request), "Id must be greater than zero.");

            var result = await userService.UpdateAsync(request);

            return new ApiResponse<int>(result);
        }


        [HttpDelete("{id}")]
        public async Task<ApiResponse<string>> DeleteAsync([FromRoute] int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

            await userService.DeleteAsync(id);
            return new ApiResponse<string>("User deleted successfully");
        }
    }
}
