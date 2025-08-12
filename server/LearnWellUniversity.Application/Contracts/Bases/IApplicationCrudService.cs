using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Dtos.Bases;
using LearnWellUniversity.Application.Requestes.Bases;
using LearnWellUniversity.Domain.Entities.Bases;

namespace LearnWellUniversity.Application.Contracts.Bases
{
    public interface IApplicationCrudService< TDto, TPk, TCRequest, TURequest> : IApplicationService
        where TDto : DtoBase<TPk>
        where TCRequest : class
        where TURequest : RequestBase<TPk>
    {
        Task<PaginatedResult<TDto>> GetPagedAsync(DynamicQueryRequest request);
        Task<TDto?> GetByIdAsync(TPk id);
        Task<TPk> AddAsync(TCRequest request);
        Task UpdateAsync(TURequest request);
        Task DeleteAsync(TPk id);
    }

}
