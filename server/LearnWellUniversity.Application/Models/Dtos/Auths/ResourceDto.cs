using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos.Auths
{
    public record ResourceDto(string Name, string? DisplayName) : DtoBase<int>;
}
