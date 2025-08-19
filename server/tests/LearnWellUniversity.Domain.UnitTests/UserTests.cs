using FluentAssertions;
using LearnWellUniversity.Domain.Entities.Auths;
using System.Text;

namespace LearnWellUniversity.Domain.UnitTest
{
    public class UserTests
    {
        [Fact]
        public void User_Should_HaveRequiredProperties()
        {
            // Arrange
            var firstName = "Test";
            var lastName = "User";
            var email = "test.user@example.com";

            var passwordHash = Encoding.UTF8.GetBytes("hashedPassword");
            var passwordSalt = Encoding.UTF8.GetBytes("salt123");

            // Act
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            // Assert
            user.FirstName.Should().Be(firstName);
            user.LastName.Should().Be(lastName);
            user.Email.Should().Be(email);
            user.PasswordHash.Should().BeEquivalentTo(passwordHash);
            user.PasswordSalt.Should().BeEquivalentTo(passwordSalt);


            user.IsActive.Should().BeTrue();
            user.IsEmailConfirmed.Should().BeFalse();
            user.IsPasswordChangeOnFirstLogin.Should().BeFalse();
        }
    }
}
