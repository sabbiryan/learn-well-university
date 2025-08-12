using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos.Auths
{
    public record RoleDto(string Name, string? DisplayName, bool IsStatic) : DtoBase<int>;
    

}
