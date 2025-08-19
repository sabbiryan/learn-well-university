using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Encryptions
{
    public record PasswordHash
    {
        public byte[] Hash { get; set; } = default!;
        public byte[] Salt { get; set; } = default!;
    }
}
