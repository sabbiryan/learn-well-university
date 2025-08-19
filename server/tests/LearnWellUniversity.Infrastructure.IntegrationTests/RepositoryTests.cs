using FluentAssertions;
using LearnWellUniversity.Domain.Entities.Auths;
using LearnWellUniversity.Infrastructure.IntegrationTests.Bases;
using LearnWellUniversity.Infrastructure.IntegrationTests.Setups;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LearnWellUniversity.Infrastructure.IntegrationTests
{
    public class RepositoryTests(InfrastructureIntegrationTestFixture fixture) : InfrastructureIntegrationTestBase(fixture)
    {

        [Fact]
        public async Task AddAsync_ShouldInsertEntity()
        {
            // Arrange
            var user = new User { 
                Email = "test@example.com", 
                FirstName = "Test", 
                LastName = "User", 
                PasswordHash = Encoding.UTF8.GetBytes("passwordHash"), 
                PasswordSalt = Encoding.UTF8.GetBytes("passwordSalt") 
            };

            // Act
            await UnitOfWork.Repository<User>().AddAsync(user);
            await DbContext.SaveChangesAsync();

            var result = await UnitOfWork.Repository<User>().GetByIdAsync(user.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Email.Should().Be("test@example.com");
        }



        [Fact]
        public async Task FilterAsync_ShouldReturnMatchingEntities()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Email = "a@example.com", FirstName = "A" ,LastName= "A", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("slat") },
                new User { Email = "b@example.com", FirstName = "B" ,LastName= "B", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("slat") },
            };

            await DbContext.Users.AddRangeAsync(users);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await UnitOfWork.Repository<User>().FilterAsync(u => u.FullName.Contains("A"));

            // Assert
            result.Should().ContainSingle(u => u.FullName == "A A");
        }


        [Fact]
        public async Task Update_ShouldModifyEntity()
        {
            // Arrange
            var user = new User { Email = "old@example.com", FirstName = "Old", LastName = "User", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("slat") };
            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();

            // Act
            user.Email = "new@example.com";
            UnitOfWork.Repository<User>().Update(user);
            await DbContext.SaveChangesAsync();

            var result = await UnitOfWork.Repository<User>().GetByIdAsync(user.Id);

            // Assert
            result!.Email.Should().Be("new@example.com");
        }


        [Fact]
        public async Task Remove_ShouldDeleteEntity()
        {
            // Arrange
            var user = new User { Email = "delete@example.com", FirstName = "Delete", LastName = "User", PasswordHash = Encoding.UTF8.GetBytes("hash"), PasswordSalt = Encoding.UTF8.GetBytes("slat") };
            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();

            // Act
            UnitOfWork.Repository<User>().Remove(user);
            await DbContext.SaveChangesAsync();

            var result = await UnitOfWork.Repository<User>().GetByIdAsync(user.Id);

            // Assert
            result.Should().BeNull();
        }


        [Fact]
        public async Task BulkInsert_ShouldInsertMultipleEntities()
        {
            // Arrange
            var users = Enumerable.Range(1, 5)
                .Select(i => new User { Email = $"user{i}@example.com", FirstName = "User", LastName = $"{i}", PasswordHash = Encoding.UTF8.GetBytes($"hash{i}"), PasswordSalt = Encoding.UTF8.GetBytes($"slat{i}") })
                .ToList();

            // Act
            await UnitOfWork.Repository<User>().BulkInsertAsync(users);
            await DbContext.SaveChangesAsync();

            var count = await DbContext.Users.CountAsync();

            // Assert
            count.Should().BeGreaterThanOrEqualTo(5);
        }

    }
}
