using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Common.Paginations;
using LearnWellUniversity.Application.Models.Dtos.Bases;
using LearnWellUniversity.Application.Models.Requestes.Bases;
using LearnWellUniversity.Domain.Entities.Bases;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers.Bases
{
    
    public abstract class CrudController<TDto, TPk, TCRequest, TURequest>(IApplicationCrudService<TDto, TPk, TCRequest, TURequest> service) : ApiControllerV1
        where TDto : DtoBase<TPk>
        where TCRequest : class
        where TURequest : RequestBase<TPk>
    {
        [HttpGet]
        public async Task<ApiResponse<PaginatedResult<TDto>>> GetPagedAsync([FromQuery] DynamicQueryRequest request)
        {
            var result = await service.GetPagedAsync(request);

            return new ApiResponse<PaginatedResult<TDto>>(result);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<TDto>> GetByIdAsync([FromRoute] TPk id)
        {
            var result = await service.GetByIdAsync(id);

            if (result == null) return new ApiResponse<TDto>(new ApiError($"No content found with id={id}"), StatusCodes.Status204NoContent);

            return new ApiResponse<TDto>(result);
        }

        [HttpPost]
        public async Task<ApiResponse<TPk>> AddAsyn([FromBody] TCRequest request)
        {
            var id = await service.AddAsync(request);

            return new ApiResponse<TPk>(id);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<TPk>> UpdateAsync([FromRoute] TPk id, [FromBody] TURequest request)
        {
            if (!id!.Equals(request.Id))
                return new ApiResponse<TPk>("Id in the route and body do not match", StatusCodes.Status400BadRequest);

            await service.UpdateAsync(request);

            return new ApiResponse<TPk>(id);
        }


        [HttpDelete("{id}")]
        public async Task<ApiResponse<TPk>> Delete([FromRoute] TPk id)
        {
            await service.DeleteAsync(id);

            return new ApiResponse<TPk>(id);
        }
    }
}
