using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Domain.Entities;

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
}