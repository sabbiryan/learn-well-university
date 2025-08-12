using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;
using Mapster;

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
                            : new DepartmentDto(src.Department.Code, src.Department.Name) { Id = src.DepartmentId })
                .Map(dest => dest.PresentAddress,
                     src => src.PresentAddress == null
                            ? null
                            : new AddressDto(src.PresentAddress.Street, src.PresentAddress.City, src.PresentAddress.State, src.PresentAddress.ZipCode, src.PresentAddress.Country) 
                            { 
                                Id = src.PresentAddressId ?? 0
                            })
                .Map(dest => dest.PermanentAddress,
                     src => src.PermanentAddress == null
                            ? null
                            : new AddressDto(src.PermanentAddress.Street, src.PermanentAddress.City, src.PermanentAddress.State, src.PermanentAddress.ZipCode, src.PermanentAddress.Country) 
                            {
                                Id = src.PermanentAddressId ?? 0
                            })
                .TwoWays();

            config.NewConfig<Department, DepartmentDto>().TwoWays();

            config.NewConfig<Address, AddressDto>().TwoWays();

            config.NewConfig<User, UserDto>().TwoWays();

        }
    }
}
