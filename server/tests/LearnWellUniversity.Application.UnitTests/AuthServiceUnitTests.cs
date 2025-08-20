using Castle.Core.Logging;
using FluentAssertions;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Encryptions;
using LearnWellUniversity.Application.Models.Encryptions;
using LearnWellUniversity.Application.Models.Requestes.Auths;
using LearnWellUniversity.Application.Services;
using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.Domain.Entities.Auths;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using System.Text;

namespace LearnWellUniversity.Application.UnitTests
{
    public class AuthServiceUnitTests
    {

        private readonly Mock<IUnitOfWork> __unitOfWorkMock;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<ILogger<AuthService>> _loggerMock = new();
        private readonly AuthService _authService;

        public AuthServiceUnitTests()
        {
            __unitOfWorkMock = new Mock<IUnitOfWork>();
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _passwordHasherMock = new Mock<IPasswordHasher>();

            _authService = new AuthService(
                __unitOfWorkMock.Object,
                _jwtTokenGeneratorMock.Object,
                _passwordHasherMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsValid()
        {
            // Arrange
            var request = new TokenRequest("admin@example.com", "password");
            
            var user = new User { 
                Id = 1, 
                Email = request.Email, 
                PasswordHash = new byte[64], 
                PasswordSalt = new byte[64],
            };

            var roles = new List<UserRole>
            {
                new UserRole
                {
                    UserId = user.Id,
                    Role = new Role { Id = 1, Name = "Admin" }
                }
            };

            
            var userRepositoryMock = new Mock<IRepository<User>>();
            var userRoleRepositoryMock = new Mock<IRepository<UserRole>>();
            var staffRepositoryMock = new Mock<IRepository<Staff>>();
            var studentRepositoryMock = new Mock<IRepository<Student>>();
            var refreshTokenRepositoryMock = new Mock<IRepository<RefreshToken>>();

            userRepositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);
            __unitOfWorkMock.Setup(u => u.Repository<User>())
                .Returns(userRepositoryMock.Object);



            userRoleRepositoryMock.Setup(r => r.FilterAsync(
                    It.IsAny<Expression<Func<UserRole, bool>>>(),
                    It.IsAny<Expression<Func<UserRole, object>>[]>()
                ))
                .ReturnsAsync(roles);
            __unitOfWorkMock.Setup(u => u.Repository<UserRole>())
                .Returns(userRoleRepositoryMock.Object);

            staffRepositoryMock.Setup(s => s.FindAsync(It.IsAny<Expression<Func<Staff, bool>>>()))
                .ReturnsAsync((Staff?)null);
            __unitOfWorkMock.Setup(u => u.Repository<Staff>())
                .Returns(staffRepositoryMock.Object);

            studentRepositoryMock.Setup(s => s.FindAsync(It.IsAny<Expression<Func<Student, bool>>>()))
                .ReturnsAsync((Student?)null);
            __unitOfWorkMock.Setup(u => u.Repository<Student>())
                .Returns(studentRepositoryMock.Object);


            _passwordHasherMock.Setup(h => h.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                .Returns(true);

            _jwtTokenGeneratorMock.Setup(j => j.GenerateAccessToken(
                 It.IsAny<User>(),
                 It.IsAny<int?>(),
                 It.IsAny<int?>(),
                 It.Is<IEnumerable<Role>>(r => r.Any(role => role.Name == "Admin")),
                 It.IsAny<IEnumerable<string>>()
                 ))
             .Returns((User u, int? staffId, int? studentId, IEnumerable<Role> roles) =>
                 ("access123", DateTime.UtcNow.AddMinutes(30))
             );

            _jwtTokenGeneratorMock.Setup(j => j.GenerateRefreshToken())
                .Returns(("refresh123", DateTime.UtcNow.AddDays(7)));

            refreshTokenRepositoryMock.Setup(r => r.AddAsync(It.IsAny<RefreshToken>()))
                .Returns(Task.CompletedTask);
            __unitOfWorkMock.Setup(u => u.Repository<RefreshToken>())
                .Returns(refreshTokenRepositoryMock.Object);

            // Act
            var response = await _authService.LoginAsync(request, "127.0.0.1");

            // Assert
            response.Should().NotBeNull();
            response.AccessToken.Should().Be("access123");
            response.RefreshToken.Should().Be("refresh123");
        }


        [Fact]
        public async Task LoginAsync_ShouldThrow_WhenUserNotFound()
        {
            // Arrange
            var request = new TokenRequest("notfound@example.com", "pwd");

            var userRepositoryMock = new Mock<IRepository<User>>();

            userRepositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync((User?)null);
            __unitOfWorkMock.Setup(r => r.Repository<User>())
                .Returns(userRepositoryMock.Object);

            // Act
            Func<Task> act = async () => await _authService.LoginAsync(request, "127.0.0.1");

            // Assert
            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("User not found.");
        }

        [Fact]
        public async Task LoginAsync_ShouldThrow_WhenPasswordInvalid()
        {
            // Arrange
            var request = new TokenRequest("admin@example.com", "wrong");

            var user = new User { Id = 1, Email = request.Email, PasswordHash = new byte[64], PasswordSalt = new byte[64] };

            var userRepositoryMock = new Mock<IRepository<User>>();
            userRepositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            __unitOfWorkMock.Setup(r => r.Repository<User>())
                .Returns(userRepositoryMock.Object);

            _passwordHasherMock.Setup(h => h.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                .Returns(false);

            // Act
            Func<Task> act = async () => await _authService.LoginAsync(request, "127.0.0.1");

            // Assert
            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("Invalid credentials");
        }
    }
}
