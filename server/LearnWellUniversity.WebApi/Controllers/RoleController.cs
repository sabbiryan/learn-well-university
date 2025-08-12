using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class RoleController(IRoleService service) : CrudController<RoleDto, int, RoleCreateRequest, RoleUpdateRequest>(service)
    {
    }

}
