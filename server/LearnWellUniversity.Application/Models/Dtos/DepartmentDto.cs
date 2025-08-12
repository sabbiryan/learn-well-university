using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record DepartmentDto(string Code, string Name) : DtoBase<int>;
   
}
