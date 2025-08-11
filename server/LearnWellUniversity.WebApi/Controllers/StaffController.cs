using LearnWellUniversity.Application.Common;
using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class StaffController(IStaffService staffService) : ApiControllerBaseV1
    {

        [HttpGet]
        public async Task<ApiResponse<PaginatedResult<StaffDto>>> GetStaffs([FromQuery] DynamicQuery query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var result = await staffService.GetAllStaffAsync(query);
            
            return new ApiResponse<PaginatedResult<StaffDto>>(result);
        }

    }
}
