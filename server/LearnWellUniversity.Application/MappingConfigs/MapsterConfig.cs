using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;
using Mapster;

namespace LearnWellUniversity.Application.MappingConfigs
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<Resource, ResourceDto>().TwoWays();
            config.NewConfig<RoleResource, RoleResourceDto>().TwoWays();

            config.NewConfig<Role, RoleDto>().TwoWays();
            config.NewConfig<Role, RoleCreateRequest>().TwoWays();
            config.NewConfig<Role, RoleUpdateRequest>().TwoWays();            
            config.NewConfig<UserRole, UserRoleDto>().TwoWays();

            config.NewConfig<User, UserDto>().TwoWays();
            config.NewConfig<User, UserUpdateRequest>().TwoWays();


            config.NewConfig<Address, AddressDto>().TwoWays();   

            config.NewConfig<Class, ClassDto>().TwoWays();
            config.NewConfig<Class, ClassCreateRequest>().TwoWays();
            config.NewConfig<Class, ClassUpdateRequest>().TwoWays();

            config.NewConfig<Course, CourseDto>().TwoWays();
            config.NewConfig<Course, CourseCreateRequest>().TwoWays();
            config.NewConfig<Course, CourseUpdateRequest>().TwoWays();

            config.NewConfig<Department, DepartmentDto>().TwoWays();

            config.NewConfig<Grading, GradingDto>().TwoWays();

            config.NewConfig<Schedule, ScheduleDto>()
                .Map(dest => dest.Day, src => src.Day.ToString())
                .TwoWays();



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
            config.NewConfig<Staff, StaffCreateRequest>().TwoWays();
            config.NewConfig<StaffUpdateRequest, Staff>().TwoWays();


            config.NewConfig<Student, StudentDto>()
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
            config.NewConfig<Student, StudentCreateRequest>().TwoWays();
            config.NewConfig<StudentUpdateRequest, Student>().TwoWays();

        }
    }
}
