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
            // Resource Mappings
            config.NewConfig<Resource, ResourceDto>()
                .MapWith(src => new ResourceDto(src.Name, src.DisplayName)
                {
                    Id = src.Id,
                })
                .TwoWays();

            // Role and Resource Mappings
            config.NewConfig<RoleResource, RoleResourceDto>()
                .MapWith(src => new RoleResourceDto(src.RoleId, src.ResourceId))
                .TwoWays();

            // Role Mappings
            config.NewConfig<Role, RoleDto>()
                .MapWith(src => new RoleDto(src.Name, src.DisplayName, src.IsStatic)
                {
                    Id = src.Id,                   
                })
                .TwoWays();
            config.NewConfig<Role, RoleCreateRequest>()
                .MapWith(src => new RoleCreateRequest(src.Name, src.DisplayName, null))
                .TwoWays();
            config.NewConfig<Role, RoleUpdateRequest>()
                .MapWith(src => new RoleUpdateRequest(src.Name, src.DisplayName, null)
                {
                    Id = src.Id
                })
                .TwoWays();

            // User Mappings
            config.NewConfig<UserRole, UserRoleDto>()
                .MapWith(src => new UserRoleDto(src.UserId, src.RoleId))
                .TwoWays();

            config.NewConfig<User, UserDto>()
                .MapWith(src => new UserDto(src.FirstName, src.LastName, src.Email, src.Phone, src.IsActive, src.IsEmailConfirmed)
                {
                    Id = src.Id
                })
                .TwoWays();
            config.NewConfig<User, UserUpdateRequest>()
                .MapWith(src => new UserUpdateRequest(src.FirstName, src.LastName, src.Phone, src.IsActive, default!)
                {
                    Id = src.Id
                })
                .TwoWays();

            // Address Mappings
            config.NewConfig<Address, AddressDto>()
                .MapWith(src => new AddressDto(src.Street, src.City, src.State, src.ZipCode, src.Country)
                {
                    Id = src.Id
                })
                .TwoWays();

            // Academic Mappings
            config.NewConfig<Class, ClassDto>()
                .MapWith(src => new ClassDto(src.Code, src.Name, src.Description)
                {
                    Id = src.Id
                })
                .TwoWays();
            config.NewConfig<Class, ClassCreateRequest>()
                .MapWith(src => new ClassCreateRequest(src.Code, src.Name, src.Description))
                .TwoWays();
            config.NewConfig<Class, ClassUpdateRequest>()
                .MapWith(src => new ClassUpdateRequest(src.Code, src.Name, src.Description)
                {
                    Id = src.Id
                })
                .TwoWays();

            // Course Mappings
            config.NewConfig<Course, CourseDto>()
                .MapWith(src => new CourseDto(src.Code, src.Name, src.CreditHour, src.Description)
                {
                    Id = src.Id
                })
                .TwoWays();
            config.NewConfig<Course, CourseCreateRequest>()
                .MapWith(src => new CourseCreateRequest(src.Code, src.Name, src.CreditHour, src.Description))
                .TwoWays();
            config.NewConfig<Course, CourseUpdateRequest>()
                .MapWith(src => new CourseUpdateRequest(src.Code, src.Name, src.CreditHour, src.Description)
                {
                    Id = src.Id
                })
                .TwoWays();

            // Department Mappings
            config.NewConfig<Department, DepartmentDto>()
                .MapWith(src => new DepartmentDto(src.Code, src.Name)
                {
                    Id = src.Id
                })
                .TwoWays();

            // Grading Mappings
            config.NewConfig<Grading, GradingDto>()
                .MapWith(src => new GradingDto(src.Name, src.Description, src.MinScore, src.MaxScore, src.GradePoint, src.IsActive)
                {
                    Id = src.Id
                })
                .TwoWays();

            // Schedule Mappings
            config.NewConfig<Schedule, ScheduleDto>()
                .Map(dest => dest.Day, src => src.Day.ToString())
                .MapWith(src => new ScheduleDto(src.Day, src.StartTime, src.EndTime, src.IsActive)
                {
                    Id = src.Id
                })
                .TwoWays();

            config.NewConfig<ClassSchedule, ClassScheduleDto>()
                .Map(dest => dest.ClassCode, src => src.Class == null ? null : src.Class.Code)
                .Map(dest => dest.ClassName, src => src.Class == null ? null : src.Class.Name)
                .Map(dest => dest.ScheduleDay, src => src.Schedule.Day.ToString())
                .Map(dest => dest.ScheduleStartTime, src => src.Schedule.StartTime.ToString(@"hh\:mm"))
                .Map(dest => dest.ScheduleEndTime, src => src.Schedule.EndTime.ToString(@"hh\:mm"))
                .TwoWays();

            // Staff Mappings
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
            
            config.NewConfig<Staff, StaffCreateRequest>()
                .MapWith(src => new StaffCreateRequest(src.FirstName, src.LastName, src.Email, src.Phone, src.Code, src.Position, src.DateOfBirth, src.DepartmentId)
                {
                    UserId = src.UserId
                })
                .TwoWays();

            config.NewConfig<Staff, StaffUpdateRequest>()
                .MapWith(src => new StaffUpdateRequest(src.FirstName, src.LastName, src.Email, src.Phone, src.Code, src.Position, src.DateOfBirth, src.DepartmentId)
                {
                    Id = src.Id
                })
                .TwoWays();

            // Student Mappings
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

            config.NewConfig<Student, StudentCreateRequest>()
                .MapWith(src => new StudentCreateRequest(src.Code, src.FirstName, src.LastName, src.Email, src.Phone, src.AcademicLevel, src.DateOfBirth, src.DepartmentId, src.EnrollmentDate))
                .TwoWays();

            config.NewConfig<Student, StudentUpdateRequest>()
                .MapWith(src => new StudentUpdateRequest(src.Code, src.FullName, src.Email, src.Phone, src.AcademicLevel, src.DateOfBirth, src.DepartmentId, src.EnrollmentDate)
                {
                    Id = src.Id
                })
                .TwoWays();

            // CourseClass Mappings
            config.NewConfig<CourseClass, CourseClassDto>()
                .Map(dest => dest.ClassCode, src => src.Class == null ? null : src.Class.Code)
                .Map(dest => dest.ClassName, src => src.Class == null ? null : src.Class.Name)
                .Map(dest => dest.CourseCode, src => src.Course == null ? null : src.Course.Code)
                .Map(dest => dest.CourseName, src => src.Course == null ? null : src.Course.Name)
                .Map(dest => dest.EnrollmentStaffName, src => src.EnrollmentStaff == null ? null : src.EnrollmentStaff.FullName)
                .MapWith(src => new CourseClassDto(
                    src.CourseId,
                    src.Course == null ? null : src.Course.Code,
                    src.Course == null ? null : src.Course.Name,
                    src.ClassId,
                    src.Class == null ? null : src.Class.Code,
                    src.Class == null ? null : src.Class.Name,
                    src.EnrollmentDate,
                    src.EnrollmentStaffId,
                    src.EnrollmentStaff == null ? null : src.EnrollmentStaff.FullName))
                .TwoWays();

            config.NewConfig<CourseClass, CourseClassRequest>()
                .MapWith(src => new CourseClassRequest(src.CourseId, src.ClassId));

            config.NewConfig<CourseClassRequest, CourseClass>()
                .Ignore(dest => dest.Course)
                .Ignore(dest => dest.Class)
                .Ignore(dest => dest.EnrollmentStaff);

            // StudentClass Mappings
            config.NewConfig<StudentClass, StudentClassDto>()
                .Map(dest => dest.ClassName, src => src.Class == null ? null : src.Class.Name)
                .Map(dest => dest.StudentName, src => src.Student == null ? null : src.Student.FullName)
                .Map(dest => dest.EnrollmentStaffName, src => src.EnrollmentStaff == null ? null : src.EnrollmentStaff.FullName)
                .MapWith(src => new StudentClassDto(
                    src.StudentId,
                     src.Student == null ? null : src.Student.FullName, 
                    src.ClassId,
                    src.Class == null ? null : src.Class.Name, 
                    src.EnrollmentDate,
                    src.EnrollmentStaffId,
                    src.EnrollmentStaff == null ? null : src.EnrollmentStaff.FullName))
                .TwoWays();

            config.NewConfig<StudentClass, StudentClassRequest>()
                .MapWith(src => new StudentClassRequest(src.StudentId, src.ClassId));

            config.NewConfig<StudentClassRequest, StudentClass>()
                .Ignore(dest => dest.Student)
                .Ignore(dest => dest.Class)
                .Ignore(dest => dest.EnrollmentStaff);

            config.NewConfig<CourseClassRequest, CourseClassDto>()
                .MapWith(src => new CourseClassDto(src.CourseId, null, null, src.ClassId, null, null, DateTime.Now, 0, null));

            // StudentCourse Mappings
            config.NewConfig<StudentCourse, StudentCourseDto>()
                .Map(dest => dest.CourseCode, src => src.Course == null ? null : src.Course.Code)
                .Map(dest => dest.CourseName, src => src.Course == null ? null : src.Course.Name)
                .Map(dest => dest.StudentCode, src => src.Student == null ? null : src.Student.Code)
                .Map(dest => dest.StudentName, src => src.Student == null ? null : src.Student.FullName)
                .Map(dest => dest.GradingName, src => src.Grading == null ? null : src.Grading.Name)
                .Map(dest => dest.EnrollmentStaffName, src => src.EnrollmentStaff == null ? null : src.EnrollmentStaff.FullName)
                .MapWith(src => new StudentCourseDto(
                    src.StudentId,
                    src.Student == null ? null : src.Student.Code,
                    src.Student == null ? null : src.Student.FullName,
                    src.CourseId,
                    src.Course == null ? null : src.Course.Code,
                    src.Course == null ? null : src.Course.Name,
                    src.Score,
                    src.GradingId,
                    src.Grading == null ? null : src.Grading.Name,
                    src.EnrollmentDate,
                    src.EnrollmentStaffId,
                    src.EnrollmentStaff == null ? null : src.EnrollmentStaff.FullName))
                .TwoWays();

            config.NewConfig<StudentCourse, StudentCourseRequest>()
                .MapWith(src => new StudentCourseRequest(src.StudentId, src.CourseId));

            config.NewConfig<StudentCourseRequest, StudentCourse>()
                .Ignore(dest => dest.Student)
                .Ignore(dest => dest.Course)
                .Ignore(dest => dest.EnrollmentStaff)
                .TwoWays();
        }
    }
}
