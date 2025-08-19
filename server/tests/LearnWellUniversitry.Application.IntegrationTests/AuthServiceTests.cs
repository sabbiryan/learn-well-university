using FluentAssertions;
using LearnWellUniversitry.Application.IntegrationTests.Bases;
using LearnWellUniversitry.Application.IntegrationTests.Setups;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Models.Requestes.Auths;
using LearnWellUniversity.Application.Models.Statics;
using Microsoft.Extensions.DependencyInjection;

namespace LearnWellUniversity.Infrastructure.IntegrationTests
{
    public class AuthServiceTests(ApplicationIntegrationTestFixture fixture) : ApplicationIntegrationTestBase(fixture)
    {

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsValid()
        {
            // Arrange
            var request = new TokenRequest(StaticUser.Admin.Email, StaticUser.Admin.Password);
            var authService = ServiceProvider.GetRequiredService<IAuthService>();

            // Act
            var response = await authService.LoginAsync(request, "127.0.0.1");

            // Assert
            response.Should().NotBeNull();

            response.AccessToken.Should().NotBeNullOrWhiteSpace();
            response.RefreshToken.Should().NotBeNullOrWhiteSpace();

            response.AccessTokenExpiresAt.Should().BeAfter(DateTime.UtcNow);
            response.RefreshTokenExpiresAt.Should().BeAfter(DateTime.UtcNow);
        }



        [Fact]
        public async Task LoginAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var request = new TokenRequest("notfound@example.com", "Anything");
            var authService = ServiceProvider.GetRequiredService<IAuthService>();

            // Act
            Func<Task> act = async () => await authService.LoginAsync(request, "127.0.0.1");

            // Assert            
            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("User not found.");
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowException_WhenInvalidPassword()
        {
            // Arrange
            var request = new TokenRequest(StaticUser.Admin.Email, "WrongPassword");
            var authService = ServiceProvider.GetRequiredService<IAuthService>();

            // Act
            Func<Task> act = async () => await authService.LoginAsync(request, "127.0.0.1");

            // Assert            
            await act
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("Invalid credentials");
        }


        [Fact]
        public async Task RefreshTokenAsync_ShouldReturnNewToken_WhenRefreshTokenValid()
        {
            // Arrange
            var authService = ServiceProvider.GetRequiredService<IAuthService>();
            var token = await authService.LoginAsync(new TokenRequest(StaticUser.Admin.Email, StaticUser.Admin.Password), "127.0.0.1");
            var request = new TokenRefreshRequest(token.RefreshToken);
            DbContext.ChangeTracker.Clear();

            // Act
            var response = await authService.RefreshTokenAsync(request, "127.0.0.1");

            // Assert
            response.Should().NotBeNull();
            response.RefreshToken.Should().NotBeNullOrWhiteSpace();
        }


        [Fact]
        public async Task RefreshTokenAsync_ShouldThrowException_WhenRefreshTokenInvalid()
        {
            // Arrange
            var authService = ServiceProvider.GetRequiredService<IAuthService>();
            var request = new TokenRefreshRequest("InvalidToken");

            // Act
            Func<Task> act = async () => await authService.RefreshTokenAsync(request, "127.0.0.1");

            // Assert
            await act
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("Invalid or expired refresh token");
        }
    }
}
