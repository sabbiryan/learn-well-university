using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;

namespace LearnWellUniversity.Application.Contracts
{
    public interface IStaffService : IApplicationServiceBase
    {
        Task<PaginatedResult<StaffDto>> GetAllStaffAsync(DynamicQuery request);
        Task<StaffDto?> GetStaffByIdAsync(int id);
        Task<Staff> AddStaffAsync(Staff staff);
        Task UpdateStaffAsync(Staff staff);
        Task DeleteStaffAsync(int id);
    }


    public interface IUserService: IApplicationServiceBase
    {
        Task<PaginatedResult<UserDto>> GetAllAsync(DynamicQuery request);
        Task<UserDto?> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}