using LearnWellUniversity.Application.Models.Dtos.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Dtos
{
    public record AddressDto(string Street, string City,  string State, string ZipCode, string Country) : DtoBase<int>;
}
