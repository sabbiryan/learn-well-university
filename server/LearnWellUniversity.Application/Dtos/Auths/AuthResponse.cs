using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Dtos.Auths
{
    public record AuthResponse(string Token, string Email);
}
