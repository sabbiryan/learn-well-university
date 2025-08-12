using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class CourseController(ICourseService service) : CrudController<CourseDto, int, CourseCreateRequest, CourseUpdateRequest>(service)
    {
    }
}
