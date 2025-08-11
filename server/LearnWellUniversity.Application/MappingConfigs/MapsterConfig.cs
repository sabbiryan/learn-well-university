using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.MappingConfigs
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Staff, StaffDto>()
            .Map(dest => dest.Department,
                 src => src.Department == null
                        ? null
                        : new DepartmentDto(src.Department.Code, src.Department.Name))
            .Map(dest => dest.PresentAddress,
                 src => src.PresentAddress == null
                        ? null
                        : new AddressDto(src.PresentAddress.Street, src.PresentAddress.City, src.PresentAddress.State, src.PresentAddress.ZipCode, src.PresentAddress.Country))
            .Map(dest => dest.PermanentAddress,
                 src => src.PermanentAddress == null
                        ? null
                        : new AddressDto(src.PermanentAddress.Street, src.PermanentAddress.City, src.PermanentAddress.State, src.PermanentAddress.ZipCode, src.PermanentAddress.Country));

            config.NewConfig<Department, DepartmentDto>();
            
            config.NewConfig<Address, AddressDto>();


        }
    }
}
