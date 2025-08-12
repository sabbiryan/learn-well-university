using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes;

namespace LearnWellUniversity.Application.Contracts
{
    public interface IRoleService : IApplicationCrudService<RoleDto, int, RoleCreateRequest, RoleUpdateRequest>;
    

}