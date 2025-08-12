using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Common.Paginations;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;

namespace LearnWellUniversity.Application.Contracts
{
    public interface IUserService: IApplicationService
    {
        Task<PaginatedResult<UserDto>> GetPagedAsync(DynamicQueryRequest request);
        Task<UserDto?> GetByIdAsync(int id);
        Task<int> UpdateAsync(UserUpdateRequest request);
        Task DeleteAsync(int id);
    }
}