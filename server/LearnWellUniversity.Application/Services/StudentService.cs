using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities;
using MapsterMapper;
using System.Linq.Expressions;

namespace LearnWellUniversity.Application.Services
{
    public class StudentService : ApplicationCrudService<Student, StudentDto, int, StudentCreateRequest, StudentUpdateRequest>, IStudentService
    {
        readonly List<Expression<Func<Student, object>>> includes = [s => s.Department, s => s.PresentAddress!, s => s.PermanentAddress!];

        readonly Expression<Func<Student, StudentDto>> selector = x => new StudentDto()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Phone = x.Phone,
            Code = x.Code,
            DateOfBirth = x.DateOfBirth,
            Department = x.Department != null ? new DepartmentDto(x.Department.Code, x.Department.Name) : null,
            PresentAddress = x.PresentAddress != null ? new AddressDto(x.PresentAddress.Street, x.PresentAddress.City, x.PresentAddress.State, x.PresentAddress.ZipCode, x.PresentAddress.Country) : null,
            PermanentAddress = x.PermanentAddress != null ? new AddressDto(x.PermanentAddress.Street, x.PermanentAddress.City, x.PermanentAddress.State, x.PermanentAddress.ZipCode, x.PermanentAddress.Country) : null
        };

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public StudentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthService authService) : base(unitOfWork, mapper)
        {
            Selector = selector;
            Includes = includes;

            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public override async Task<int> AddAsync(StudentCreateRequest request)
        {
            int id = 0;
            
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var user = await _authService.RegisterAsync(new SignupRequest(request.FirstName, request.LastName, request.Email, request.Password, request.Phone, request.RoleIds));
                
                request.UserId = user.Id;

                id = await base.AddAsync(request);
            });

            return id;
        }


        public async Task<List<StudentClassessFriendListDto>> GetClassesFriendsAsync(int studentId)
        {
            if (studentId <= 0)
            {
                throw new ArgumentException("Invalid student ID");
            }

            var studentClasses = await _unitOfWork.Repository<StudentClass>()
                .FilterAsync(sc => sc.StudentId == studentId, x=> x.Class, x => x.Student);

            var studentClassFriends = studentClasses.Select(x => new { ClassName = x.Class.Name, x.Student })
                .GroupBy(x => x.ClassName)
                .ToList()
                .ConvertAll(x => new StudentClassessFriendListDto
                {
                    ClassName = x.Key,
                    Friends = [.. x.Select(f => new StudentClassFriendDto
                    {
                        FirstName = f.Student.FirstName,
                        LastName = f.Student.LastName
                    })]
                });

            return studentClassFriends;
        }
    }

}
