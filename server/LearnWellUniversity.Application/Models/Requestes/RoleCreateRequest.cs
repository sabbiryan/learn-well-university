using LearnWellUniversity.Application.Models.Requestes.Bases;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record RoleCreateRequest(
        string Name,
        string? DisplayName,
        int[]? ResourceIds = null
    ): CreateRequestBase;
    
}
