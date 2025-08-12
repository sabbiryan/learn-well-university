using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Exceptions;
using LearnWellUniversity.Application.Models.Common.Paginations;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities.Auths;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork,
        IAuthService authService,
        IMapper mapper) : ApplicationService, IUserService
    {

        public async Task<PaginatedResult<UserDto>> GetPagedAsync(DynamicQueryRequest request)
        {
            Expression<Func<User, UserDto>> selector = x => new UserDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                IsActive = x.IsActive,
            };


            List<Expression<Func<User, object>>> includes = [u => u.UserRoles];

            var result = await unitOfWork.Repository<User>().GetPagedAsync(request, selector, includes);

            return result;
        }


        public async Task<UserDto?> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

            var user = await unitOfWork.Repository<User>().GetByIdAsync(id);

            if (user == null) return null;

            var result = mapper.Map<UserDto>(user);

            return result;
        }


        public async Task<int> UpdateAsync(UserUpdateRequest request)
        {
            if (request.Id <= 0) throw new ArgumentOutOfRangeException(nameof(request.Id), "Id must be greater than zero.");

            var user = await unitOfWork.Repository<User>().GetByIdAsync(request.Id);

            if (user == null) throw new EntityNotFoundException($"User with id {request.Id} not found.");

            mapper.Map(request, user);

            unitOfWork.Repository<User>().Update(user);

            await authService.AssingUserToRoles(request.Id, request.RoleIds);

            await unitOfWork.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");

            var user = await unitOfWork.Repository<User>().GetByIdAsync(id);

            if (user == null) throw new EntityNotFoundException($"User with id {id} not found.");

            unitOfWork.Repository<User>().Remove(user);

            await authService.RemoveUserFromRoles(id);

            await unitOfWork.SaveChangesAsync();
        }
    
    
    }
}
