using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class StudentController(IApplicationCrudService<StudentDto, int, StudentCreateRequest, StudentUpdateRequest> service) : 
        CrudController<StudentDto, int, StudentCreateRequest, StudentUpdateRequest>(service)
    {
    }
}
