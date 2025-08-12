using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities;
using MapsterMapper;

namespace LearnWellUniversity.Application.Services
{
    public class ClassService : ApplicationCrudService<Class, ClassDto, int, ClassCreateRequest, ClassUpdateRequest>, IClassService
    {
        public ClassService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            Selector = null;
            Includes = null; 
        }


    }
}
