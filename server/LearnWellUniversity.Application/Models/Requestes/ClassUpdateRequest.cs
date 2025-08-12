using LearnWellUniversity.Application.Models.Requestes.Bases;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record ClassUpdateRequest(
        string Name,
        string Code,
        string? Description
    ): UpdateRequestBase<int>;
}
