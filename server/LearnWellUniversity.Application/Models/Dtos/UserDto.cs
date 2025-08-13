using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record UserDto(
        string FirstName, 
        string LastName, 
        string Email,
        string? Phone, 
        bool IsActive, 
        bool IsEmailConfirmed
    ) : DtoBase<int>;
}
