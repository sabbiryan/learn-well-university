using LearnWellUniversity.Application.Dtos.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Dtos
{
    public record AddressDto(string Street, string City,  string State, string ZipCode, string Country) : DtoBase<int>;
}
