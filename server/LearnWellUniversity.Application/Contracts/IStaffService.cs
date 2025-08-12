using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Application.Requestes;
using LearnWellUniversity.Domain.Entities;

namespace LearnWellUniversity.Application.Contracts
{
    public interface IStaffService : IApplicationCrudService<StaffDto, int, StaffCreateRequest, StaffUpdateRequest>
    {
        
    }
}