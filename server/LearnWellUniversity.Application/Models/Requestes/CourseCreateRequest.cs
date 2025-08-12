using LearnWellUniversity.Application.Models.Requestes.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record CourseCreateRequest(
        string Name,
        string Code,
        decimal CreditHour,
        string? Description
    ): CreateRequestBase;
}
