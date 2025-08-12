using LearnWellUniversity.Application.Models.Requestes.Bases;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record CourseUpdateRequest(
        string Name,
        string Code,
        decimal CreditHour,
        string? Description
    ) : UpdateRequestBase<int>;
}
