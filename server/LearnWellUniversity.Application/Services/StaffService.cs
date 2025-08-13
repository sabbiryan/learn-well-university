using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Application.Models.Statics;
using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;
using MapsterMapper;
using System.Linq.Expressions;

namespace LearnWellUniversity.Application.Services
{
    public class StaffService : ApplicationCrudService<Staff, StaffDto, int, StaffCreateRequest, StaffUpdateRequest>, IStaffService
    {

        readonly List<Expression<Func<Staff, object>>> includes = [s => s.Department, s => s.PresentAddress!, s => s.PermanentAddress!];

        readonly Expression<Func<Staff, StaffDto>> selector = x => new StaffDto()
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

        public StaffService(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IAuthService authService) : base(unitOfWork, mapper)
        {
            Selector = selector;
            Includes = includes;

            _unitOfWork = unitOfWork;
            _authService = authService;
        }


        public override async Task<int> AddAsync(StaffCreateRequest request)
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
    }
}
