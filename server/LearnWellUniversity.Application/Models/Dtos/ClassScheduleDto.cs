using LearnWellUniversity.Application.Models.Dtos.Bases;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record ClassScheduleDto(
        int ClassId,
        string ClassCode,
        string ClassName,
        string ScheduleDay,
        TimeOnly ScheduleStartTime,
        TimeOnly ScheduleEndTime) : DtoBase<int>;
}