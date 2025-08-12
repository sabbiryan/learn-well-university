using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record ClassDto(string Code, string Name, string? Description) : DtoBase<int>;
}
