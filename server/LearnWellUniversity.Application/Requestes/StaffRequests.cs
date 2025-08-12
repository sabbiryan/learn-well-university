using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Application.Requestes.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Requestes
{
    public record StaffCreateRequest(string FirstName, string LastName, string Email, string? Phone, string Code, string? Position, DateTime? DateOfBirth, int DepartmentId);

    public record StaffUpdateRequest(string FirstName, string LastName, string Email, string? Phone, string Code, string? Position, DateTime? DateOfBirth, int DepartmentId): RequestBase<int>;
}
