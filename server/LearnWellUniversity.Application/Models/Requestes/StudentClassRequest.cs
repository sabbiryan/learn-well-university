using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record StudentClassRequest(
        int StudentId,
        int ClassId
    );
}
