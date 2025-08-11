using LearnWellUniversity.Application.Dtos.Bases;

namespace LearnWellUniversity.Application.Dtos
{
    public record DepartmentDto(string Code, string Name) : DtoBase<int>;
   
}
