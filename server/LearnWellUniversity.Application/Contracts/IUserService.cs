using LearnWellUniversity.Application.Common;
using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Application.Requestes;
using LearnWellUniversity.Domain.Entities.Auths;

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