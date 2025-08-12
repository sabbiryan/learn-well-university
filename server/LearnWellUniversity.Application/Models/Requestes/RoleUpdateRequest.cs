using LearnWellUniversity.Application.Models.Requestes.Bases;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record RoleUpdateRequest(
        string Name,
        string? DisplayName,
        int[]? ResourceIds = null
    ): UpdateRequestBase<int>;
    
}
