using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;

namespace LearnWellUniversity.Application.Contracts
{
    public interface ICourseService: IApplicationCrudService<CourseDto, int, CourseCreateRequest, CourseUpdateRequest>;

}