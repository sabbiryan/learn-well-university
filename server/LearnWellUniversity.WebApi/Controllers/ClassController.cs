using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class ClassController(IClassService service) : CrudController<ClassDto, int, ClassCreateRequest, ClassUpdateRequest>(service)
    {

    }


}
