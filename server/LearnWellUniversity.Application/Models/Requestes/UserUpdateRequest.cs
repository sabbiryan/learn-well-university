using LearnWellUniversity.Application.Models.Requestes.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Requestes
{
    public record UserUpdateRequest(string FirstName, string LastName, string? Phone, bool IsActive, int[] RoleIds) : RequestBase<int>
    {
    }


   
}
