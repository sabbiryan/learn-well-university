using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Requestes.Auths
{
    public record TokenResponse(string Token, string Email);
}
