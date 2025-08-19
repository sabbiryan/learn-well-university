using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Encryptions;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes.Auths;
using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LearnWellUniversity.Application.Services
{
    public class AuthService(
        IUnitOfWork unitOfWork, 
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        ILogger<AuthService> logger
    ) : ApplicationService, IAuthService
    {
        public async Task<SignupResponse> RegisterAsync(SignupRequest request)
        {
            var existingUser = await unitOfWork.Repository<User>().FindAsync(x=> x.Email.Equals(request.Email));

            if (existingUser != null)
                throw new Exception("User already exists");

            var passwordHash = passwordHasher.CreatePasswordHash(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = passwordHash.Hash,
                PasswordSalt = passwordHash.Salt,
                IsActive = true,
            };

            try
            {
                await unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    await unitOfWork.Repository<User>().AddAsync(user);
                    await unitOfWork.SaveChangesAsync();

                    await AssingUserToRoles(user.Id, request.RoleIds);
                });
         
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occurred while registering user");
                throw;
            }
            
            return new SignupResponse(user.Id, user.Email);
        }

      
        public async Task<TokenResponse> LoginAsync(TokenRequest request, string ipAddress)
        {
            var user = await unitOfWork.Repository<User>().FindAsync(x => x.Email.Equals(request.Email)) ?? throw new Exception("User not found.");

            if (!passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid credentials");

            var userRoles = await unitOfWork.Repository<UserRole>().FilterAsync(x => x.UserId == user.Id, x => x.Role);

            if (userRoles == null || !userRoles.Any())
                throw new Exception("User has no roles assigned");

            #region Check if the user is a staff or student

            int? studentId = null;
            var staff = await unitOfWork.Repository<Staff>().FindAsync(s => s.UserId == user.Id);
            if (staff == null)
            {
                var student = await unitOfWork.Repository<Student>().FindAsync(s => s.UserId == user.Id);
                studentId = student?.Id;
            }

            #endregion

            (string accessToken, DateTime accessTokenExpiresAt) = jwtTokenGenerator.GenerateAccessToken(user, staff?.Id, studentId, userRoles.Select(x => x.Role));

            (string refreshToken, DateTime refreshTokenExpiresAt) = jwtTokenGenerator.GenerateRefreshToken();

            await SaveRefreshTokenAsync(ipAddress, user.Id, refreshToken, refreshTokenExpiresAt);


            return new TokenResponse(accessToken, accessTokenExpiresAt, refreshToken, refreshTokenExpiresAt);
        }


        public async Task<TokenResponse> RefreshTokenAsync(TokenRefreshRequest request, string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                throw new ArgumentException("Refresh token is required", nameof(request));

            var refreshToken = await unitOfWork.Repository<RefreshToken>().FindAsync(x => x.Token == request.RefreshToken, x => x.User);

            if (refreshToken == null || !refreshToken.IsActive)
                throw new Exception("Invalid or expired refresh token");


            (string newRefreshToken, DateTime newRefreshTokenExpiresAt) = jwtTokenGenerator.GenerateRefreshToken();

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken;

            await unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                unitOfWork.Repository<RefreshToken>().Update(refreshToken);

                await unitOfWork.SaveChangesAsync();

                await SaveRefreshTokenAsync(ipAddress, refreshToken.UserId, newRefreshToken, newRefreshTokenExpiresAt);

            });

            // Generate new access token
            var userRoles = await unitOfWork.Repository<UserRole>().FilterAsync(x=> x.UserId == refreshToken.UserId, x => x.Role);
            var (AccessToken, AccessTokenExpiresAt) = jwtTokenGenerator.GenerateAccessToken(refreshToken.User, null, null, userRoles.Select(r => r.Role));

            return new TokenResponse(
                AccessToken,
                AccessTokenExpiresAt,
                newRefreshToken,
                newRefreshTokenExpiresAt
            );
        }

        public async Task<bool> RevokeRefreshTokenAsync(TokenRefreshRequest request, string ipAddress)
        {
            var refreshToken = await unitOfWork.Repository<RefreshToken>().FindAsync(x => x.Token == request.RefreshToken, x => x.User);

            if(refreshToken == null || !refreshToken.IsActive)
                throw new Exception("Invalid or expired refresh token");

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = null;

            unitOfWork.Repository<RefreshToken>().Update(refreshToken);

            await unitOfWork.SaveChangesAsync();

            return true;
        }


        private async Task SaveRefreshTokenAsync(string ipAddress, int userId, string refreshToken, DateTime refreshTokenExpiresAt)
        {
            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = refreshToken,
                ExpiresAt = refreshTokenExpiresAt,
                CreatedByIp = ipAddress,
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.Repository<RefreshToken>().AddAsync(refreshTokenEntity);

            await unitOfWork.SaveChangesAsync();
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
