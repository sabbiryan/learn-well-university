using Testcontainers.PostgreSql;
using Xunit;

namespace LearnWellUniversity.Shared.Tests.Setups
{
    public class PostgresTestContainer : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _container;

        public string ConnectionString => _container.GetConnectionString();

        public PostgresTestContainer()
        {
            _container = new PostgreSqlBuilder()
              .WithImage("postgres:latest")
              .WithDatabase("learn_well_university_test_db")
              .WithUsername("postgres")
              .WithPassword("postgres")
              .WithCleanUp(true)
              .Build();
        }


        public async Task InitializeAsync()
        {          
            await _container.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _container.StopAsync();
        }
    }
}
