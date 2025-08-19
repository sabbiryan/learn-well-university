using LearnWellUniversity.Application.Models.Encryptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Encryptions
{
   public interface IPasswordHasher
    {
        PasswordHash CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }   
}
