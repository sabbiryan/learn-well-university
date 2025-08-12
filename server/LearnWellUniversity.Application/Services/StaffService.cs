using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Application.Requestes;
using LearnWellUniversity.Domain.Entities;
using MapsterMapper;
using System.Linq.Expressions;

namespace LearnWellUniversity.Application.Services
{
    public class StaffService : ApplicationCrudService<Staff,StaffDto,int, StaffCreateRequest, StaffUpdateRequest>, IStaffService
    {

        List<Expression<Func<Staff, object>>> includes = [s => s.Department, s => s.PresentAddress!, s => s.PermanentAddress!];

        Expression<Func<Staff, StaffDto>> selector = x => new StaffDto()
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

        public StaffService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            Selector = selector;
            Includes = includes;
        }
    }
}
