using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Domain.Entities.Auths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork) : ApplicationServiceBase, IUserService
    {

        public async Task<PaginatedResult<UserDto>> GetAllAsync(DynamicQuery request)
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


        public Task<UserDto?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }


        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
