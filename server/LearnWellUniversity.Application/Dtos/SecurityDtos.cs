using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Dtos
{
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password, string? Phone, int RoleId);
    public record LoginRequest(string Email, string Password);
    public record AuthResponse(string Token, string Email);
}
