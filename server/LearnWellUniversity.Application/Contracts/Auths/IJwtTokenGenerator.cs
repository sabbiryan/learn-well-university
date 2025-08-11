using LearnWellUniversity.Domain.Entities.Securities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Contracts.Auths
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, IEnumerable<Role> roles);
    }
}
