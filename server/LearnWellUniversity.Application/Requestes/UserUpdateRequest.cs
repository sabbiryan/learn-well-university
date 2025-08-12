using LearnWellUniversity.Application.Requestes.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Requestes
{
    public record UserUpdateRequest(string FirName, string LastName, string? Phone, bool IsActive, int[] RoleIds) : RequestBase<int>
    {
    }
}
