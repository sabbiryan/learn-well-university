using LearnWellUniversity.Application.Models.Dtos.Bases;
using LearnWellUniversity.Domain.Enums;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record ScheduleDto(Day Day, TimeOnly StartTime, TimeOnly EndTime, bool IsActive) : DtoBase<int>;
}
