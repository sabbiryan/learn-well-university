using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record GradingDto(string Name, string Description, decimal MinScore, decimal MaxScore, decimal GradePoint, bool IsActive) : DtoBase<int>;


}
