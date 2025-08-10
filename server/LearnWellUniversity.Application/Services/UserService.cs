using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Jwt;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Application.Encryptions;
using LearnWellUniversity.Domain.Entities.Securities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork, 
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<UserService> logger) : ApplicationServiceBase, IUserService
    {
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await unitOfWork.Repository<User>().FindAsync(x=> x.Email.Equals(request.Email));

            if (existingUser != null)
                throw new Exception("User already exists");

            new PasswordHasher().CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true,
            };

            try
            {
                await unitOfWork.BeginTransactionAsync();

                await unitOfWork.Repository<User>().AddAsync(user);
                await unitOfWork.SaveChangesAsync();

                
                await unitOfWork.Repository<UserRole>().AddAsync(new UserRole
                {
                    UserId = user.Id,
                    RoleId = request.RoleId
                });
                await unitOfWork.SaveChangesAsync();


                await unitOfWork.CommitTransactionAsync();                
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occurred while registering user");

                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
            
            return new AuthResponse("", user.Email);
        }


        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await unitOfWork.Repository<User>().FindAsync(x=> x.Email.Equals(request.Email));

            if (user == null || !new PasswordHasher().VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid credentials");

            var userRoles = await unitOfWork.Repository<UserRole>().FilterAsync(x => x.UserId == user.Id, x=> x.Role);

            if(userRoles == null || !userRoles.Any())
                throw new Exception("User has no roles assigned");

            var token = jwtTokenGenerator.GenerateToken(user, userRoles.Select(x=> x.Role));

            return new AuthResponse(token, user.Email);
        }
    }
}
