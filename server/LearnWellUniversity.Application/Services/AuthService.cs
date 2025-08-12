using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Dtos.Auths;
using LearnWellUniversity.Application.Encryptions;
using LearnWellUniversity.Domain.Entities.Auths;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Services
{
    public class AuthService(
        IUnitOfWork unitOfWork, 
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<AuthService> logger
    ) : ApplicationService, IAuthService
    {
        public async Task<SignupResponse> RegisterAsync(SignupRequest request)
        {
            var existingUser = await unitOfWork.Repository<User>().FindAsync(x=> x.Email.Equals(request.Email));

            if (existingUser != null)
                throw new Exception("User already exists");

            PasswordHasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

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
                
                await AssingUserToRoles(user.Id, request.RoleIds);

                await unitOfWork.CommitTransactionAsync();                
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occurred while registering user");

                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
            
            return new SignupResponse(user.Id, user.Email);
        }

      
        public async Task<TokenResponse> LoginAsync(TokenRequest request)
        {
            var user = await unitOfWork.Repository<User>().FindAsync(x=> x.Email.Equals(request.Email));

            if (user == null || !PasswordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid credentials");

            var userRoles = await unitOfWork.Repository<UserRole>().FilterAsync(x => x.UserId == user.Id, x=> x.Role);

            if(userRoles == null || !userRoles.Any())
                throw new Exception("User has no roles assigned");

            var token = jwtTokenGenerator.GenerateToken(user, userRoles.Select(x=> x.Role));

            return new TokenResponse(token, user.Email);
        }


        public async Task AssingUserToRoles(int userId, int[] RoleIds)
        {
            List<UserRole> userRoles = [];

            foreach (var roleId in RoleIds)
            {
                userRoles.Add(new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }

            await unitOfWork.Repository<UserRole>().BulkInsertOrUpdateAsync(userRoles);

            await unitOfWork.SaveChangesAsync();

        }

        public async Task RemoveUserFromRoles(int id)
        {
            var userRoles = await unitOfWork.Repository<UserRole>().FilterAsync(x => x.UserId == id);

            if (userRoles == null || !userRoles.Any()) 
                return;

            await unitOfWork.Repository<UserRole>().BulkDeleteAsync(userRoles);
        }
    }
}
