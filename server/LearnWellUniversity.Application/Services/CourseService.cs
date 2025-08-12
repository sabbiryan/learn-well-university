using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities;
using MapsterMapper;

namespace LearnWellUniversity.Application.Services
{
    public class CourseService : ApplicationCrudService<Course, CourseDto, int, CourseCreateRequest, CourseUpdateRequest>, ICourseService
    {
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
