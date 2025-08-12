using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;

namespace LearnWellUniversity.Application.Contracts
{
    public interface IClassService : IApplicationCrudService<ClassDto, int, ClassCreateRequest, ClassUpdateRequest>;
    

}